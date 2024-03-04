using System;
using System.Security;
using System.Security.Cryptography;
using SuperPassword.Entity.Data;
using SuperPassword.Security;

namespace SuperPassword.Entity
{
    public class EncryptedBase
    {
        internal Func<byte[], byte[], byte[]> EncryptionHandler;
        internal Func<byte[], byte[], byte[]> DecryptionHandler;

        private byte[] _salt;

        public byte[] Salt
        {
            get { return _salt; }
            set { _salt = value; }
        }

        private uint _id;
        public uint ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private ISecurityModule _cipher;

        public EncryptedBase(CipherTypes type)
        {
            SwitchCipher(type);
        }

        internal byte[] GetNonce(byte i)
        {
            SHA256 sha256 = SHA256.Create();
            byte[] hashBytes = sha256.ComputeHash(Salt.Concat(new byte[] { i }).ToArray());
            byte[] first8Bytes = new byte[8];
            Array.Copy(hashBytes, 0, first8Bytes, 0, 8);
            return first8Bytes;
        }

        internal void SwitchCipher(CipherTypes type)
        {
            if (EncryptionHandler != null) EncryptionHandler = null;
            if (DecryptionHandler != null) DecryptionHandler = null;

            switch (type)
            {
                case CipherTypes.ChaCha20:
                    _cipher = new ChaCha20(GlobalEntity.ActiveUsser.Key);
                    EncryptionHandler += _cipher.Encrypt;
                    DecryptionHandler += _cipher.Decrypt;
                    break;
            }
        }
    }
}
