using CommunityToolkit.Mvvm.ComponentModel;
using SuperPassword.Commom.ObjectModel;
using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SuperPassword.Config.Config
{
    [Serializable]
    public partial class GlobalConfig : ConfigBase, IConfig
    {

        [ObservableProperty] private string _apiUrl = string.Empty;

        [ObservableProperty] private uint _maxLocalId = 10000;

        [ObservableProperty] private ObservableDictionary<uint, string?> _nameMap = new();

        [JsonIgnore] public uint ActiveId { get; set; }

        public GlobalConfig() : base(string.Empty, "global.json")
        {
            _nameMap.PropertyChanged += (shis, e) => OnPropertyChanged(e);
            PropertyChanged += (s, e) => OnAnyPropertyChanged();
        }

        protected override void Deserialize(JsonElement rootElement)
        {
            JsonElement tempElement;
            string? nullableString;
            if (rootElement.TryGetProperty("ApiUrl", out tempElement))
            {
                nullableString = tempElement.GetString();
                if (nullableString != null)
                {
                    ApiUrl = nullableString;
                }
            }
            if (rootElement.TryGetProperty("MaxLocalId", out tempElement))
                MaxLocalId = tempElement.GetUInt32();
            if (rootElement.TryGetProperty("NameMap", out tempElement))
                foreach (var property in tempElement.EnumerateObject())
                {
                    // 获取属性名和对应的 JsonElement
                    string propertyName = property.Name;
                    JsonElement propertyValue = property.Value;
                }
        }

        protected override string Serialize()
        {
            return JsonSerializer.Serialize(this, serializer_options);
        }
    }
}
