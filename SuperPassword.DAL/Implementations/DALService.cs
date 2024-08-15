using Microsoft.Extensions.DependencyInjection;
using SuperPassword.Config.Service;
using SuperPassword.DAL.Implementations.Offline;
using SuperPassword.DAL.Implementations.Offline.DataContext;
using SuperPassword.DAL.Implementations.Online;
using SuperPassword.DAL.Interfaces;
using SuperPassword.DAL.Interfaces.Models;
using SuperPassword.DAL.Online.Clinet;
using SuperPassword.Entity.Interface;

namespace SuperPassword.DAL.Implementations
{
    public class DALService : IDALService
    {
        private IOfflineService offlineService;
        private IOnlineService onlineService;
        private IServiceProvider serviceProvider;
        public DALService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            offlineService = serviceProvider.GetService<IOfflineService>()!;
        }

        public static void AddService(ServiceCollection container)
        {
            container.AddSingleton<IOnlineService, OnlineService>();
            container.AddSingleton(provider => new HttpRestClient(provider.GetService<IConfigService>()!.AppConfig.ApiUrl));

            container.AddSingleton<IOfflineService, OfflineService>();
            container.AddSingleton((sp) =>
            {
                var configService = sp.GetService<IConfigService>()!;
                return new InfoGroupDbContext(configService.UserConfig.CombineUserPath("data.db"));
            });
        }

        public async Task<IDALResponse> AddAsync(string name, IDALInfoGroup newInfoGroup)
        {
            return await offlineService.AddAsync(newInfoGroup);
        }

        public async Task<IDALResponse> DeleteAsync(string username, Guid id)
        {
            return await offlineService.DeleteAsync(id);
        }

        public async Task<IDALResponse<IList<IDALInfoGroup>>> GetAllAsync(string username)
        {
            return await offlineService.GetAllAsync();
        }

        public async Task<IDALResponse<IDALInfoGroup>> GetFirstOfDefaultAsync(string username, Guid id)
        {
            return await offlineService.GetFirstOfDefaultAsync(id);
        }
        public async Task<IDALResponse> UpdateAsync(string username, IDALInfoGroup newInfoGroup)
        {
            return await offlineService.UpdateAsync(newInfoGroup);
        }

        public async Task<IDALResponse<byte[]>> LoginAsync(IUser user)
        {
            var result = await offlineService.LoginAsync(user);
            if (result.DataStatus == ResponseDataStatus.Success)
            {
                var dbContext = serviceProvider.GetService<InfoGroupDbContext>();
                if (dbContext != null)
                {
                    await offlineService.UpdateInfoGroupDbContextAsync(dbContext);
                }
                else throw new ArgumentNullException(nameof(dbContext));
            }
            return result;
        }

        public async Task<IDALResponse<byte[]>> SignUpAsync(IUser user)
        {
            var result = await offlineService.SignUpAsync(user);
            if (result.DataStatus == ResponseDataStatus.Success)
            {
                var dbContext = serviceProvider.GetService<InfoGroupDbContext>();
                if (dbContext != null)
                {
                    await offlineService.UpdateInfoGroupDbContextAsync(dbContext);
                }
                else throw new ArgumentNullException(nameof(dbContext));
            }
            return result;
        }

    }
}
