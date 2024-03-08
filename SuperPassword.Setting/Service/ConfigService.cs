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
                _globalConfig = Read<GlobalConfig>("global.json");
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
            UserConfig newConfig = Read<UserConfig>($"User/{localId}.json");
            _userConfigDictionary.Add(localId, newConfig);
            Write(newConfig);
            return newConfig;
        }
    }

    public T Read<T>(string path) where T : IConfig, new()
    {
        _rwLock.EnterReadLock();
        try
        {
            var filePath = DefaultConfig.CombineConfigPath(path);
            T? config;
            if (!File.Exists(filePath))
            {
                config = new T();
            }
            else
            {
                var json = File.ReadAllText(filePath);
                config = JsonSerializer.Deserialize<T>(json, _options);
            }

            if (config == null)
            {
                config = new T();
            }
            config.PropertyChanged += OnAnyPropertyChanged;
            return config;
        }
        finally
        {
            _rwLock.ExitReadLock();
        }
    }

    public void Write(IConfig config)
    {
        _rwLock.EnterWriteLock();
        try
        {
            var path = DefaultConfig.CombineConfigPath(config.DirName);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var file = Path.Combine(path, config.FileName);
            File.WriteAllText(file, JsonSerializer.Serialize(config, _options));
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine(e.StackTrace);
        }
        finally
        {
            _rwLock.ExitWriteLock();
        }
    }

    private void OnAnyPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        var config = sender as IConfig;
        if (config != null)
        {
            Write(config);
        }
    }
}