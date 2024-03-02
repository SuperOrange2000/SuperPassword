using SuperPassword.DAL.OnlineService.Clinet;
using SuperPassword.Entity;
using SuperPassword.Entity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperPassword.DAL.OnlineService
{
    public class DataserviceOnline : IDataServiceDAL
    {
        private readonly HttpRestClient client;

        public DataserviceOnline(HttpRestClient client)
        {
            this.client = client;
        }

        public async Task<ResponseDAL> AddAsync(UserEntity user, InfoGroupEntity entity)
        {
            BaseRequest request = new BaseRequest("api/add", RestSharp.Method.Post);
            request.AddParameter("token", user.Token);
            request.AddParameter("id", entity.ID);
            request.AddParameter("salt", entity.Salt);
            request.AddParameter("username", entity.EncryptedUsername);
            request.AddParameter("password", entity.EncryptedPassword);
            request.AddParameter("site", entity.EncryptedSite);
            request.AddParameter("tags", entity.EncryptedTagEntities);
            return await client.ExecuteAsync(request);
        }

        public async Task<ResponseDAL> DeleteAsync(UserEntity user, uint id)
        {
            BaseRequest request = new BaseRequest("api/delete", RestSharp.Method.Post);
            request.AddParameter("token", user.Token);
            request.AddParameter("ids", new List<uint> { id });
            return await client.ExecuteAsync(request);
        }

        public async Task<ResponseDAL> GetAllAsync(UserEntity user)
        {
            BaseRequest request = new BaseRequest("api/get", RestSharp.Method.Post);
            request.AddParameter("ids", new List<uint> { });
            request.AddParameter("token", user.Token);
            var result = await client.ExecuteAsync(request);
            return result;
        }

        public async Task<ResponseDAL> GetFirstOfDefaultAsync(UserEntity user, uint id)
        {
            BaseRequest request = new BaseRequest("api/get-data", RestSharp.Method.Post);
            request.AddParameter("token", user.Token);
            return await client.ExecuteAsync(request);
        }

        public async Task<ResponseDAL> UpdateAsync(UserEntity user, InfoGroupEntity entity)
        {
            BaseRequest request = new BaseRequest("api/update", RestSharp.Method.Post);
            request.AddParameter("token", user.Token);
            request.AddParameter("id", entity.ID);
            request.AddParameter("username", entity.EncryptedUsername);
            request.AddParameter("password", entity.EncryptedPassword);
            request.AddParameter("site", entity.EncryptedSite);
            request.AddParameter("tags", entity.EncryptedTagEntities);
            return await client.ExecuteAsync(request);
        }
    }
}
