using SuperPassword.Config.Models;

namespace SuperPassword.Config.Service
{
    public interface IConfigService
    {
        AppConfig AppConfig {  get; }
        UserConfig UserConfig {  get; }
        UserProperties UserProperties { get; }
        void SwitchUser(string name, Guid? userId = null);
        void MountSaveFunction();
    }
}
