using RestSharp;
using SuperPassword.Config.Service;
using SuperPassword.DAL.Implementations.Online;
using System.Net;
using System.Text.Json;

namespace SuperPassword.DAL.Online.Client
{
    public class HttpRestClient : RestClient
    {
        public HttpRestClient(IConfigService configService) : base(initOptions())
        {
            BaseRequest.SetBaseUri(configService.AppConfig.ApiUrl);
            //SetCSRFHeader();

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

        public async Task<OnlineResponse<T>> RequestAsync<T>(BaseRequest request)
        {
            RestResponse response = await this.ExecuteAsync<RestResponse>(request);
            var result = Deserialize<T>(response.Content);
            result.NetworkStatusCode = response.StatusCode;
            return result;
        }

        public async Task<OnlineResponse> RequestAsync(BaseRequest request)
        {
            RestResponse response = await this.ExecuteAsync<RestResponse>(request);
            var result = Deserialize(response.Content);
            result.NetworkStatusCode = response.StatusCode;
            return result;
        }

        public OnlineResponse<T> RequestSync<T>(BaseRequest request)
        {
            RestResponse response = this.Execute<RestResponse>(request);
            var result = Deserialize<T>(response.Content);
            result.NetworkStatusCode = response.StatusCode;
            return result;
        }

        public OnlineResponse RequestSync(BaseRequest request)
        {
            RestResponse response = this.Execute<RestResponse>(request);
            var result = Deserialize(response.Content);
            result.NetworkStatusCode = response.StatusCode;
            return result;
        }

        private OnlineResponse<T> Deserialize<T>(string? jsonText)
        {
            if (string.IsNullOrEmpty(jsonText))
            {
                OnlineResponse<T> result = new OnlineResponse<T>();
                return result;
            }
            else
            {
                OnlineResponse<T>? result;
                try
                {
                    result = JsonSerializer.Deserialize<OnlineResponse<T>>(jsonText);
                    if (result == null)
                        throw new NullReferenceException();
                }
                catch (Exception ex) when (ex is JsonException || ex is NullReferenceException)
                {
                    Console.WriteLine(ex.Message);
                    result = new OnlineResponse<T>()
                    {
                        ErrorMessage = jsonText
                    };
                }
                finally
                {

                }
                return result;
            }
        }

        private OnlineResponse Deserialize(string? jsonText)
        {
            if (string.IsNullOrEmpty(jsonText))
            {
                OnlineResponse result = new OnlineResponse();
                return result;
            }
            else
            {
                OnlineResponse? result;
                try
                {
                    result = JsonSerializer.Deserialize<OnlineResponse>(jsonText);
                    if (result == null)
                        throw new NullReferenceException();
                }
                catch (Exception ex) when (ex is JsonException || ex is NullReferenceException)
                {
                    Console.WriteLine(ex.Message);
                    result = new OnlineResponse()
                    {
                        ErrorMessage = jsonText
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
