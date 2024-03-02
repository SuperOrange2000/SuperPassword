using SuperPassword.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperPassword.Entity.Setting
{
    public class UserSetting
    {
        private static readonly UserSetting _instance = new UserSetting();

        private CipherTypes _cipherType;

        public CipherTypes CipherType
        {
            get { return _cipherType; }
            set { _cipherType = value; }
        }

        private UserSetting()
        {
            CipherType = CipherTypes.ChaCha20;
        }

        public static UserSetting Instance
        {
            get { return _instance; }
        }
    }
}
