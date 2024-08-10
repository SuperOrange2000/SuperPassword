using CommunityToolkit.Mvvm.ComponentModel;
using System.Text.Json.Serialization;

namespace SuperPassword.Config.Models
{
    [Serializable]
    public partial class UserConfig : ConfigBase
    {
        [ObservableProperty] private uint localId = 0;

        [ObservableProperty] private byte[] salt;

        [ObservableProperty] private string name;

        [JsonIgnore] public override string DirName => CombineAppPath("config");
        [JsonIgnore] public override string FileName => $"{LocalId}.json";

        public UserConfig()
        {
        }
    }
}
