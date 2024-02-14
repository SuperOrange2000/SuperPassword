using RestSharp;
using SuperPassword.Shared.Contact;
using SuperPassword.Shared.DTOs;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task<ApiResponse<object>> AddAsync(UserDTO user, InfoGroupDTO entity)
        {
            BaseRequest request = new BaseRequest("api/add", RestSharp.Method.Post);
            request.AddParameter("token", user.Token);
            request.AddParameter("id", entity.ID);
            request.AddParameter("salt", entity.Salt);
            request.AddParameter("username", entity.EncryptedUsername);
            request.AddParameter("password", entity.EncryptedPassword);
            request.AddParameter("site", entity.EncryptedSite);
            request.AddParameter("tags", entity.EncryptedTagDTOs);
            return await client.ExecuteAsync<object>(request);
        }

        public async Task<ApiResponse<object>> DeleteAsync(UserDTO user, uint id)
        {
            BaseRequest request = new BaseRequest("api/delete", RestSharp.Method.Post);
            request.AddParameter("token", user.Token);
            request.AddParameter("ids", new List<uint> { id });
            return await client.ExecuteAsync<object>(request);
        }

        public async Task<ApiResponse<List<InfoGroupDTO>>> GetAllAsync(UserDTO user)
        {
            BaseRequest request = new BaseRequest("api/get", RestSharp.Method.Post);
            request.AddParameter("ids", new List<string> { });
            request.AddParameter("token", user.Token);
            var result = await client.ExecuteAsync<List<InfoGroupDTO>>(request);
            return result;
        }

        public async Task<ApiResponse<InfoGroupDTO>> GetFirstOfDefaultAsync(UserDTO user, uint id)
        {
            BaseRequest request = new BaseRequest("api/get-data", RestSharp.Method.Post);
            request.AddParameter("token", user.Token);
            return await client.ExecuteAsync<InfoGroupDTO>(request);
        }

        public async Task<ApiResponse<InfoGroupDTO>> UpdateAsync(UserDTO user, InfoGroupDTO entity)
        {
            BaseRequest request = new BaseRequest("api/update", RestSharp.Method.Post);
            request.AddParameter("token", user.Token);
            request.AddParameter("id", entity.ID);
            request.AddParameter("username", entity.EncryptedUsername);
            request.AddParameter("password", entity.EncryptedPassword);
            request.AddParameter("site", entity.EncryptedSite);
            request.AddParameter("tags", entity.EncryptedTagDTOs);
            return await client.ExecuteAsync<InfoGroupDTO>(request);
        }

        public async Task<ApiResponse<string>> SignUp(UserDTO User, string password)
        {
            BaseRequest request = new BaseRequest("api/sign-up/", RestSharp.Method.Post);
            request.AddParameter("username", User.UserName);
            request.AddParameter("password", password);
            return await client.ExecuteAsync<string>(request);
        }

        public async Task<ApiResponse<string>> Login(UserDTO User, string password)
        {
            BaseRequest request = new BaseRequest("api/login/", RestSharp.Method.Post);
            request.AddParameter("username", User.UserName);
            request.AddParameter("password", password);
            request.AddParameter("device", "windows");
            return await client.ExecuteAsync<string>(request);
        }
    }
}
