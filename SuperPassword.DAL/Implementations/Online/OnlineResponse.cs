using SuperPassword.DAL.Interfaces.Online;
using System.Net;
using System.Text.Json.Serialization;

namespace SuperPassword.DAL.Implementations.Online
{
    public class OnlineResponse<T> : OnlineResponse, IOnlineResponse<T>
    {
        [JsonPropertyName("content")]
        public T Content { get; set; }

        public OnlineResponse<T2> Convert<T2>(Func<T, T2> implementationFactory)
        {
            return new OnlineResponse<T2>
            {
                NetworkStatusCode = NetworkStatusCode,
                ServerMessage = ServerMessage,
                Content = implementationFactory.Invoke(Content)
            };
        }
    }

    public class OnlineResponse : IOnlineResponse
    {
        public HttpStatusCode? NetworkStatusCode { get; set; }

        [JsonPropertyName("message")]
        public string? ServerMessage { get; set; }
    }
}
