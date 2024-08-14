using SuperPassword.DAL.Implementations.Models;
using SuperPassword.DAL.Interfaces.Models;
using SuperPassword.Entity.Interface;

namespace SuperPassword.DAL.Interfaces
{
    public interface IDataService
    {
        Task<IDALResponse> AddAsync(string username, IInfoGroup entity);

        Task<IDALResponse> UpdateAsync(string username, IInfoGroup entity);

        Task<IDALResponse> DeleteAsync(string username, Guid id);

        Task<IDALResponse<IDALInfoGroup>> GetFirstOfDefaultAsync(string username, Guid id);

        Task<IDALResponse<IList<IDALInfoGroup>>> GetAllAsync(string username);
    }
}
