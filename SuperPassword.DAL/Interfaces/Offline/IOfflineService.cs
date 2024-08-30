using SuperPassword.DAL.Implementations.Offline.DataContext;

namespace SuperPassword.DAL.Interfaces.Offline
{
    public partial interface IOfflineService
    {
        Task UpdateInfoGroupDbContextAsync();
    }
}
