using SuperPassword.DAL.Interfaces.Online;
using System.Net;
using System.Text.Json.Serialization;

namespace SuperPassword.DAL.Implementations.Online
{
    public class OnlineResponse<T> : OnlineResponse, IOnlineResponse<T>
    {
        [JsonPropertyName("content")]
        public T Content { get; set; }
    }

    public class OnlineResponse : IOnlineResponse
    {
        public HttpStatusCode? NetworkStatusCode { get; set; }

        [JsonPropertyName("message")]
        public string? ServerMessage { get; set; }
    }

}
