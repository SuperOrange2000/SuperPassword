using SuperPassword.Config.Config;
using System.ComponentModel;
using System.Text.Json;

namespace SuperPassword.Config.Service;

public class ConfigService : IConfigService
{
    private readonly ReaderWriterLockSlim _rwLock = new();

    private readonly JsonSerializerOptions _options = new()
    {
        NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowNamedFloatingPointLiterals,
        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true,
        AllowTrailingCommas = true
    };

    private GlobalConfig _globalConfig;

    public GlobalConfig GlobalConfig 
    { 
        get 
        {
            if (_globalConfig == null)
            {
                _globalConfig = new();
            }
            return _globalConfig;
        }
    }

    public Dictionary<uint, UserConfig> _userConfigDictionary { get; } = new();

    public DefaultConfig DefaultConfig { get; } = new DefaultConfig();

    public UserConfig GetUerConfig(uint localId)
    {
        if (_userConfigDictionary.TryGetValue(localId, out UserConfig? instance))
        {
            return instance;
        }
        else
        {
            UserConfig newConfig = new();
            newConfig.Read();
            _userConfigDictionary.Add(localId, newConfig);
            return newConfig;
        }
    }
}