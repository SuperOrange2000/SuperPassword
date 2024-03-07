using SuperPassword.Config.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperPassword.Config.Service
{
    public interface IConfigService
    {
        T Get<T>() where T : IConfig, new();

        T Read<T>() where T : IConfig, new();

        void Write<T>() where T : IConfig, new();
    }
}
