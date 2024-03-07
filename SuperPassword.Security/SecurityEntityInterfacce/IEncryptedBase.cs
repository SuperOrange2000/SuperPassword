namespace SuperPassword.Security.SecurityEntityInterface
{
    public interface IEncryptedBase
    {
        static Func<byte[], byte[], byte[]> EncryptionHandler { get; set; }
        static Func<byte[], byte[], byte[]> DecryptionHandler { get; set; }
    }
}
