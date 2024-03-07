using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperPassword.Config.Config
{
    public class DefaultConfig : IConfig
    {
        public static readonly string Version = "0.0.1";

        public static readonly string StartUpPath = AppContext.BaseDirectory;

        public static string AppPath = Absolute("SuperPassword.exe");

        public Action? OnAnyChangedAction {  get; set; }

        public static string Absolute(string relativePath)
        {
            return Path.Combine(StartUpPath, relativePath);
        }
    }
}
