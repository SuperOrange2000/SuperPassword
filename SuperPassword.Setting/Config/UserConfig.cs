using CommunityToolkit.Mvvm.ComponentModel;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace SuperPassword.Config.Config
{
    [Serializable]
    public partial class UserConfig : ConfigBase
    {
        [ObservableProperty] private uint _localId = 0;

        [ObservableProperty] private byte[] _salt;

        protected override void Deserialize(JsonElement rootElement)
        {
            JsonElement tempElement;
            if(rootElement.TryGetProperty("LocalId", out tempElement))
                LocalId = tempElement.GetUInt32();
            if (rootElement.TryGetProperty("Salt", out tempElement))
                Salt = tempElement.GetBytesFromBase64();
            else
            {
                Salt = new byte[32];
                var random = new Random();
                random.NextBytes(Salt);
            }
        }

        protected override string Serialize()
        {
            return JsonSerializer.Serialize(this, serializer_options);
        }

        public UserConfig(uint localId) : base("User", localId+".json")
        {
            LocalId = localId;

            Read();

            PropertyChanged += (s, e) => OnAnyPropertyChanged();
        }

        public UserConfig() : base("User", "0.json")
        {
            LocalId = 0;

            Read();

            PropertyChanged += (s, e) => OnAnyPropertyChanged();
        }
    }
}
