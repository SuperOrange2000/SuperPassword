using CommunityToolkit.Mvvm.ComponentModel;
using SuperPassword.Commom.ObjectModel;
using System.Text.Json.Serialization;

namespace SuperPassword.Config.Models
{
    [Serializable]
    public partial class AppConfig : ConfigBase
    {
        [ObservableProperty] private string apiUrl = string.Empty;

        [ObservableProperty] private ObservableDictionary<string, Guid> usernameMap = new();

        [ObservableProperty] private byte storageMode = 0b001;

        [JsonIgnore] public uint MaxLocalId = 10000;

        [JsonIgnore] public static readonly string Version = "0.0.1";
        [JsonIgnore] public override string DirName { get; } = DataPath;
        [JsonIgnore] public override string FileName { get; } = "config.json";
        [JsonIgnore] public uint ActiveId { get; set; }
        [JsonIgnore] public string GetDataPath { get; } = DataPath;
        [JsonIgnore] public int EncryptInterations { get; } = 10042;
        [JsonIgnore] public int PasswordLength { get; } = 32;

        public AppConfig()
        {
            AutoSaveBind();
        }

        public void AutoSaveBind()
        {
            UsernameMap.PropertyChanged += (s, e) => OnPropertyChanged(e);
        }

        public string CombineAppPath(string relativePath)
        {
            return Path.Combine(DirName, relativePath);
        }
    }
}
