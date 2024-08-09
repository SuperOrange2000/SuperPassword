using SuperPassword.BLL.Interfaces.Models;
using SuperPassword.Entity.Interface;

namespace SuperPassword.BLL.Interfaces
{
    public interface IDataService
    {
        Task<IBLLResponse<object>> AddAsync(IInfoGroup entity);

        Task<IBLLResponse<IInfoGroup>> UpdateAsync(IInfoGroup entity);

        Task<IBLLResponse<object>> DeleteAsync(uint id);

        Task<IBLLResponse<IInfoGroup>> GetFirstOfDefaultAsync(uint id);

        Task<IBLLResponse<List<IInfoGroup>>> GetAllAsync();
    }
}
