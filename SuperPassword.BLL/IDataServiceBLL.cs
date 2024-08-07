using SuperPassword.Entity.Interface;
using SuperPassword.BLL.Models;

namespace SuperPassword.BLL
{
    public interface IDataServiceBLL
    {
        Task<ResponseBLL<object>> AddAsync(string username, string token, IInfoGroup entity);

        Task<ResponseBLL<IInfoGroup>> UpdateAsync(string username, string token, IInfoGroup entity);

        Task<ResponseBLL<object>> DeleteAsync(string username, string token, uint id);

        Task<ResponseBLL<IInfoGroup>> GetFirstOfDefaultAsync(string username, string token, uint id);

        Task<ResponseBLL<List<IInfoGroup>>> GetAllAsync(string username, string token);
    }
}
