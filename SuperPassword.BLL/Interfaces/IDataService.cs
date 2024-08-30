using SuperPassword.BLL.Interfaces.Models;
using SuperPassword.DAL.Interfaces.Models;
using SuperPassword.Entity.Interface;

namespace SuperPassword.BLL.Interfaces
{
    public partial interface IBLLService
    {
        Task<IBLLResponse> AddAsync(IInfoGroup entity);

        Task<IBLLResponse> UpdateAsync(IInfoGroup entity);

        Task<IBLLResponse> DeleteAsync(Guid id);

        Task<IBLLResponse<IDALInfoGroup, IBLLInfoGroup>> GetFirstOfDefaultAsync(Guid id);

        Task<IBLLResponse<IList<IDALInfoGroup>, IList<IBLLInfoGroup>>> GetAllAsync();
    }
}
