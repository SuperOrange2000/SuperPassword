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

        private UserSetting()
        {
        }

        public static UserSetting Instance
        {
            get { return _instance; }
        }
    }
}
