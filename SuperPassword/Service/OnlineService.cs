using RestSharp;
using SuperPassword.Shared.Contact;
using SuperPassword.Shared.Dtos;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SuperPassword.Service
{
    class OnlineService : IOnlineService
    {
        private readonly HttpRestClient client;

        public OnlineService(HttpRestClient client)
        {
            this.client = client;
        }

        public async Task<ApiResponse<object>> AddAsync(UserDto user, InfoGroupDTO entity)
        {
            BaseRequest request = new BaseRequest("api/add", RestSharp.Method.Post);
            request.AddParameter("token", user.Token);
            request.AddParameter("id", entity.ID);
            request.AddParameter("data", entity.ToByteArray());
            request.AddParameter("salt", entity.Salt);
            return await client.ExecuteAsync<object>(request);
        }

        public async Task<ApiResponse<object>> DeleteAsync(UserDto user, uint id)
        {
            BaseRequest request = new BaseRequest("api/delete", RestSharp.Method.Post);
            request.AddParameter("token", user.Token);
            request.AddParameter("ids", new List<uint> { id });
            return await client.ExecuteAsync<object>(request);
        }

        public async Task<ApiResponse<List<InfoGroupDTO>>> GetAllAsync(UserDto user)
        {
            BaseRequest request = new BaseRequest("api/quick-get-data", RestSharp.Method.Post);
            request.AddParameter("ids", new List<string> { });
            request.AddParameter("token", user.Token);
            return await client.ExecuteAsync<List<InfoGroupDTO>>(request);
        }

        public async Task<ApiResponse<InfoGroupDTO>> GetFirstOfDefaultAsync(UserDto user, uint id)
        {
            BaseRequest request = new BaseRequest("api/get-data", RestSharp.Method.Post);
            request.AddParameter("token", user.Token);
            return await client.ExecuteAsync<InfoGroupDTO>(request);
        }

        public async Task<ApiResponse<InfoGroupDTO>> UpdateAsync(UserDto user, InfoGroupDTO entity)
        {
            BaseRequest request = new BaseRequest("api/update", RestSharp.Method.Post);
            request.AddParameter("token", user.Token);
            request.AddParameter("id", entity.ID);
            request.AddParameter("username", entity.Username);
            request.AddParameter("password", entity.Password);
            request.AddParameter("site", entity.Site);
            request.AddParameter("tags", entity.TagDtos);
            return await client.ExecuteAsync<InfoGroupDTO>(request);
        }

        public async Task<ApiResponse<string>> SignUp(UserDto User, string password)
        {
            BaseRequest request = new BaseRequest("api/sign-up/", RestSharp.Method.Post);
            request.AddParameter("username", User.UserName);
            request.AddParameter("password", password);
            return await client.ExecuteAsync<string>(request);
        }

        public async Task<ApiResponse<string>> Login(UserDto User, string password)
        {
            BaseRequest request = new BaseRequest("api/login/", RestSharp.Method.Post);
            request.AddParameter("username", User.UserName);
            request.AddParameter("password", password);
            request.AddParameter("device", "windows");
            return await client.ExecuteAsync<string>(request);
        }
    }
}
