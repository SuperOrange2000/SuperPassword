using SuperPassword.Security;
using SuperPassword.Config.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperPassword.Entity.Config
{
    [Serializable]
    public class UserConfig : IConfig
    {
        public Action? OnAnyChangedAction {  get; set; }
    }
}
