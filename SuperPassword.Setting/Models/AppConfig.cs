using CommunityToolkit.Mvvm.ComponentModel;
using Mapster;
using SuperPassword.Commom.ObjectModel;
using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SuperPassword.Config.Models
{
    [Serializable]
    public partial class AppConfig : ConfigBase
    {

        [ObservableProperty] private string apiUrl = string.Empty;

        [ObservableProperty] private ObservableDictionary<uint, string?> nameMap = new();

        [JsonIgnore] public  uint MaxLocalId = 10000;

        [JsonIgnore] public static readonly string Version = "0.0.1";
        [JsonIgnore] public override string DirName => CombineAppPath("config");
        [JsonIgnore] public override string FileName => "config.json";
        [JsonIgnore] public uint ActiveId { get; set; }

        public AppConfig()
        {
            nameMap.PropertyChanged += (shis, e) => OnPropertyChanged(e);
        }
    }
}
