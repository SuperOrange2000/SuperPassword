using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SuperPassword.Config.Config
{
    public interface IConfig
    {
        [JsonIgnore] public Action? OnAnyChangedAction { get; set; }
    }
}
