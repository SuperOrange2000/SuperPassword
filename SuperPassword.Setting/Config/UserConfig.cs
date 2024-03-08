using CommunityToolkit.Mvvm.ComponentModel;
using System.Text.Json.Serialization;

namespace SuperPassword.Config.Config
{
    [Serializable]
    public partial class UserConfig : ObservableObject, IConfig
    {
        [ObservableProperty] private uint _localId ;

        [ObservableProperty] private byte[] _salt;

        [JsonIgnore] public string DirName => "User";

        [JsonIgnore] public string FileName => LocalId + ".json";
        
        public UserConfig()
        {
            Salt = new byte[32];
            var random = new Random();
            random.NextBytes(Salt);
        }
    }
}
