using SuperPassword.Config.Service;
using SuperPassword.DAL.Implementations.Offline.DataContext;
using SuperPassword.DAL.Interfaces;

namespace SuperPassword.DAL.Implementations.Offline
{
    internal partial class OfflineService : IOfflineService
    {
        private IConfigService _configService;
        private InfoGroupDbContext _infoGroupDbContext;
        public OfflineService(IConfigService configService)
        {
            _configService = configService;
        }

        public void UpdateInfoGroupDbContext(InfoGroupDbContext infoGroupDbContext)
        {
            _infoGroupDbContext = infoGroupDbContext;
            _infoGroupDbContext.Database.EnsureCreated();
        }
    }
}
