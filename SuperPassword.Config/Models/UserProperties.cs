using CommunityToolkit.Mvvm.ComponentModel;
using System.Security.Cryptography;
using System.Text.Json.Serialization;

namespace SuperPassword.Config.Models
{
    public partial class UserProperties : ConfigBase
    {
        [JsonIgnore][ObservableProperty] private Guid _id;
        [ObservableProperty] private byte[] salt = new byte[8];
        [ObservableProperty] private byte[] encryptedPassword;
        [ObservableProperty] private byte[] verificationCode;
        [JsonIgnore] public override string DirName { get => CombineDataPath($"{Id}"); }
        [JsonIgnore] public override string FileName => $"properties";

        public UserProperties()
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(Salt);
            }
        }
    }
}
