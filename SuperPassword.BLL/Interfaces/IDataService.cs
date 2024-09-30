using SuperPassword.BLL.Models;
using SuperPassword.DAL.Models;
using SuperPassword.Entity.Interface;

namespace SuperPassword.BLL.Interfaces
{
    public partial interface IBLLService
    {
        Task<BLLResponse> AddAsync(IInfoGroup entity);

        Task<BLLResponse> UpdateAsync(IInfoGroup entity);

        Task<BLLResponse> DeleteAsync(Guid id);

        Task<BLLResponse<DALInfoGroup, BLLInfoGroup>> GetFirstOfDefaultAsync(Guid id);

        Task<BLLResponse<List<DALInfoGroup>, List<BLLInfoGroup>>> GetAllAsync();
    }
}
