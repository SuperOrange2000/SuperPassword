using SuperPassword.DAL.Interfaces.Models;
using SuperPassword.DAL.Interfaces.Online;
using SuperPassword.DAL.Online.Client;

namespace SuperPassword.DAL.Implementations.Online
{
    public partial class OnlineService
    {
        public async Task<IOnlineResponse> AddAsync(string username, IDALInfoGroup newInfoGroup)
        {
            BaseRequest request = new BaseRequest("api/add", RestSharp.Method.Post);
            request.AddParameter("token", Token);
            request.AddParameter("id", newInfoGroup.InfoGroupGuid);
            //request.AddParameter("salt", entity.Salt);
            request.AddParameter("site", newInfoGroup.Site);
            request.AddParameter("site_nonce", newInfoGroup.SiteNonce);
            request.AddParameter("site", newInfoGroup.Site);
            request.AddParameter("username", newInfoGroup.Username);
            request.AddParameter("password", newInfoGroup.Password);
            request.AddParameter("tags", newInfoGroup.Tags);
            return await client.RequestAsync(request);
        }

        public async Task<IOnlineResponse> DeleteAsync(string username, Guid id)
        {
            BaseRequest request = new BaseRequest("api/delete", RestSharp.Method.Post);
            request.AddParameter("Token", Token);
            request.AddParameter("ids", new List<Guid> { id });
            return await client.RequestAsync(request);
        }

        public async Task<IOnlineResponse<IList<IDALInfoGroup>>> GetAllAsync(string username)
        {
            BaseRequest request = new BaseRequest("api/get", RestSharp.Method.Post);
            request.AddParameter("ids", new List<uint> { });
            request.AddParameter("Token", Token);
            var result = await client.RequestAsync<IList<IDALInfoGroup>>(request);
            return result;
        }

        public async Task<IOnlineResponse<IDALInfoGroup>> GetFirstOfDefaultAsync(string username, Guid id)
        {
            BaseRequest request = new BaseRequest("api/get-data", RestSharp.Method.Post);
            request.AddParameter("Token", Token);
            return await client.RequestAsync<IDALInfoGroup>(request);
        }

        public async Task<IOnlineResponse> UpdateAsync(string username, IDALInfoGroup infoGroup)
        {
            BaseRequest request = new BaseRequest("api/update", RestSharp.Method.Post);
            request.AddParameter("Token", Token);
            request.AddParameter("id", infoGroup.InfoGroupGuid);
            request.AddParameter("username", infoGroup.Username);
            request.AddParameter("password", infoGroup.Password);
            request.AddParameter("site", infoGroup.Site);
            request.AddParameter("tags", infoGroup.Tags);
            return await client.RequestAsync(request);
        }
    }
}
