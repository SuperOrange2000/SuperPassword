using SuperPassword.DAL.Interfaces;
using SuperPassword.DAL.Interfaces.Models;
using SuperPassword.DAL.Online.Clinet;
using SuperPassword.Entity.Interface;

namespace SuperPassword.DAL.Implementations.Online
{
    internal partial class OnlineService : IOnlineService
    {
        public async Task<IDALResponse> AddAsync(string username, IInfoGroup entity)
        {
            BaseRequest request = new BaseRequest("api/add", RestSharp.Method.Post);
            request.AddParameter("Token", Token);
            request.AddParameter("id", entity.InfoGroupGuid);
            //request.AddParameter("salt", entity.Salt);
            request.AddParameter("username", entity.Username);
            request.AddParameter("site", entity.Site);
            request.AddParameter("tags", entity.Tags);
            return await client.RequestAsync(request);
        }

        public async Task<IDALResponse> DeleteAsync(string username, Guid id)
        {
            BaseRequest request = new BaseRequest("api/delete", RestSharp.Method.Post);
            request.AddParameter("Token", Token);
            request.AddParameter("ids", new List<Guid> { id });
            return await client.RequestAsync(request);
        }

        public async Task<IDALResponse<IList<IDALInfoGroup>>> GetAllAsync(string username)
        {
            BaseRequest request = new BaseRequest("api/get", RestSharp.Method.Post);
            request.AddParameter("ids", new List<uint> { });
            request.AddParameter("Token", Token);
            var result = await client.RequestAsync<IList<IDALInfoGroup>>(request);
            return result;
        }

        public async Task<IDALResponse<IDALInfoGroup>> GetFirstOfDefaultAsync(string username, Guid id)
        {
            BaseRequest request = new BaseRequest("api/get-data", RestSharp.Method.Post);
            request.AddParameter("Token", Token);
            return await client.RequestAsync<IDALInfoGroup>(request);
        }

        public async Task<IDALResponse> UpdateAsync(string username, IInfoGroup entity)
        {
            BaseRequest request = new BaseRequest("api/update", RestSharp.Method.Post);
            request.AddParameter("Token", Token);
            request.AddParameter("id", entity.InfoGroupGuid);
            request.AddParameter("username", entity.Username);
            request.AddParameter("password", entity.Password);
            request.AddParameter("site", entity.Site);
            request.AddParameter("tags", entity.Tags);
            return await client.RequestAsync(request);
        }
    }
}
