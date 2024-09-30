using RestSharp;
using SuperPassword.DAL.Models;
using SuperPassword.DAL.Online.Client;
using System.Net;

namespace SuperPassword.DAL.Implementations.Online
{
    public partial class OnlineService
    {

        public async Task<OnlineResponse<LoginResult>> SignUpAsync(string name, byte[] password)
        {
            BaseRequest request = new BaseRequest("sign-up", Method.Post);
            request.AddJsonBody(new
            {
                account = name,
                password,
                device = "windows",
            });
            var response = await client.RequestAsync<LoginResult>(request);
            if (response.NetworkStatusCode == HttpStatusCode.OK && response.Content != null)
            {
                BaseRequest.SetToken(response.Content.Token);
            }
            return response;
        }

        public async Task<OnlineResponse<LoginResult>> LoginAsync(long id, byte[] password)
        {
            BaseRequest request = new BaseRequest("login", Method.Post);
            request.AddJsonBody(new
            {
                id,
                password,
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
