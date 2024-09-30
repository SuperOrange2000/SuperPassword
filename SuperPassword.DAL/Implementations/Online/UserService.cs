using RestSharp;
using SuperPassword.DAL.Models;
using SuperPassword.DAL.Online.Client;
using System.Net;

namespace SuperPassword.DAL.Implementations.Online
{
    public partial class OnlineService
    {

        public async Task<OnlineResponse<LoginResult>> SignUpAsync(string name, string password)
        {
            BaseRequest request = new BaseRequest("sign-up", RestSharp.Method.Post);
            request.AddJsonBody(new
            {
                account = name,
                password = password,
                device = "windows",
            });
            var response = await client.RequestAsync<LoginResult>(request);
            if (response.NetworkStatusCode == HttpStatusCode.OK && response.Content != null)
            {
                BaseRequest.SetToken(response.Content.Token);
            }
            return response;
        }

        public async Task<OnlineResponse<LoginResult>> LoginAsync(long id, string password)
        {
            BaseRequest request = new BaseRequest("login", RestSharp.Method.Post);
            request.AddJsonBody(new
            {
                id = id,
                password = password,
                device = "windows",
            });
            var response = await client.RequestAsync<LoginResult>(request);
            if (response.NetworkStatusCode == HttpStatusCode.OK && response.Content != null)
            {
                BaseRequest.SetToken(response.Content.Token);
            }
            return response;
        }
    }
}
