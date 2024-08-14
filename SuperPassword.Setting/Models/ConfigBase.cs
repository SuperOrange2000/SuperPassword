using CommunityToolkit.Mvvm.ComponentModel;

namespace SuperPassword.Config.Models
{
    abstract public class ConfigBase : ObservableObject, IConfig
    {
        public static string StartUpPath { get; } = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        public static string DataPath { get; } = Path.Combine(StartUpPath, "SuperPassword");

        public abstract string DirName { get; }
        public abstract string FileName { get; }

        public static string CombineDataPath(string relativePath)
        {
            return Path.Combine(DataPath, relativePath);
        }
    }
}
