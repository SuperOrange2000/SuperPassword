using CommunityToolkit.Mvvm.ComponentModel;
using System.Text.Json.Serialization;

namespace SuperPassword.Config.Config
{
    public class DefaultConfig : ObservableObject, IConfig
    {
        public static readonly string Version = "0.0.1";

        public static readonly string DocumentPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "SuperPassword"
        );

        public static readonly string ConfigPath = Path.Combine(
            DocumentPath, "Config"
        );

        public static readonly string StartUpPath = AppContext.BaseDirectory;

        public static string AppPath = CombineAppPath("SuperPassword.exe");

        [JsonIgnore] public Action? OnAnyChangedAction { get; set; }

        [JsonIgnore] public string DirName => string.Empty;

        [JsonIgnore] public string FileName => string.Empty;

        public static string CombineAppPath(string relativePath)
        {
            return Path.Combine(StartUpPath, relativePath);
        }
        public static string CombineDocPath(string relativePath)
        {
            return Path.Combine(DocumentPath, relativePath);
        }
        public static string CombineConfigPath(string relativePath)
        {
            return Path.Combine(ConfigPath, relativePath);
        }
    }
}
