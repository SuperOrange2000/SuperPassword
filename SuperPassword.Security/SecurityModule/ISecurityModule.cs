namespace SuperPassword.Security.SecurityModule
{
    public interface ISecurityModule
    {
        void SetArguments(byte[] key);
        byte[]? Encrypt(byte[] plaintext, out byte[] nonce, out byte[]? tag);

        byte[]? Decrypt(byte[] encryptedData, byte[] nonce,byte[]? tag = null);

        byte[]? GetBytes(byte[] iv, int length);
    }
}
