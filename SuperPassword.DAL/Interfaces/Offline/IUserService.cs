using SuperPassword.DAL.Models;
using SuperPassword.Entity.Interface;

namespace SuperPassword.DAL.Interfaces.Offline
{
    public partial interface IOfflineService
    {
        Task<OfflineResponse<byte[]>> LoginAsync(IUser user);

        Task<OfflineResponse<byte[]>> SignUpAsync(IUser user);
    }
}
