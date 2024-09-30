using SuperPassword.DAL.Implementations.Models;
using SuperPassword.DAL.Interfaces.Offline;
using SuperPassword.Entity.Interface;
using System.Security.Cryptography;
using System.Text;

namespace SuperPassword.DAL.Implementations.Offline
{
    public partial class OfflineService
    {
        public async Task<IOfflineResponse<byte[]>> LoginAsync(IUser user)
        {
            if (!_configService.AppConfig.UsernameMap.ContainsKey(user.Name))
                return new OfflineResponse<byte[]>() { DataStatus = ResponseDataStatus.ResourcesNotFoundError };
            byte[] spwd = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(user.Password),
                _configService.UserProperties.Salt,
                _configService.AppConfig.EncryptInterations,
                HashAlgorithmName.SHA512,
                _configService.AppConfig.PasswordLength
            );
            var verificationCode = Decrypt(spwd, _configService.UserProperties.VerificationCode);
            if (verificationCode != null && verificationCode.All(i => i == 0))
            {
                _configService.MountSaveFunction();
                await UpdateInfoGroupDbContextAsync();
                return new OfflineResponse<byte[]>()
                {
                    DataStatus = ResponseDataStatus.Success,
                    Content = Decrypt(spwd, _configService.UserProperties.EncryptedPassword)
                };
            }
            else return new OfflineResponse<byte[]>() { DataStatus = ResponseDataStatus.Forbidden };
        }

        public async Task<IOfflineResponse<byte[]>> SignUpAsync(IUser user)
        {
            byte[] internalPwd;
            if (_configService.AppConfig.UsernameMap.ContainsKey(user.Name))
                return new OfflineResponse<byte[]>() { DataStatus = ResponseDataStatus.NameConflictError };
            else
            {
                _configService.MountSaveFunction();
                byte[] spwd = Rfc2898DeriveBytes.Pbkdf2(
                    Encoding.UTF8.GetBytes(user.Password),
                    _configService.UserProperties.Salt,
                    _configService.AppConfig.EncryptInterations,
                    HashAlgorithmName.SHA512,
                    _configService.AppConfig.PasswordLength
                );
                internalPwd = new byte[_configService.AppConfig.PasswordLength];
                using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(internalPwd);
                }
                _configService.UserProperties.EncryptedPassword = Encrypt(spwd, internalPwd);
                _configService.UserProperties.VerificationCode = Encrypt(spwd, new byte[16]);
            }
            await UpdateInfoGroupDbContextAsync();
            return new OfflineResponse<byte[]>()
            {
                DataStatus = ResponseDataStatus.Success,
                Content = internalPwd
            };
        }

        private byte[] Encrypt(byte[] key, byte[] plainData)
        {
            using AesGcm aesGcm = new AesGcm(key, 16);
            byte[] nonce = new byte[12];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(nonce);
            }
            byte[] encryptedData = new byte[plainData.Length];
            byte[] tag = new byte[16];
            aesGcm.Encrypt(nonce, plainData, encryptedData, tag);

            return [.. encryptedData, .. nonce, .. tag];
        }

        private byte[]? Decrypt(byte[] key, byte[] cipherData)
        {
            using AesGcm aesGcm = new AesGcm(key, 16);
            byte[] encryptedData = cipherData.SkipLast(28).ToArray();
            byte[] nonce = cipherData.SkipLast(16).TakeLast(12).ToArray();
            byte[] tag = cipherData.TakeLast(16).ToArray();
            try
            {
                byte[] decryptedData = new byte[encryptedData.Length];
                aesGcm.Decrypt(nonce, encryptedData, tag!, decryptedData);
                return decryptedData;
            }
            catch (CryptographicException e)
            {
                // 认证失败，密文可能被篡改
                Console.WriteLine("Decryption failed: " + e.Message);
                return null;
            }
        }
    }
}
