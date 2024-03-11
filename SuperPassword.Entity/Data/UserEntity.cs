using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;

namespace SuperPassword.Entity
{
    public class UserEntity
    {
        private string userName;

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        private string account;

        public string Account
        {
            get { return account; }
            set { account = value; }
        }

        private byte[] password = new byte[32];

        public string Password
        {
            get
            {
                SHA256 sha256 = SHA256.Create();
                byte[] hashBytes = sha256.ComputeHash(password.Concat(Salt).Concat(new byte[] { 0x01 }).ToArray());
                return Convert.ToBase64String(hashBytes);
            }
            set 
            {
                SHA256 sha256 = SHA256.Create();
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(value).Concat(Salt).ToArray());
                Array.Copy(hashBytes, 0, password, 0, 32);
            }
        }
        public byte[] Key
        {
            get
            {
                SHA256 sha256 = SHA256.Create();
                return sha256.ComputeHash(password.Concat(Salt).Concat(new byte[] {0x02}).ToArray());
            }
        }

        private byte[] _salt;

        public byte[] Salt
        {
            get { return _salt; }
            set { _salt = value; }
        }


        private string _token;

        public string Token
        {
            get { return _token; }
            set { _token = value; }
        }
    }
}
