using Microsoft.Extensions.DependencyInjection;
using SuperPassword.Config.Service;
using SuperPassword.DAL.Implementations.Offline.DataContext;
using SuperPassword.DAL.Interfaces.Offline;

namespace SuperPassword.DAL.Implementations.Offline
{
    public partial class OfflineService : IOfflineService
    {
        private IConfigService _configService;
        private InfoGroupDbContext _infoGroupDbContext;
        private IServiceProvider _serviceProvider;
        public OfflineService(IConfigService configService, IServiceProvider serviceProvider)
        {
            _configService = configService;
            _serviceProvider = serviceProvider;
        }
        public static void AddService(ServiceCollection container)
        {
            container.AddTransient((sp) =>
            {
                var configService = sp.GetService<IConfigService>()!;
                return new InfoGroupDbContext(configService.UserConfig.CombineUserPath("data.db"));
            });
        }
        public async Task UpdateInfoGroupDbContextAsync()
        {
            _infoGroupDbContext = _serviceProvider.GetService<InfoGroupDbContext>()!;
            await _infoGroupDbContext.Database.EnsureCreatedAsync();
        }
    }
}
