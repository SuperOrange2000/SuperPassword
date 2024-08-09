using SuperPassword.DAL.Interfaces.Models;
using SuperPassword.Entity.Interface;

namespace SuperPassword.DAL.Interfaces
{
    public interface IDataService
    {
        Task<IDALResponse> AddAsync(string username, string token, IInfoGroup entity);

        Task<IDALResponse> UpdateAsync(string username, string token, IInfoGroup entity);

        Task<IDALResponse> DeleteAsync(string username, string token, uint id);

        Task<IDALResponse> GetFirstOfDefaultAsync(string username, string token, uint id);

        Task<IDALResponse> GetAllAsync(string username, string token);
    }
}
