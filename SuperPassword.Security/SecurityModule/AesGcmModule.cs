using System.Security.Cryptography;
using System.Text;

namespace SuperPassword.Security.SecurityModule
{
    internal class AesGcmModule : ISecurityModule
    {
        private byte[] key { get; set; }
        private int tagSize { get; set; } = 16;
        public void SetArguments(byte[] key)
        {
            this.key = key;
        }
        public byte[]? Decrypt(byte[] encryptedData, byte[] nonce, byte[]? tag = null)
        {
            using (AesGcm aesGcm = new AesGcm(key, tagSize))
            {
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

        public byte[]? Encrypt(byte[] plainData, out byte[] nonce, out byte[]? tag)
        {
            using (AesGcm aesGcm = new AesGcm(key, tagSize))
            {
                nonce = new byte[12];
                using(RandomNumberGenerator rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(nonce);
                }
                byte[] encryptedData = new byte[plainData.Length];
                tag = new byte[tagSize];
                aesGcm.Encrypt(nonce, plainData, encryptedData, tag);
                return encryptedData;
            }
        }

        public byte[]? GetBytes(byte[] iv, int length)
        {
            throw new NotImplementedException();
        }
    }
}
