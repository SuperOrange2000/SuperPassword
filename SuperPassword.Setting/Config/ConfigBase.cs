using CommunityToolkit.Mvvm.ComponentModel;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace SuperPassword.Config.Config
{
    abstract public class ConfigBase : ObservableObject, IConfig
    {
        [JsonIgnore] private readonly ReaderWriterLockSlim _rwLock = new();

        [JsonIgnore] public string DirName { get; init; }

        [JsonIgnore] public string FileName { get; init; }

        protected readonly JsonSerializerOptions serializer_options = new()
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            //PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
            AllowTrailingCommas = true
        };

        private readonly JsonDocumentOptions doc_options = new()
        {
            AllowTrailingCommas = true
        };

        public ConfigBase(string dir, string file)
        {
            DirName = dir;
            FileName = file;
        }

        public void Read()
        {
            _rwLock.EnterReadLock();
            JsonDocument jsonDocument;
            try
            {
                var filePath = DefaultConfig.CombineConfigPath($"{DirName}/{FileName}");
                if (File.Exists(filePath))
                {
                    var jsonText = File.ReadAllText(filePath);
                    jsonDocument = JsonDocument.Parse(jsonText);
                }
                else
                    jsonDocument = JsonDocument.Parse(@"{}", doc_options);
            }
            finally
            {
                _rwLock.ExitReadLock();
            }
            Deserialize(jsonDocument.RootElement);
            Write();
        }

        public void Write()
        {
            _rwLock.EnterWriteLock();
            try
            {
                var path = DefaultConfig.CombineConfigPath(DirName);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                var file = Path.Combine(path, FileName);
                File.WriteAllText(file, Serialize());
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

        protected abstract void Deserialize(JsonElement rootElement);
        protected abstract string Serialize();

        protected void OnAnyPropertyChanged()
        {
            Write();
        }
    }
}
