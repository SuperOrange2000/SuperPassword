using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using System.Formats.Asn1;
using System.Security.Cryptography;

namespace SuperPassword.Security.SecurityModule
{
    public class ChaCha20 : ISecurityModule
    {
        private ICipherParameters _key;

        public void SetArguments(byte[] key)
        {
            _key = new KeyParameter(key);
        }

        public byte[] GetBytes(byte[] iv, int length)
        {
            ICipherParameters parameters = new ParametersWithIV(_key, iv);
            IStreamCipher chacha = new ChaChaEngine();
            chacha.Init(true, parameters);

            // 生成并输出密钥流
            byte[] output = new byte[length]; // 输出64字节的密钥流作为示例
            chacha.ProcessBytes(new byte[length], 0, length, output, 0); // 生成密钥流
            chacha.Reset();

            return output;
        }

        public byte[] Encrypt(byte[] plaintext, out byte[] nonce, out byte[]? tag)
        {
            tag = null;
            nonce = new byte[16];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(nonce);
            }

            ICipherParameters parameters = new ParametersWithIV(_key, nonce);
            IStreamCipher chacha = new ChaChaEngine();
            chacha.Init(true, parameters);

            byte[] output = new byte[plaintext.Length];
            chacha.ProcessBytes(plaintext, 0, plaintext.Length, output, 0);
            chacha.Reset();

            return output;
        }

        public byte[] Decrypt(byte[] ciphertext, byte[] iv, byte[]? tag = null)
        {
            ICipherParameters parameters = new ParametersWithIV(_key, iv);
            IStreamCipher chacha = new ChaChaEngine();
            chacha.Init(false, parameters);

            byte[] output = new byte[ciphertext.Length];
            chacha.ProcessBytes(ciphertext, 0, ciphertext.Length, output, 0);
            chacha.Reset();

            return output;
        }
    }

}
