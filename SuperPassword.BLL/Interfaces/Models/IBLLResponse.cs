using SuperPassword.Entity.Interface;
using System.Net;

namespace SuperPassword.BLL.Interfaces.Models
{
    public interface IBLLResponse<T>
    {
        public HttpStatusCode? NetworkStatusCode { get; set; }
        public ResponseDataStatus? DataStatus { get; set; }
        public string? ServerMessage { get; set; }
        public T? Content { get; set; }
    }

    public interface IBLLResponse : IBLLResponse<object> { }
}
