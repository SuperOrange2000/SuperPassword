using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;

namespace SuperPassword.Security.SecurityModule
{
    public class ChaCha20 : ISecurityModule
    {
        private ICipherParameters _key;

        public ChaCha20(byte[] key)
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

        public byte[] Encrypt(byte[] plaintext, byte[] iv)
        {
            ICipherParameters parameters = new ParametersWithIV(_key, iv);
            IStreamCipher chacha = new ChaChaEngine();
            chacha.Init(true, parameters);

            byte[] output = new byte[plaintext.Length];
            chacha.ProcessBytes(plaintext, 0, plaintext.Length, output, 0);
            chacha.Reset();

            return output;
        }

        public byte[] Decrypt(byte[] ciphertext, byte[] iv)
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
