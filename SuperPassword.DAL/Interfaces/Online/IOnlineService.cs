using SuperPassword.DAL.Models;

namespace SuperPassword.DAL.Interfaces.Online
{


    public interface IOnlineService
    {
        Task<OnlineResponse> AddAsync(string username, DALInfoGroup newInfoGroup);
        Task<OnlineResponse> DeleteAsync(string username, Guid id);
        Task<OnlineResponse<List<DALInfoGroup>>> GetAllAsync(string username);
        Task<OnlineResponse<DALInfoGroup>> GetFirstOfDefaultAsync(string username, Guid id);
        Task<OnlineResponse<LoginResult>> LoginAsync(long id, byte[] password);
        Task<OnlineResponse<LoginResult>> SignUpAsync(string name, byte[] password);
        Task<OnlineResponse> UpdateAsync(string username, DALInfoGroup infoGroup);
    }
}
