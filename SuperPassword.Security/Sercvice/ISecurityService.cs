using SuperPassword.Security.Sercvice;

namespace SuperPassword.Security.Service
{
    public interface ISecurityService
    {
        void SwitchCipher(SecurityMode mode, byte[] key);
        byte[]? Encrypt(byte[] plaintext, out byte[] nonce, out byte[]? tag);
        byte[]? Decrypt(byte[] encryptedData, byte[] nonce, byte[]? tag = null);
    }
}
