using SuperPassword.Config.Config;

namespace SuperPassword.Config.Service
{
    public interface IConfigService
    {
        UserConfig GetUerConfig(uint localId);
        GlobalConfig GlobalConfig {  get; }
        DefaultConfig DefaultConfig { get; }
    }
}
