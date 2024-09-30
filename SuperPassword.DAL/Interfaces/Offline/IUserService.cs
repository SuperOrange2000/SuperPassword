using SuperPassword.DAL.Models;
using SuperPassword.Entity.Interface;

namespace SuperPassword.DAL.Interfaces.Offline
{
    public partial interface IOfflineService
    {
        Task<OfflineResponse<byte[]>> LoginAsync(string name, byte[] spwd);

        Task<OfflineResponse<byte[]>> SignUpAsync(string name, byte[] spwd);
    }
}
