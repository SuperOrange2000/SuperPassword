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
            set { userName = value;}
        }

        private string account;

        public string Account
        {
            get { return account; }
            set { account = value;}
        }

        private string password;

        public string Password
        {
            get { return password; }
            set 
            {
                SHA256 sha256 = SHA256.Create();
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(value));
                Array.Copy(hashBytes, 0, _key, 0, 32);
                password = value;
            }
        }

        private byte[] _key = new byte[32];

        public byte[] Key
        {
            get { return _key; }
            set { _key = value; }
        }


        private string _token;

        public string Token
        {
            get { return _token; }
            set { _token = value; }
        }
    }
}
