using Newtonsoft.Json;
using RestSharp;
using SuperPassword.Shared.Contact;
using System;
using System.Net;
using System.Threading.Tasks;

namespace SuperPassword.Service
{
    public class HttpRestClient
    {
        private readonly string apiUrl;
        protected readonly RestClient client;

        public HttpRestClient(string apiUrl)
        {
            this.apiUrl = apiUrl;
            RestClientOptions options = new RestClientOptions();
            options.CookieContainer = new CookieContainer();
            client = new RestClient(options);
        }

        public async Task<ApiResponse<T>> ExecuteAsync<T>(BaseRequest baseRequest)
        {
            var request = new RestRequest(new Uri(apiUrl + baseRequest.Route), baseRequest.Method);
            request.AddHeader("Content-Type", baseRequest.ContentType);

            if (baseRequest.Parameters.Count != 0)
                //foreach (var kvp in baseRequest.Parameters)
                //{
                //    request.AddParameter(kvp.Key, kvp.Value, ParameterType.GetOrPost);
                //}
                request.AddJsonBody(JsonConvert.SerializeObject(baseRequest.Parameters));
            var response = await client.ExecuteAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                CookieCollection responseCookies = response.Cookies ?? new CookieCollection();
                foreach (Cookie cookie in responseCookies)
                {
                    client.Options.CookieContainer?.Add(cookie);
                    if (cookie.Name == "csrftoken")
                        client.AddDefaultHeader("X-CSRFToken", cookie.Value);
                }
                return JsonConvert.DeserializeObject<ApiResponse<T>>(response.Content);
            }
            else
                return new ApiResponse<T>()
                {
                    Status = "",
                    Message = response.ErrorMessage ?? ""
                };
        }
    }
}
