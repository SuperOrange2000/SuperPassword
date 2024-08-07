using SuperPassword.DAL.OnlineService.Clinet;
using SuperPassword.Entity.Interface;
using SuperPassword.DAL.Models;

namespace SuperPassword.DAL.OnlineService
{
    public class UserServiceOnline : IUserServiceDAL
    {
        private readonly HttpRestClient client;

        public UserServiceOnline(HttpRestClient client)
        {
            this.client = client;
        }

        public async Task<ResponseDAL> SignUp(IUser User)
        {
            BaseRequest request = new BaseRequest("api/sign-up", RestSharp.Method.Post);
            request.AddParameter("username", User.Name);
            request.AddParameter("password", User.Password);
            return await client.ExecuteAsync(request);
        }

        public async Task<ResponseDAL> Login(IUser User)
        {
            BaseRequest request = new BaseRequest("api/login", RestSharp.Method.Post);
            request.AddParameter("username", User.Name);
            request.AddParameter("password", User.Password);
            request.AddParameter("device", "windows");
            return await client.ExecuteAsync(request);
        }
    }
}
