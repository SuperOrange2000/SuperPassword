using SuperPassword.DAL.Models;

namespace SuperPassword.DAL.Interfaces.Offline
{
    public partial interface IOfflineService
    {
        Task<OfflineResponse> AddAsync(DALInfoGroup newInfoGroup);

        Task<OfflineResponse> DeleteAsync(Guid id);

        Task<OfflineResponse<List<DALInfoGroup>>> GetAllAsync();

        Task<OfflineResponse<DALInfoGroup>> GetFirstOfDefaultAsync(Guid id);

        Task<OfflineResponse> UpdateAsync(DALInfoGroup newInfoGroup);
    }
}
