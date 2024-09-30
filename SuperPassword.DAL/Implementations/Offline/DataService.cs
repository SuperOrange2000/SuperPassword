using Mapster;
using Microsoft.EntityFrameworkCore;
using SuperPassword.DAL.Models;

namespace SuperPassword.DAL.Implementations.Offline
{
    public partial class OfflineService
    {
        public async Task<OfflineResponse> AddAsync(DALInfoGroup newInfoGroup)
        {
            await _infoGroupDbContext.AddAsync(newInfoGroup);
            await _infoGroupDbContext.SaveChangesAsync();
            return new OfflineResponse { DataStatus = ResponseDataStatus.Success };
        }

        public async Task<OfflineResponse> DeleteAsync(Guid id)
        {
            DALInfoGroup result = await _infoGroupDbContext.InfoGroups.SingleAsync(item => item.InfoGroupGuid == id);
            _infoGroupDbContext.Remove(result);
            await _infoGroupDbContext.SaveChangesAsync();
            return new OfflineResponse { DataStatus = ResponseDataStatus.Success };
        }

        public async Task<OfflineResponse<List<DALInfoGroup>>> GetAllAsync()
        {
            var result = await _infoGroupDbContext.InfoGroups.ToListAsync<DALInfoGroup>();
            foreach (var infoGroup in result)
            {
                infoGroup.Tags = _infoGroupDbContext.
                    Tags.
                    Where(tag => tag.InfoGroupId == infoGroup.Id).
                    ToList();
            }
            return new OfflineResponse<List<DALInfoGroup>>()
            {
                DataStatus = ResponseDataStatus.Success,
                Content = result
            };
        }

        public async Task<OfflineResponse<DALInfoGroup>> GetFirstOfDefaultAsync(Guid id)
        {
            DALInfoGroup result = await _infoGroupDbContext.InfoGroups.SingleAsync(item => item.InfoGroupGuid == id);
            return new OfflineResponse<DALInfoGroup> { DataStatus = ResponseDataStatus.Success, Content = result };
        }

        public async Task<OfflineResponse> UpdateAsync(DALInfoGroup newInfoGroup)
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
