using CommunityToolkit.Mvvm.ComponentModel;
using System.Text.Json.Serialization;

namespace SuperPassword.Config.Models
{
    [Serializable]
    public partial class UserConfig : ConfigBase
    {
        [ObservableProperty] private Guid _id = Guid.NewGuid();

        [ObservableProperty] private string name;

        [JsonIgnore] public override string DirName { get => CombineDataPath($"{Id}"); }
        [JsonIgnore] public override string FileName => $"config.json";

        public string CombineUserPath(string relativePath)
        {
            return Path.Combine(DataPath, DirName, relativePath);
        }
    }
}
