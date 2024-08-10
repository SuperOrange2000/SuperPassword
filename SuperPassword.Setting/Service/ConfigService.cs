using SuperPassword.Config.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SuperPassword.Config.Service
{
    public class ConfigService : IConfigService
    {
        public AppConfig AppConfig { get; init; }
        public UserConfig UserConfig { get; set; }

        public ConfigService()
        {
            AppConfig = Read<AppConfig>(Path.Combine(ConfigBase.StartUpPath, "config", "config.json"));
            AppConfig.PropertyChanged += (s, e) => Write(s as AppConfig);

            if (AppConfig.NameMap.Count == 0)
            {
                AppConfig.NameMap.Add(0, null);
                UserConfig = new UserConfig();
            }
            else
                SwitchUser(AppConfig.NameMap.FirstOrDefault().Key);
        }

        public void SwitchUser(uint userId)
        {
            UserConfig = Read<UserConfig>(Path.Combine(ConfigBase.StartUpPath, "config", $"{AppConfig.NameMap[userId]}.json"));
            UserConfig.PropertyChanged += (s, e) => Write(s as UserConfig);
        }

        private readonly ReaderWriterLockSlim _rwLock = new();

        public T Read<T>(string filePath) where T : ConfigBase, new()
        {
            if (File.Exists(filePath))
            {
                _rwLock.EnterReadLock();
                JsonDocument jsonDocument;
                try
                {

                    var jsonText = File.ReadAllText(filePath);
                    jsonDocument = JsonDocument.Parse(jsonText);
                }
                finally
                {
                    _rwLock.ExitReadLock();
                }
                return JsonSerializer.Deserialize<T>(jsonDocument)!;
            }
            else
            {
                var newConfig = new T();
                Write(newConfig);
                return newConfig;
            }

        }

        public void Write<T>(T? config) where T : ConfigBase
        {
            if (config == null) return;

            _rwLock.EnterWriteLock();
            try
            {
                //var dirPath = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(config.DirName))
                {
                    Directory.CreateDirectory(config.DirName);
                }
                File.WriteAllText(Path.Combine(config.DirName, config.FileName), JsonSerializer.Serialize(config, serializer_options));
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

        protected readonly JsonSerializerOptions serializer_options = new()
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            //PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
            AllowTrailingCommas = true
        };
    }
}
