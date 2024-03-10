using SuperPassword.Config.Config;

namespace SuperPassword.Config.Service
{
    public class ConfigService : IConfigService
    {
        public GlobalConfig GlobalConfig { get; } = new();

        public UserConfigService UserConfig { get; } = new();

        public DefaultConfig DefaultConfig { get; } = new();


    }

    public class UserConfigService
    {
        public UserConfig this[uint key]
        {
            get => GetUerConfig(key);
        }

        public Dictionary<uint, UserConfig> _userConfigDictionary { get; } = new();

        public UserConfig GetUerConfig(uint localId)
        {
            if (_userConfigDictionary.TryGetValue(localId, out UserConfig? instance))
            {
                return instance;
            }
            else
            {
                UserConfig newConfig = new(localId);
                newConfig.Read();
                _userConfigDictionary.Add(localId, newConfig);
                return newConfig;
            }
        }
    }
}
