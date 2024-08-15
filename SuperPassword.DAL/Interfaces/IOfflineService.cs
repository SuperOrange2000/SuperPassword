using SuperPassword.DAL.Implementations.Offline.DataContext;
using SuperPassword.DAL.Interfaces.Models;
using SuperPassword.Entity.Interface;

namespace SuperPassword.DAL.Interfaces
{
    internal interface IOfflineService
    {
        Task UpdateInfoGroupDbContextAsync(InfoGroupDbContext infoGroupDbContext);
        Task<IDALResponse> AddAsync(IDALInfoGroup newInfoGroup);

        Task<IDALResponse> DeleteAsync(Guid id);

        Task<IDALResponse<IList<IDALInfoGroup>>> GetAllAsync();

        Task<IDALResponse<IDALInfoGroup>> GetFirstOfDefaultAsync(Guid id);

        Task<IDALResponse> UpdateAsync(IDALInfoGroup newInfoGroup);

        Task<IDALResponse<byte[]>> LoginAsync(IUser user);

        Task<IDALResponse<byte[]>> SignUpAsync(IUser user);
    }
}
