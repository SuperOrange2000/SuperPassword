using SuperPassword.DAL.Interfaces.Models;
using SuperPassword.Entity.Interface;
using System.Text.Json.Serialization;

namespace SuperPassword.DAL.Interfaces.Online
{
    public class LoginResponse
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }
        [JsonPropertyName("id")]
        public long Id { get; set; }
    }

    public interface IOnlineService
    {
        Task<IOnlineResponse> AddAsync(string username, IDALInfoGroup newInfoGroup);
        Task<IOnlineResponse> DeleteAsync(string username, Guid id);
        Task<IOnlineResponse<IList<IDALInfoGroup>>> GetAllAsync(string username);
        Task<IOnlineResponse<IDALInfoGroup>> GetFirstOfDefaultAsync(string username, Guid id);
        Task<IOnlineResponse<LoginResponse>> LoginAsync(long id, string password);
        Task<IOnlineResponse<LoginResponse>> SignUpAsync(string name, string password);
        Task<IOnlineResponse> UpdateAsync(string username, IDALInfoGroup infoGroup);
    }
}
