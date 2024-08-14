using RestSharp;
using SuperPassword.DAL.Implementations.Models;
using System.Net;
using System.Text.Json;

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

        public async Task<DALResponse<T>> RequestAsync<T>(BaseRequest request)
        {
            RestResponse response = await this.ExecuteAsync<RestResponse>(request);
            var ResponseBody = Deserialize<T>(response.Content);
            return new DALResponse<T>
            {
                NetworkStatusCode = response.StatusCode,
                Content = ResponseBody.Content,
                ServerMessage = ResponseBody.Message
            };
        }

        public async Task<DALResponse> RequestAsync(BaseRequest request)
        {
            RestResponse response = await this.ExecuteAsync<RestResponse>(request);
            var ResponseBody = Deserialize<object>(response.Content);
            return new DALResponse
            {
                NetworkStatusCode = response.StatusCode,
                ServerMessage = ResponseBody.Message
            };
        }

        public DALResponse<T> RequestSync<T>(BaseRequest request)
        {
            RestResponse response = this.Execute<RestResponse>(request);
            var ResponseBody = Deserialize<T>(response.Content);
            return new DALResponse<T>
            {
                NetworkStatusCode = response.StatusCode,
                Content = ResponseBody.Content,
                ServerMessage = ResponseBody.Message
            };
        }

        public DALResponse RequestSync(BaseRequest request)
        {
            RestResponse response = this.Execute<RestResponse>(request);
            var ResponseBody = Deserialize<object>(response.Content);
            return new DALResponse
            {
                NetworkStatusCode = response.StatusCode,
                ServerMessage = ResponseBody.Message
            };
        }

        private ServerResponse<T> Deserialize<T>(string? jsonText)
        {
            if (string.IsNullOrEmpty(jsonText))
            {
                ServerResponse<T> result = new ServerResponse<T>();
                return result;
            }
            else
            {
                ServerResponse<T>? result;
                try
                {
                    result = JsonSerializer.Deserialize<ServerResponse<T>>(jsonText);
                    if (result == null)
                        throw new NullReferenceException();
                }
                catch (Exception ex) when (ex is JsonException || ex is NullReferenceException)
                {
                    Console.WriteLine(ex.Message);
                    result = new ServerResponse<T>()
                    {
                        Message = jsonText
                    };
                }
                finally
                {

                }
                return result;
            }
        }
    }
}
