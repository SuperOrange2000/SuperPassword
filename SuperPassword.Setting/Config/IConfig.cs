using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SuperPassword.Config.Config
{
    public interface IConfig : INotifyPropertyChanged, INotifyPropertyChanging
    {
        string DirName { get; init; }

        string FileName { get; init; }
    }
}
