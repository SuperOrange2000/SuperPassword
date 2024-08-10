using Microsoft.Extensions.DependencyInjection;
using SuperPassword.Config.Service;
using SuperPassword.DAL.Implementations.Online;
using SuperPassword.DAL.Interfaces;
using SuperPassword.DAL.Interfaces.Models;
using SuperPassword.DAL.Online.Clinet;
using SuperPassword.Entity.Interface;

namespace SuperPassword.DAL.Implementations
{
    public class DALService : IDALService
    {
        private IInternalService service;
        public DALService(IServiceProvider serviceProvider, IConfigService configService)
        {
            service = serviceProvider.GetService<IOnlineService>()!;
        }

        public static void AddService(ServiceCollection container)
        {
            container.AddSingleton<IOnlineService, OnlineService>();
            container.AddSingleton(provider => new HttpRestClient(provider.GetService<IConfigService>()!.AppConfig.ApiUrl));
        }

        public async Task<IDALResponse> AddAsync(string username, string token, IInfoGroup entity)
        {
            return await service.AddAsync(username, token, entity);
        }

        public async Task<IDALResponse> DeleteAsync(string username, string token, uint id)
        {
            return await service.DeleteAsync(username, token, id);
        }

        public async Task<IDALResponse> GetAllAsync(string username, string token)
        {
            return await service.GetAllAsync(username, token);
        }

        public async Task<IDALResponse> GetFirstOfDefaultAsync(string username, string token, uint id)
        {
            return await service.GetFirstOfDefaultAsync(username, token, id);
        }

        public async Task<IDALResponse> Login(string name, string password)
        {
            return await service.Login(name, password);
        }

        public async Task<IDALResponse> SignUp(string name, string password)
        {
            return await service.SignUp(name, password);
        }

        public async Task<IDALResponse> UpdateAsync(string username, string token, IInfoGroup entity)
        {
            return await service.UpdateAsync(username, token, entity);
        }
    }
}
