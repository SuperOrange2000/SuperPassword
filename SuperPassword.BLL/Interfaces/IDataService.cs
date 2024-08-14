using SuperPassword.BLL.Interfaces.Models;
using SuperPassword.Entity.Interface;

namespace SuperPassword.BLL.Interfaces
{
    public interface IDataService
    {
        Task<IBLLResponse> AddAsync(IInfoGroup entity);

        Task<IBLLResponse> UpdateAsync(IInfoGroup entity);

        Task<IBLLResponse> DeleteAsync(Guid id);

        Task<IBLLResponse<IBLLInfoGroup>> GetFirstOfDefaultAsync(Guid id);

        Task<IBLLResponse<IList<IBLLInfoGroup>>> GetAllAsync();
    }
}
