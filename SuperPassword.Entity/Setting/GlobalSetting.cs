using SuperPassword.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperPassword.Entity.Setting
{
    public class GlobalSetting
    {
        private string _apiUrl;

        public string ApiUrl
        {
            get { return _apiUrl; }
            set { _apiUrl = value; }
        }

        private GlobalSetting()
        {
            ApiUrl = @"https://s.oragne.top/";
        }
    }
}
