using RestSharp;
using SuperPassword.DAL.Implementations.Models;
using System.Net;

namespace SuperPassword.DAL.Online.Clinet
{
    public class HttpRestClient : RestClient
    {
        public HttpRestClient(string apiUrl) : base(initOptions())
        {
            SetCSRFHeader();
        }

        private static RestClientOptions initOptions()
        {
            RestClientOptions options = new RestClientOptions();
            options.CookieContainer = new CookieContainer();
            return options;
        }

        private void SetCSRFHeader()
        {
            BaseRequest request = new BaseRequest("api/csrf", Method.Get);
            var response = this.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                CookieCollection responseCookies = response.Cookies ?? new CookieCollection();
                foreach (Cookie cookie in responseCookies)
                {
                    //client.Options.CookieContainer?.Add(cookie);
                    if (cookie.Name == "csrftoken")
                        this.AddDefaultHeader("X-CSRFToken", cookie.Value);
                }
            }
        }

        private Entity.Interface.ResponseStatus AdaptStatus(HttpStatusCode httpStatusCode)
        {
            switch (httpStatusCode)
            {
                case HttpStatusCode.OK: return Entity.Interface.ResponseStatus.Success;
                default: return Entity.Interface.ResponseStatus.NoContent;
            }
        }

        public async Task<DALResponse> ExecuteAsync(BaseRequest request)
        {
            RestResponse response = await this.ExecuteAsync<RestResponse>(request);
            return new DALResponse { Status = AdaptStatus(response.StatusCode), Content = response.Content };
        }

        public DALResponse ExecuteSync(BaseRequest request)
        {
            RestResponse response = this.Execute(request);
            return new DALResponse { Status = AdaptStatus(response.StatusCode), Content = response.Content };
        }

    }
}
