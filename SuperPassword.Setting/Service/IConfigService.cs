using SuperPassword.Config.Config;

namespace SuperPassword.Config.Service
{
    public interface IConfigService
    {
        UserConfigService UserConfig {  get; }
        GlobalConfig GlobalConfig {  get; }
        DefaultConfig DefaultConfig { get; }
    }
}
