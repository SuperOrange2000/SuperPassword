using Mapster;
using Microsoft.EntityFrameworkCore;
using SuperPassword.DAL.Implementations.Models;
using SuperPassword.DAL.Interfaces.Models;
using SuperPassword.Entity.Interface;

namespace SuperPassword.DAL.Implementations.Offline
{
    internal partial class OfflineService
    {
        public async Task<IDALResponse> AddAsync(IDALInfoGroup newInfoGroup)
        {
            await _infoGroupDbContext.AddAsync(newInfoGroup);
            await _infoGroupDbContext.SaveChangesAsync();
            return new DALResponse { DataStatus = ResponseDataStatus.Success };
        }

        public async Task<IDALResponse> DeleteAsync(Guid id)
        {
            DALInfoGroup result = await _infoGroupDbContext.InfoGroups.SingleAsync(item => item.InfoGroupGuid == id);
            _infoGroupDbContext.Remove(result);
            await _infoGroupDbContext.SaveChangesAsync();
            return new DALResponse { DataStatus = ResponseDataStatus.Success };
        }

        public async Task<IDALResponse<IList<IDALInfoGroup>>> GetAllAsync()
        {
            var result = await _infoGroupDbContext.InfoGroups.ToListAsync<IDALInfoGroup>();
            foreach (var infoGroup in result)
            {
                infoGroup.Tags = _infoGroupDbContext.Tags.Where(tag => tag.InfoGroupId == infoGroup.Id).ToList();
            }
            return new DALResponse<IList<IDALInfoGroup>>()
            {
                DataStatus = ResponseDataStatus.Success,
                Content = result
            };
        }

        public async Task<IDALResponse<IDALInfoGroup>> GetFirstOfDefaultAsync(Guid id)
        {
            DALInfoGroup result = await _infoGroupDbContext.InfoGroups.SingleAsync(item => item.InfoGroupGuid == id);
            return new DALResponse<IDALInfoGroup> { DataStatus = ResponseDataStatus.Success, Content = result };
        }

        public async Task<IDALResponse> UpdateAsync(IDALInfoGroup newInfoGroup)
        {
            DALInfoGroup target = await _infoGroupDbContext.InfoGroups.SingleAsync(item => item.InfoGroupGuid == newInfoGroup.InfoGroupGuid);
            var originalId = target.Id;
            newInfoGroup.Adapt(target);
            target.Id = originalId;
            await _infoGroupDbContext.SaveChangesAsync();
            return new DALResponse() { DataStatus = ResponseDataStatus.Success };
        }
    }
}
