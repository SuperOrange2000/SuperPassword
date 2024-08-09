using SuperPassword.DAL.Interfaces.Models;
using SuperPassword.DAL.Online.Clinet;

namespace SuperPassword.DAL.Implementations.Online
{
    internal partial class OnlineService
    {

        public async Task<IDALResponse> SignUp(string name, string password)
        {
            BaseRequest request = new BaseRequest("api/sign-up", RestSharp.Method.Post);
            request.AddParameter("username", name);
            request.AddParameter("password", password);
            return await client.ExecuteAsync(request);
        }

        public async Task<IDALResponse> Login(string name, string password)
        {
            BaseRequest request = new BaseRequest("api/login", RestSharp.Method.Post);
            request.AddParameter("username", name);
            request.AddParameter("password", password);
            request.AddParameter("device", "windows");
            return await client.ExecuteAsync(request);
        }
    }
}
