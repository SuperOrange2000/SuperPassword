using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperPassword.Security
{
    public interface ISecurityModule
    {
        public byte[] Encrypt(byte[] plaintext, byte[] nonce);

        public byte[] Decrypt(byte[] encryptedData, byte[] nonce);

        public byte[] GetBytes(byte[] iv, int length);
    }
}
