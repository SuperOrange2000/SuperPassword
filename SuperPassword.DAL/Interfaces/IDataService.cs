using SuperPassword.DAL.Interfaces.Models;

namespace SuperPassword.DAL.Interfaces
{
    public interface IDataService
    {
        Task<IDALResponse> AddAsync(string name, IDALInfoGroup newInfoGroup);

        Task<IDALResponse> UpdateAsync(string name, IDALInfoGroup newInfoGroup);

        Task<IDALResponse> DeleteAsync(string username, Guid id);

        Task<IDALResponse<IDALInfoGroup>> GetFirstOfDefaultAsync(string username, Guid id);

        Task<IDALResponse<IList<IDALInfoGroup>>> GetAllAsync(string username);
    }
}
