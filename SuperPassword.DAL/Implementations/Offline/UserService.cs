using SuperPassword.DAL.Models;
using SuperPassword.Entity.Interface;
using System.Security.Cryptography;
using System.Text;

namespace SuperPassword.DAL.Implementations.Offline
{
    public partial class OfflineService
    {
        public async Task<OfflineResponse<byte[]>> LoginAsync(string name, byte[] internalPwd)
        {
            if (!_configService.AppConfig.UsernameMap.ContainsKey(name))
                return new OfflineResponse<byte[]>() { DataStatus = ResponseDataStatus.ResourcesNotFoundError };

            var verificationCode = Decrypt(internalPwd, _configService.UserProperties.VerificationCode);
            if (verificationCode != null && verificationCode.All(i => i == 0))
            {
                _configService.MountSaveFunction();
                await UpdateInfoGroupDbContextAsync();
                return new OfflineResponse<byte[]>()
                {
                    DataStatus = ResponseDataStatus.Success,
                    Content = Decrypt(internalPwd, _configService.UserProperties.EncryptedPassword)
                };
            }
            else return new OfflineResponse<byte[]>() { DataStatus = ResponseDataStatus.Forbidden };
        }

        public async Task<OfflineResponse<byte[]>> SignUpAsync(string name, byte[] internalPwd)
        {
            byte[] spwd;
            if (_configService.AppConfig.UsernameMap.ContainsKey(name))
                return new OfflineResponse<byte[]>() { DataStatus = ResponseDataStatus.NameConflictError };
            else
            {
                _configService.MountSaveFunction();
                spwd = new byte[_configService.AppConfig.PasswordLength];
                using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(spwd);
                }
                _configService.UserProperties.EncryptedPassword = Encrypt(internalPwd, spwd);
                _configService.UserProperties.VerificationCode = Encrypt(internalPwd, new byte[16]);
            }
            await UpdateInfoGroupDbContextAsync();
            return new OfflineResponse<byte[]>()
            {
                DataStatus = ResponseDataStatus.Success,
                Content = spwd
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
