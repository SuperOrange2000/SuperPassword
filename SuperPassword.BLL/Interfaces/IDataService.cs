using SuperPassword.BLL.Interfaces.Models;
using SuperPassword.DAL.Models;
using SuperPassword.Entity.Interface;

namespace SuperPassword.BLL.Interfaces
{
    public partial interface IBLLService
    {
        Task<IBLLResponse> AddAsync(IInfoGroup entity);

        Task<IBLLResponse> UpdateAsync(IInfoGroup entity);

        Task<IBLLResponse> DeleteAsync(Guid id);

        Task<IBLLResponse<DALInfoGroup, IBLLInfoGroup>> GetFirstOfDefaultAsync(Guid id);

        Task<IBLLResponse<List<DALInfoGroup>, List<IBLLInfoGroup>>> GetAllAsync();
    }
}
