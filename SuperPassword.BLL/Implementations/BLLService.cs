using Microsoft.Extensions.DependencyInjection;
using SuperPassword.BLL.Interfaces;
using SuperPassword.BLL.Models;
using SuperPassword.Config.Service;
using SuperPassword.DAL.Implementations.Offline;
using SuperPassword.DAL.Implementations.Online;
using SuperPassword.DAL.Interfaces.Offline;
using SuperPassword.DAL.Interfaces.Online;
using SuperPassword.Security.Service;

namespace SuperPassword.BLL.Implementations
{
    public partial class BLLService : IBLLService
    {
        private StorageMode storageMode = StorageMode.LocalOnly;

        private IOfflineService offlineService;
        private IOnlineService onlineService;

        private ISecurityService securityService;
        private IServiceProvider serviceProvider;
        private IConfigService configService;
        private BLLUser activeUser;

        private bool IsOnline => (storageMode & StorageMode.ServerOnly) != 0;
        private bool IsOffline => (storageMode & StorageMode.LocalOnly) != 0;

        //private byte[] internalPassword;
        public BLLService(IServiceProvider serviceProvider, ISecurityService securityService, IConfigService configService)
        {
            this.securityService = securityService;
            this.serviceProvider = serviceProvider;
            this.configService = configService;
        }
        public static void AddService(ServiceCollection container)
        {
            container.AddSingleton<IOnlineService, OnlineService>();
            container.AddSingleton<IOfflineService, OfflineService>();
            container.AddSingleton<ISecurityService, SecurityService>();

            OnlineService.AddService(container);
            OfflineService.AddService(container);
            SecurityService.AddService(container);
        }

        public async Task UpdateStorageModeAsync(StorageMode mode)
        {
            storageMode = mode;
            if (IsOffline)
            {
                offlineService = await Task.Run(() => serviceProvider.GetService<IOfflineService>()!);
            }
            if (IsOnline)
            {
                onlineService = await Task.Run(() => serviceProvider.GetService<IOnlineService>()!);
            }
        }

        internal BLLUser ActiveUser { get => activeUser; }
    }
}
