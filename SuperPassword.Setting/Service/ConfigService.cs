using SuperPassword.Config.Config;
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

    private Dictionary<Type, IConfig> _configDictionary = new Dictionary<Type, IConfig>();

    public T Get<T>() where T : IConfig, new()
    {
        if (_configDictionary.TryGetValue(typeof(T), out IConfig instance))
        {
            return (T)instance;
        }
        else
        {
            T newConfig = Read<T>();
            _configDictionary.Add(typeof(T), newConfig);
            return newConfig;
        }
    }

    public T Read<T>() where T : IConfig, new()
    {
        _rwLock.EnterReadLock();
        try
        {
            var filePath = DefaultConfig.Absolute(@"User/config.json");
            if (!File.Exists(filePath))
            {
                return new T();
            }

            var json = File.ReadAllText(filePath);
            var config = JsonSerializer.Deserialize<T>(json, _options);
            if (config == null)
            {
                return new T();
            }

            return config;
        }
        finally
        {
            _rwLock.ExitReadLock();
        }
    }

    public void Write<T>() where T : IConfig, new()
    {
        _rwLock.EnterWriteLock();
        try
        {
            var path = DefaultConfig.Absolute("User");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var file = Path.Combine(path, "config.json");
            File.WriteAllText(file, JsonSerializer.Serialize(Get<T>(), _options));
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
}