using SuperPassword.DAL.Implementations.Models;
using SuperPassword.DAL.Interfaces.Models;
using SuperPassword.Entity.Interface;
using System.Security.Cryptography;
using System.Text;

namespace SuperPassword.DAL.Implementations.Offline
{
    internal partial class OfflineService
    {
        public async Task<IDALResponse<byte[]>> LoginAsync(IUser user)
        {
            if (!_configService.AppConfig.UserNameMap.ContainsKey(user.Name))
                return new DALResponse<byte[]>() { DataStatus = ResponseDataStatus.ResourcesNotFoundError };
            _configService.SwitchUser(user.Name);
            byte[] spwd = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(user.Password),
                _configService.UserProperties.Salt,
                _configService.AppConfig.EncryptInterations,
                HashAlgorithmName.SHA512,
                _configService.AppConfig.PasswordLength
            );
            if ((await DecryptAsync(spwd, _configService.UserProperties.VerificationCode, "VERIFICATION")).All(i => i == 0))
            {
                _configService.MountSaveFunction();
                return new DALResponse<byte[]>()
                {
                    DataStatus = ResponseDataStatus.Success,
                    Content = await DecryptAsync(spwd, _configService.UserProperties.EncryptedPassword, "PASSWORD")
                };
            }
            else return new DALResponse<byte[]>() { DataStatus = ResponseDataStatus.Forbidden };
        }

        public async Task<IDALResponse<byte[]>> SignUpAsync(IUser user)
        {
            byte[] internalpwd;
            if (_configService.AppConfig.UserNameMap.ContainsKey(user.Name))
                return new DALResponse<byte[]>() { DataStatus = ResponseDataStatus.NameConflictError };
            else
            {
                _configService.SwitchUser(user.Name, user.UserGuid);
                _configService.MountSaveFunction();
                byte[] spwd = Rfc2898DeriveBytes.Pbkdf2(
                    Encoding.UTF8.GetBytes(user.Password),
                    _configService.UserProperties.Salt,
                    _configService.AppConfig.EncryptInterations,
                    HashAlgorithmName.SHA512,
                    _configService.AppConfig.PasswordLength
                );
                internalpwd = new byte[_configService.AppConfig.PasswordLength];
                using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(internalpwd);
                }
                _configService.UserProperties.EncryptedPassword = await EncryptAsync(spwd, internalpwd, "PASSWORD");
                _configService.UserProperties.VerificationCode = await EncryptAsync(spwd, new byte[16], "VERIFICATION");
            }
            return new DALResponse<byte[]>()
            {
                DataStatus = ResponseDataStatus.Success,
                Content = internalpwd
            };
        }

        private async Task<byte[]> EncryptAsync(byte[] key, byte[] plainData, string serverName)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                using (SHA512 sha3Hash = SHA512.Create())
                {
                    aes.IV = sha3Hash.ComputeHash(
                        key.Concat(Encoding.UTF8.GetBytes($"USER_{serverName}_AES_IV")).ToArray()
                    ).Take(16).ToArray();
                }
                aes.Mode = CipherMode.CBC;
                // 创建加密器
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        csEncrypt.Write(plainData, 0, plainData.Length);
                        csEncrypt.FlushFinalBlock();
                        return msEncrypt.ToArray();
                    }
                }
            }
        }

        private async Task<byte[]> DecryptAsync(byte[] key, byte[] cipherData, string serverName)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                using (SHA512 sha3Hash = SHA512.Create())
                {
                    aes.IV = sha3Hash.ComputeHash(
                        key.Concat(Encoding.UTF8.GetBytes($"USER_{serverName}_AES_IV")).ToArray()
                    ).Take(16).ToArray();
                }
                aes.Mode = CipherMode.CBC;
                // 创建解密器
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream msDecrypt = new MemoryStream(cipherData))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        byte[] decryptedData = new byte[cipherData.Length];
                        int decryptedCount = csDecrypt.Read(decryptedData, 0, decryptedData.Length);
                        return decryptedData;
                    }
                }
            }
        }
    }
}
