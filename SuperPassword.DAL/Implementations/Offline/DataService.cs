using Mapster;
using Microsoft.EntityFrameworkCore;
using SuperPassword.DAL.Implementations.Models;
using SuperPassword.DAL.Interfaces.Models;
using SuperPassword.DAL.Interfaces.Offline;

namespace SuperPassword.DAL.Implementations.Offline
{
    public partial class OfflineService
    {
        public async Task<IOfflineResponse> AddAsync(IDALInfoGroup newInfoGroup)
        {
            await _infoGroupDbContext.AddAsync(newInfoGroup);
            await _infoGroupDbContext.SaveChangesAsync();
            return new OfflineResponse { DataStatus = ResponseDataStatus.Success };
        }

        public async Task<IOfflineResponse> DeleteAsync(Guid id)
        {
            DALInfoGroup result = await _infoGroupDbContext.InfoGroups.SingleAsync(item => item.InfoGroupGuid == id);
            _infoGroupDbContext.Remove(result);
            await _infoGroupDbContext.SaveChangesAsync();
            return new OfflineResponse { DataStatus = ResponseDataStatus.Success };
        }

        public async Task<IOfflineResponse<IList<IDALInfoGroup>>> GetAllAsync()
        {
            var result = await _infoGroupDbContext.InfoGroups.ToListAsync<IDALInfoGroup>();
            foreach (var infoGroup in result)
            {
                infoGroup.Tags = _infoGroupDbContext.
                    Tags.
                    Where(tag => tag.InfoGroupId == infoGroup.Id).
                    ToList();
            }
            return new OfflineResponse<IList<IDALInfoGroup>>()
            {
                DataStatus = ResponseDataStatus.Success,
                Content = result
            };
        }

        public async Task<IOfflineResponse<IDALInfoGroup>> GetFirstOfDefaultAsync(Guid id)
        {
            DALInfoGroup result = await _infoGroupDbContext.InfoGroups.SingleAsync(item => item.InfoGroupGuid == id);
            return new OfflineResponse<IDALInfoGroup> { DataStatus = ResponseDataStatus.Success, Content = result };
        }

        public async Task<IOfflineResponse> UpdateAsync(IDALInfoGroup newInfoGroup)
        {
            DALInfoGroup target = await _infoGroupDbContext.InfoGroups.SingleAsync(item => item.InfoGroupGuid == newInfoGroup.InfoGroupGuid);
            var originalId = target.Id;
            newInfoGroup.Adapt(target);
            target.Id = originalId;
            await _infoGroupDbContext.SaveChangesAsync();
            return new OfflineResponse { DataStatus = ResponseDataStatus.Success };
        }
    }
}
