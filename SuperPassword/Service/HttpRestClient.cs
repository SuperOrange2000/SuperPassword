using Newtonsoft.Json;
using RestSharp;
using SuperPassword.Shared.Contact;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace SuperPassword.Service
{
    public class HttpRestClient : RestClient
    {
        public HttpRestClient(string apiUrl) : base(initOptions())
        {
            BaseRequest request = new BaseRequest("api/csrf/", RestSharp.Method.Get);
            ExecuteSync<object>(request);
        }

        private static RestClientOptions initOptions()
        {
            RestClientOptions options = new RestClientOptions();
            options.CookieContainer = new CookieContainer();
            return options;
        }

        public async Task<ApiResponse<T>> ExecuteAsync<T>(BaseRequest request)
        {
            var response = this.Execute(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                CookieCollection responseCookies = response.Cookies ?? new CookieCollection();
                foreach (Cookie cookie in responseCookies)
                {
                    //client.Options.CookieContainer?.Add(cookie);
                    if (cookie.Name == "csrftoken")
                        this.AddDefaultHeader("X-CSRFToken", cookie.Value);
                }
            }

            if (string.IsNullOrEmpty(response.Content))
            {
                ApiResponse<T> result = new ApiResponse<T>();
                result.Status = response.StatusCode;
                return result;
            }
            else
            {
                ApiResponse<T> result = JsonConvert.DeserializeObject<ApiResponse<T>>(response.Content);
                if (result != null)
                    result.Status = response.StatusCode;
                return result;
            }
        }

        public ApiResponse<T> ExecuteSync<T>(BaseRequest request)
        {
            var response = this.Execute(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                CookieCollection responseCookies = response.Cookies ?? new CookieCollection();
                foreach (Cookie cookie in responseCookies)
                {
                    //client.Options.CookieContainer?.Add(cookie);
                    if (cookie.Name == "csrftoken")
                        this.AddDefaultHeader("X-CSRFToken", cookie.Value);
                }
            }
            var result = JsonConvert.DeserializeObject<ApiResponse<T>>(response.Content);
            if (result != null)
                result.Status = response.StatusCode;
            return result;
        }
    }
}
