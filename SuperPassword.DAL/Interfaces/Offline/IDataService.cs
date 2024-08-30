using SuperPassword.DAL.Interfaces.Models;

namespace SuperPassword.DAL.Interfaces.Offline
{
    public partial interface IOfflineService
    {
        Task<IOfflineResponse> AddAsync(IDALInfoGroup newInfoGroup);

        Task<IOfflineResponse> DeleteAsync(Guid id);

        Task<IOfflineResponse<IList<IDALInfoGroup>>> GetAllAsync();

        Task<IOfflineResponse<IDALInfoGroup>> GetFirstOfDefaultAsync(Guid id);

        Task<IOfflineResponse> UpdateAsync(IDALInfoGroup newInfoGroup);
    }
}
