using SuperPassword.DAL.Interfaces.Online;
using SuperPassword.DAL.Online.Client;
using SuperPassword.Entity.Interface;
using System.Net;

namespace SuperPassword.DAL.Implementations.Online
{
    public partial class OnlineService
    {

        public async Task<IOnlineResponse> SignUpAsync(IUser user)
        {
            BaseRequest request = new BaseRequest("api/sign-up", RestSharp.Method.Post);
            request.AddParameter("username", user.Name);
            request.AddParameter("password", user.Password);
            var response = await client.RequestAsync<string>(request);
            if (response.NetworkStatusCode == HttpStatusCode.OK && response.Content != null)
            {
                Token = response.Content;
            }
            return response;
        }

        public async Task<IOnlineResponse> LoginAsync(IUser user)
        {
            BaseRequest request = new BaseRequest("api/login", RestSharp.Method.Post);
            request.AddParameter("username", user.Name);
            request.AddParameter("password", user.Password);
            request.AddParameter("device", "windows");
            var response = await client.RequestAsync<string>(request);
            if (response.NetworkStatusCode == HttpStatusCode.OK && response.Content != null)
            {
                Token = response.Content;
            }
            return response;
        }
    }
}
