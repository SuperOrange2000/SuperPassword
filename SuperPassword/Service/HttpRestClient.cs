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

            BaseRequest request = new BaseRequest("api/csrf/", RestSharp.Method.Get);
            Execute<object>(request);
        }

        public async Task<ApiResponse<T>> ExecuteAsync<T>(BaseRequest request)
        {
            request.AddHeader("Content-Type", request.ContentType);

            if (request.Parameters.Count != 0)
                foreach (var kvp in request.Parameters)
                {
                    if(kvp.Value is List<string>)
                        foreach (var kvp2 in (List<string>)kvp.Value)
                            request.AddParameter(kvp.Key, kvp2, ParameterType.GetOrPost);
                    else
                        request.AddParameter(kvp.Key,kvp.Value, ParameterType.GetOrPost);
                }
            //request.AddJsonBody(JsonConvert.SerializeObject(baseRequest.Parameters));
            var response = await client.ExecuteAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                CookieCollection responseCookies = response.Cookies ?? new CookieCollection();
                foreach (Cookie cookie in responseCookies)
                {
                    //client.Options.CookieContainer?.Add(cookie);
                    if (cookie.Name == "csrftoken")
                        client.AddDefaultHeader("X-CSRFToken", cookie.Value);
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

        public ApiResponse<T> Execute<T>(BaseRequest request)
        {
            request.AddHeader("Content-Type", request.ContentType);

            if (request.Parameters.Count != 0)
                foreach (var kvp in request.Parameters)
                {
                    request.AddParameter(kvp.Key, kvp.Value, ParameterType.GetOrPost);
                }
            //request.AddJsonBody(JsonConvert.SerializeObject(baseRequest.Parameters));
            var response = client.Execute(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                CookieCollection responseCookies = response.Cookies ?? new CookieCollection();
                foreach (Cookie cookie in responseCookies)
                {
                    //client.Options.CookieContainer?.Add(cookie);
                    if (cookie.Name == "csrftoken")
                        client.AddDefaultHeader("X-CSRFToken", cookie.Value);
                }
            }
            var result = JsonConvert.DeserializeObject<ApiResponse<T>>(response.Content);
            if (result != null)
                result.Status = response.StatusCode;
            return result;
        }
    }
}
