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
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.Get;
            request.Route = $"log/csrf/";
            client.ExecuteAsync<object>(request);
        }

        public async Task<ApiResponse<object>> AddAsync(InfoGroupDTO entity)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.Post;
            request.Route = $"api/add/";
            request.Parameters.Add("id", entity.ID);
            request.Parameters.Add("account", entity.Username);
            request.Parameters.Add("password", entity.Password);
            request.Parameters.Add("site", entity.Site);
            request.Parameters.Add("tags", entity.TagDtos);
            return await client.ExecuteAsync<object>(request);
        }

        public async Task<ApiResponse<object>> DeleteAsync(string id)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.Delete;
            request.Route = $"api/delete/";
            request.Parameters.Add("ids", new List<string> { id });
            return await client.ExecuteAsync<object>(request);
        }

        public async Task<ApiResponse<List<InfoGroupDTO>>> GetAllAsync()
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.Get;
            request.Route = $"api/get/";
            request.Parameters.Add("ids", new List<string> { });
            return await client.ExecuteAsync<List<InfoGroupDTO>>(request);
        }

        public async Task<ApiResponse<InfoGroupDTO>> GetFirstOfDefaultAsync(string id)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.Get;
            request.Route = $"api/Get?id={id}";
            return await client.ExecuteAsync<InfoGroupDTO>(request);
        }

        public async Task<ApiResponse<InfoGroupDTO>> UpdateAsync(InfoGroupDTO entity)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.Post;
            request.Route = $"api/update/";
            request.Parameters.Add("id", entity.ID);
            request.Parameters.Add("account", entity.Username);
            request.Parameters.Add("password", entity.Password);
            request.Parameters.Add("site", entity.Site);
            request.Parameters.Add("tags", entity.TagDtos);
            return await client.ExecuteAsync<InfoGroupDTO>(request);
        }

        public async Task<ApiResponse<string>> SignUp(UserDto entity)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.Post;
            request.Route = $"log/sign-up/";
            request.Parameters.Add("account", entity.Account);
            request.Parameters.Add("password", entity.Password);
            return await client.ExecuteAsync<string>(request);
        }

        public async Task<ApiResponse<string>> Login(UserDto entity)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.Post;
            request.Route = $"log/login/";
            request.Parameters.Add("account", entity.Account);
            request.Parameters.Add("password", entity.Password);
            return await client.ExecuteAsync<string>(request);
        }
    }
}
