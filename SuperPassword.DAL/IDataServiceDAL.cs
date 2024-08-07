using SuperPassword.Entity.Interface;
using SuperPassword.DAL.Models;

namespace SuperPassword.DAL
{
    public interface IDataServiceDAL
    {
        Task<ResponseDAL> AddAsync(string username, string token, IInfoGroup entity);

        Task<ResponseDAL> UpdateAsync(string username, string token, IInfoGroup entity);

        Task<ResponseDAL> DeleteAsync(string username, string token, uint id);

        Task<ResponseDAL> GetFirstOfDefaultAsync(string username, string token, uint id);

        Task<ResponseDAL> GetAllAsync(string username, string token);
    }
}
