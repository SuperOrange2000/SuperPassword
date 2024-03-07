using SuperPassword.Config.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SuperPassword.Entity.Config
{
    public class GlobalConfig: IConfig
    {
        [JsonIgnore] public Action? OnAnyChangedAction { get; set; }

        private string _apiUrl = @"https://s.oragne.top/";
    }
}
