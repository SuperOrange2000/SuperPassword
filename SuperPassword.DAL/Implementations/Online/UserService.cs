using RestSharp;
using SuperPassword.DAL.Interfaces.Online;
using SuperPassword.DAL.Online.Client;
using SuperPassword.Entity.Interface;
using System.Net;

namespace SuperPassword.DAL.Implementations.Online
{
    public partial class OnlineService
    {

        public async Task<IOnlineResponse<LoginResponse>> SignUpAsync(string name, string password)
        {
            BaseRequest request = new BaseRequest("sign-up", RestSharp.Method.Post);
            request.AddJsonBody(new
            {
                account = name,
                password = password,
                device = "windows",
            });
            var response = await client.RequestAsync<LoginResponse>(request);
            if (response.NetworkStatusCode == HttpStatusCode.OK && response.Content != null)
            {
                BaseRequest.SetToken(response.Content.Token);
            }
            return response;
        }

        public async Task<IOnlineResponse<LoginResponse>> LoginAsync(long id, string password)
        {
            BaseRequest request = new BaseRequest("login", RestSharp.Method.Post);
            request.AddJsonBody(new
            {
                id = id,
                password = password,
                device = "windows",
            });
            var response = await client.RequestAsync<LoginResponse>(request);
            if (response.NetworkStatusCode == HttpStatusCode.OK && response.Content != null)
            {
                BaseRequest.SetToken(response.Content.Token);
            }
            return response;
        }
    }
}
