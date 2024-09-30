using System.Net;
using System.Text.Json.Serialization;

namespace SuperPassword.DAL.Models
{
    public class OnlineResponse<T> : OnlineResponse
    {
        [JsonPropertyName("data")]
        public T Content { get; set; }

        public OnlineResponse<T2> Convert<T2>(Func<T, T2> implementationFactory)
        {
            return new OnlineResponse<T2>
            {
                NetworkStatusCode = NetworkStatusCode,
                ErrorMessage = ErrorMessage,
                Content = implementationFactory.Invoke(Content)
            };
        }
    }

    public class OnlineResponse
    {
        public HttpStatusCode? NetworkStatusCode { get; set; }

        [JsonPropertyName("errorMsg")]
        public string? ErrorMessage { get; set; }

        [JsonPropertyName("errorCode")]
        public int ErrorCode { get; set; }

        [JsonPropertyName("success")]
        public bool IsSuccess { get; set; }
    }
}
