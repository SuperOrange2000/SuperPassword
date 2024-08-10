using SuperPassword.BLL.Interfaces.Models;
using SuperPassword.DAL.Interfaces.Models;
using SuperPassword.Entity.Interface;


namespace SuperPassword.BLL.Implementations
{
    public partial class BLLService
    {

        public async Task<IBLLResponse<object>> AddAsync(IInfoGroup infoGroup)
        {
            IDALResponse responseDAL = await _DALService.AddAsync(ActiveUser.Name, ActiveUser.Token, infoGroup);
            return Deserialize<object>(responseDAL);
        }

        public async Task<IBLLResponse<object>> DeleteAsync(uint id)
        {
            IDALResponse responseDAL = await _DALService.DeleteAsync(ActiveUser.Name, ActiveUser.Token, id);
            return Deserialize<object>(responseDAL);
        }

        public async Task<IBLLResponse<List<IInfoGroup>>> GetAllAsync()
        {
            IDALResponse responseDAL = await _DALService.GetAllAsync(ActiveUser.Name, ActiveUser.Token);
            return Deserialize<List<IInfoGroup>>(responseDAL);
        }

        public async Task<IBLLResponse<IInfoGroup>> GetFirstOfDefaultAsync(uint id)
        {
            IDALResponse responseDAL = await _DALService.GetFirstOfDefaultAsync(ActiveUser.Name, ActiveUser.Token, id);
            return Deserialize<IInfoGroup>(responseDAL);
        }

        public async Task<IBLLResponse<IInfoGroup>> UpdateAsync(IInfoGroup infoGroup)
        {
            IDALResponse responseDAL = await _DALService.UpdateAsync(ActiveUser.Name, ActiveUser.Token, infoGroup);
            return Deserialize<IInfoGroup>(responseDAL);
        }
    }
}
