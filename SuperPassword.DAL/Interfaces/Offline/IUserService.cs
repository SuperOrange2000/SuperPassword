using SuperPassword.Entity.Interface;

namespace SuperPassword.DAL.Interfaces.Offline
{
    public partial interface IOfflineService
    {
        Task<IOfflineResponse<byte[]>> LoginAsync(IUser user);

        Task<IOfflineResponse<byte[]>> SignUpAsync(IUser user);
    }
}
