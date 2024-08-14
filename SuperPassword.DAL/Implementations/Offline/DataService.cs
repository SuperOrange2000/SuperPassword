using Mapster;
using Microsoft.EntityFrameworkCore;
using SuperPassword.DAL.Implementations.Models;
using SuperPassword.DAL.Interfaces.Models;
using SuperPassword.Entity.Interface;

namespace SuperPassword.DAL.Implementations.Offline
{
    internal partial class OfflineService
    {
        public async Task<IDALResponse> AddAsync(string username, IInfoGroup entity)
        {
            DALInfoGroup newInfoGroup = new();
            entity.Adapt(newInfoGroup);
            await _infoGroupDbContext.AddAsync(newInfoGroup);
            await _infoGroupDbContext.SaveChangesAsync();
            return new DALResponse { DataStatus = ResponseDataStatus.Success };
        }

        public async Task<IDALResponse> DeleteAsync(string username, Guid id)
        {
            DALInfoGroup result = await _infoGroupDbContext.InfoGroups.SingleAsync(item => item.InfoGroupGuid == id);
            _infoGroupDbContext.Remove(result);
            await _infoGroupDbContext.SaveChangesAsync();
            return new DALResponse { DataStatus = ResponseDataStatus.Success };
        }

        public async Task<IDALResponse<IList<IDALInfoGroup>>> GetAllAsync(string username)
        {
            return new DALResponse<IList<IDALInfoGroup>>()
            {
                DataStatus = ResponseDataStatus.Success,
                Content = await _infoGroupDbContext.InfoGroups.ToListAsync<IDALInfoGroup>()
            };
        }

        public async Task<IDALResponse<IDALInfoGroup>> GetFirstOfDefaultAsync(string username, Guid id)
        {
            DALInfoGroup result = await _infoGroupDbContext.InfoGroups.SingleAsync(item => item.InfoGroupGuid == id);
            return new DALResponse<IDALInfoGroup> { DataStatus = ResponseDataStatus.Success, Content = result };
        }

        public async Task<IDALResponse> UpdateAsync(string username, IInfoGroup entity)
        {
            DALInfoGroup result = await _infoGroupDbContext.InfoGroups.SingleAsync(item => item.InfoGroupGuid == entity.InfoGroupGuid);
            result.Adapt(entity);
            return new DALResponse() { DataStatus = ResponseDataStatus.Success };
        }
    }
}
