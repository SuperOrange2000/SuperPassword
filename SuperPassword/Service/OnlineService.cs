using SuperPassword.Shared.Contact;
using SuperPassword.Shared.Dtos;
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
            request.Parameters.Add("token", user.Token);
            request.Parameters.Add("id", entity.ID);
            request.Parameters.Add("username", entity.Username);
            request.Parameters.Add("password", entity.Password);
            request.Parameters.Add("site", entity.Site);
            request.Parameters.Add("tags", entity.TagDtos);
            return await client.ExecuteAsync<object>(request);
        }

        public async Task<ApiResponse<object>> DeleteAsync(UserDto user, string id)
        {
            BaseRequest request = new BaseRequest("api/delete", RestSharp.Method.Post);
            request.Parameters.Add("token", user.Token);
            request.Parameters.Add("ids", new List<string> { id });
            return await client.ExecuteAsync<object>(request);
        }

        public async Task<ApiResponse<List<InfoGroupDTO>>> GetAllAsync(UserDto user)
        {
            BaseRequest request = new BaseRequest("api/basic-get", RestSharp.Method.Post);
            request.Parameters.Add("ids", new List<string> { });
            request.Parameters.Add("token", user.Token);
            return await client.ExecuteAsync<List<InfoGroupDTO>>(request);
        }

        public async Task<ApiResponse<InfoGroupDTO>> GetFirstOfDefaultAsync(UserDto user, string id)
        {
            BaseRequest request = new BaseRequest("api/detailed-get", RestSharp.Method.Post);
            request.Parameters.Add("token", user.Token);
            return await client.ExecuteAsync<InfoGroupDTO>(request);
        }

        public async Task<ApiResponse<InfoGroupDTO>> UpdateAsync(UserDto user, InfoGroupDTO entity)
        {
            BaseRequest request = new BaseRequest("api/update", RestSharp.Method.Post);
            request.Parameters.Add("token", user.Token);
            request.Parameters.Add("id", entity.ID);
            request.Parameters.Add("username", entity.Username);
            request.Parameters.Add("password", entity.Password);
            request.Parameters.Add("site", entity.Site);
            request.Parameters.Add("tags", entity.TagDtos);
            return await client.ExecuteAsync<InfoGroupDTO>(request);
        }

        public async Task<ApiResponse<string>> SignUp(UserDto User, string password)
        {
            BaseRequest request = new BaseRequest("api/sign-up/", RestSharp.Method.Post);
            request.Parameters.Add("username", User.UserName);
            request.Parameters.Add("password", password);
            return await client.ExecuteAsync<string>(request);
        }

        public async Task<ApiResponse<string>> Login(UserDto User, string password)
        {
            BaseRequest request = new BaseRequest("api/login/", RestSharp.Method.Post);
            request.Parameters.Add("username", User.UserName);
            request.Parameters.Add("password", password);
            request.Parameters.Add("device", "windows");
            return await client.ExecuteAsync<string>(request);
        }
    }
}
