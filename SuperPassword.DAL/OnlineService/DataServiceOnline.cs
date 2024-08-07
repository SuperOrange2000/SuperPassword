using SuperPassword.DAL.OnlineService.Clinet;
using SuperPassword.Entity.Interface;
using SuperPassword.DAL.Models;

namespace SuperPassword.DAL.OnlineService
{
    public class DataserviceOnline : IDataServiceDAL
    {
        private readonly HttpRestClient client;

        public DataserviceOnline(HttpRestClient client)
        {
            this.client = client;
        }

        public async Task<ResponseDAL> AddAsync(string username, string token, IInfoGroup entity)
        {
            BaseRequest request = new BaseRequest("api/add", RestSharp.Method.Post);
            request.AddParameter("token", token);
            request.AddParameter("id", entity.Id);
            //request.AddParameter("salt", entity.Salt);
            request.AddParameter("username", entity.Username);
            request.AddParameter("site", entity.Site);
            request.AddParameter("tags", entity.Tags);
            return await client.ExecuteAsync(request);
        }

        public async Task<ResponseDAL> DeleteAsync(string username, string token, uint id)
        {
            BaseRequest request = new BaseRequest("api/delete", RestSharp.Method.Post);
            request.AddParameter("token", token);
            request.AddParameter("ids", new List<uint> { id });
            return await client.ExecuteAsync(request);
        }

        public async Task<ResponseDAL> GetAllAsync(string username, string token)
        {
            BaseRequest request = new BaseRequest("api/get", RestSharp.Method.Post);
            request.AddParameter("ids", new List<uint> { });
            request.AddParameter("token", token);
            var result = await client.ExecuteAsync(request);
            return result;
        }

        public async Task<ResponseDAL> GetFirstOfDefaultAsync(string username, string token, uint id)
        {
            BaseRequest request = new BaseRequest("api/get-data", RestSharp.Method.Post);
            request.AddParameter("token", token);
            return await client.ExecuteAsync(request);
        }

        public async Task<ResponseDAL> UpdateAsync(string username, string token, IInfoGroup entity)
        {
            BaseRequest request = new BaseRequest("api/update", RestSharp.Method.Post);
            request.AddParameter("token", token);
            request.AddParameter("id", entity.Id);
            request.AddParameter("username", entity.Username);
            request.AddParameter("password", entity.Password);
            request.AddParameter("site", entity.Site);
            request.AddParameter("tags", entity.Tags);
            return await client.ExecuteAsync(request);
        }
    }
}
