using SuperPassword.DAL.Implementations.Offline.DataContext;

namespace SuperPassword.DAL.Interfaces
{
    internal interface IOfflineService : IInternalService
    {
        void UpdateInfoGroupDbContext(InfoGroupDbContext infoGroupDbContext);
    }
}
