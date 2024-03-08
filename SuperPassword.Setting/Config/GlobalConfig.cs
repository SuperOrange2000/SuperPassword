using CommunityToolkit.Mvvm.ComponentModel;
using SuperPassword.Commom.ObjectModel;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace SuperPassword.Config.Config
{
    [Serializable]
    public partial class GlobalConfig : ObservableObject, IConfig
    {

        [ObservableProperty] private string _apiUrl = @"https://s.oragne.top/";

        [ObservableProperty] private uint _maxLocalId = 10000;

        [ObservableProperty] private ObservableDictionary<uint, string?> _nameMap = new();

        [JsonIgnore] public uint ActiveId { get; set; }

        [JsonIgnore] public string DirName => string.Empty;

        [JsonIgnore] public string FileName => "global.json";

        public GlobalConfig()
        {
            _nameMap.PropertyChanged += (shis, e) => OnPropertyChanged(e);
        }
    }
}
