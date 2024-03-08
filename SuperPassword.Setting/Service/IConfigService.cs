using SuperPassword.Config.Config;

namespace SuperPassword.Config.Service
{
    public interface IConfigService
    {
        UserConfig GetUerConfig(uint localId);
        GlobalConfig GlobalConfig {  get; }
        DefaultConfig DefaultConfig { get; }
        T Read<T>(string path) where T : IConfig, new();
        void Write(IConfig config);
    }
}
