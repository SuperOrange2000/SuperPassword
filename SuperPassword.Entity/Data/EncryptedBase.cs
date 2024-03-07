using SuperPassword.Security.SecurityEntityInterface;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;

namespace SuperPassword.Entity
{
    public class EncryptedBase : IEncryptedBase
    {
        protected Action OnSaltChanged;
        private byte[] _salt;
        [JsonPropertyName("salt")]
        public byte[] Salt
        {
            get { return _salt; }
            set { _salt = value; OnSaltChanged?.Invoke(); }
        }

        private uint _id;
        public uint ID
        {
            get { return _id; }
            set { _id = value; }
        }

        internal byte[] GetNonce(byte i)
        {
            SHA256 sha256 = SHA256.Create();
            byte[] hashBytes = sha256.ComputeHash(Salt.Concat(new byte[] { i }).ToArray());
            byte[] first8Bytes = new byte[8];
            Array.Copy(hashBytes, 0, first8Bytes, 0, 8);
            return first8Bytes;
        }

        internal string? Get(byte[] data, byte nonceID)
        {
            if (data == null) return null;
            byte[]? plaintext = IEncryptedBase.DecryptionHandler?.Invoke(data, GetNonce(nonceID));
            if (plaintext == null)
                return null;
            return Encoding.UTF8.GetString(plaintext);
        }

        internal void Set(string? value, ref byte[] data, byte nonceID)
        {
            if (value == null) return;
            byte[] plaintext = Encoding.UTF8.GetBytes(value);
            var encryptedData = IEncryptedBase.EncryptionHandler?.Invoke(plaintext, GetNonce(nonceID));
            if (encryptedData == null) return;
            else data = encryptedData;
        }

        public EncryptedBase()
        {

        }
    }
}
