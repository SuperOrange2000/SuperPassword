using System.Net;

namespace SuperPassword.DAL.Interfaces.Online
{
    public interface IOnlineResponse<T> : IOnlineResponse
    {
        public T Content { get; set; }
    }

    public interface IOnlineResponse
    {
        public HttpStatusCode? NetworkStatusCode { get; set; }
        public string? ServerMessage { get; set; }
    }
}
