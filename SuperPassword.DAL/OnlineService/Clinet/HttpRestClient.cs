using RestSharp;
using SuperPassword.Entity;
using System.Net;

namespace SuperPassword.DAL.OnlineService.Clinet
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

        public async Task<ResponseDAL> ExecuteAsync(BaseRequest request)
        {
            RestResponse response = await this.ExecuteAsync<RestResponse>(request);
            return new ResponseDAL { Status = response.StatusCode, Content = response.Content };
        }

        public ResponseDAL ExecuteSync(BaseRequest request)
        {
            RestResponse response = this.Execute(request);
            return new ResponseDAL { Status = response.StatusCode, Content = response.Content };
        }

    }
}
