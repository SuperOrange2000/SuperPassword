using Mapster;
using SuperPassword.BLL.Implementations.Models;
using SuperPassword.BLL.Interfaces.Models;
using SuperPassword.DAL.Interfaces.Models;
using SuperPassword.Entity.Interface;


namespace SuperPassword.BLL.Implementations
{
    public partial class BLLService
    {
        public async Task<IBLLResponse> AddAsync(IInfoGroup infoGroup)
        {
            IDALResponse responseDAL = await _DALService.AddAsync(ActiveUser.Name, infoGroup);
            return new BLLResponse(responseDAL);
        }

        public async Task<IBLLResponse> DeleteAsync(Guid id)
        {
            IDALResponse responseDAL = await _DALService.DeleteAsync(ActiveUser.Name, id);
            return new BLLResponse(responseDAL);
        }

        public async Task<IBLLResponse<IList<IBLLInfoGroup>>> GetAllAsync()
        {
            IDALResponse<IList<IDALInfoGroup>> responseDAL = await _DALService.GetAllAsync(ActiveUser.Name);
            BLLResponse<IList<IBLLInfoGroup>> result = new();
            responseDAL.Adapt(result);
            return result;
        }

        public async Task<IBLLResponse<IBLLInfoGroup>> GetFirstOfDefaultAsync(Guid id)
        {
            IDALResponse<IDALInfoGroup> responseDAL = await _DALService.GetFirstOfDefaultAsync(ActiveUser.Name, id);
            BLLResponse<IBLLInfoGroup> result = new();
            responseDAL.Adapt(result);
            return result;
        }

        public async Task<IBLLResponse> UpdateAsync(IInfoGroup infoGroup)
        {
            IDALResponse responseDAL = await _DALService.UpdateAsync(ActiveUser.Name, infoGroup);
            return new BLLResponse(responseDAL);
        }
    }
}
