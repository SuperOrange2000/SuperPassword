using SuperPassword.DAL.Interfaces.Models;
using SuperPassword.Entity.Interface;

namespace SuperPassword.DAL.Interfaces.Online
{
    public interface IOnlineService
    {
        Task<IOnlineResponse> AddAsync(string username, IDALInfoGroup newInfoGroup);
        Task<IOnlineResponse> DeleteAsync(string username, Guid id);
        Task<IOnlineResponse<IList<IDALInfoGroup>>> GetAllAsync(string username);
        Task<IOnlineResponse<IDALInfoGroup>> GetFirstOfDefaultAsync(string username, Guid id);
        Task<IOnlineResponse> LoginAsync(IUser user);
        Task<IOnlineResponse> SignUpAsync(IUser user);
        Task<IOnlineResponse> UpdateAsync(string username, IDALInfoGroup infoGroup);
    }
}
