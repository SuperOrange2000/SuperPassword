using SuperPassword.Config.Models;
using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SuperPassword.Config.Service
{
    public class ConfigService : IConfigService
    {
        public AppConfig AppConfig { get; init; }
        public UserConfig UserConfig { get; set; }
        public UserProperties UserProperties { get; set; }

        public ConfigService()
        {
            var config = Read<AppConfig>(Path.Combine(ConfigBase.DataPath, "config.json"));
            if (config == null)
            {
                AppConfig = new AppConfig();
                Write(AppConfig);
            }
            else
            {
                AppConfig = config;
                AppConfig.AutoSaveBind();
            }
            AppConfig.PropertyChanged += (s, e) => Write(s as AppConfig);
        }

        public void SwitchUser(string name, Guid? userId = null)
        {
            if (UserConfig != null) { UserConfig.PropertyChanged -= OnPropertyChanged<UserConfig>; }
            if (UserProperties != null) { UserProperties.PropertyChanged -= OnPropertyChanged<UserProperties>; }

            if (!AppConfig.UsernameMap.ContainsKey(name) && userId == null)
                throw new Exception(nameof(userId));
            if (AppConfig.UsernameMap.ContainsKey(name))
            {
                UserConfig = Read<UserConfig>(Path.Combine(AppConfig.DataPath, AppConfig.UsernameMap[name].ToString(), "config.json")) ??
                    new() { Name = name, Id = (Guid)userId! };
                UserProperties = Read<UserProperties>(Path.Combine(AppConfig.DataPath, AppConfig.UsernameMap[name].ToString(), "properties")) ??
                    new() { Id = (Guid)userId! };
            }
            else
            {
                UserConfig = new() { Name = name, Id = (Guid)userId! };
                UserProperties = new() { Id = (Guid)userId! };
            }
        }

        public void MountSaveFunction()
        {
            UserConfig.PropertyChanged += (s, e) => Write(s as UserConfig);
            Write(UserConfig);
            UserProperties.PropertyChanged += (s, e) => Write(s as UserProperties);
            Write(UserProperties);
            if (!AppConfig.UsernameMap.ContainsKey(UserConfig.Name))
                AppConfig.UsernameMap.Add(UserConfig.Name, UserConfig.Id);
        }

        private void OnPropertyChanged<T>(object? sender, PropertyChangedEventArgs e) where T : ConfigBase
        => Write(sender as T);


        private readonly ReaderWriterLockSlim _rwLock = new();

        public T? Read<T>(string filePath) where T : ConfigBase, new()
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
                return JsonSerializer.Deserialize<T>(jsonDocument);
            }
            else
            {
                return null;
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

        private readonly JsonSerializerOptions serializer_options = new()
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            //PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
            AllowTrailingCommas = true
        };

        private List<Guid> GetGuidFolders(string path)
        {
            List<Guid> guidList = new List<Guid>();
            try
            {
                string[] directories = Directory.GetDirectories(path);
                foreach (string dir in directories)
                {
                    // 获取当前文件夹的名称
                    string folderName = Path.GetFileName(dir);
                    // 尝试将文件夹名称转换为Guid
                    if (Guid.TryParse(folderName, out Guid guid))
                    {
                        guidList.Add(guid);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            return guidList;
        }
    }
}
