using CommunityToolkit.Mvvm.ComponentModel;

namespace SuperPassword.Config.Models
{
    abstract public class ConfigBase : ObservableObject, IConfig
    {

        public static readonly string DocumentPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "SuperPassword"
        );

        public static readonly string ConfigPath = Path.Combine(
            DocumentPath, "Config"
        );

        public static readonly string StartUpPath = AppContext.BaseDirectory;

        public static string AppPath = CombineAppPath("SuperPassword.exe");

        public abstract string DirName { get; }
        public abstract string FileName { get; }

        public ConfigBase()
        {

        }

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
