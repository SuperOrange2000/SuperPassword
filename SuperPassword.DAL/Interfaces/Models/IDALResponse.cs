using SuperPassword.Entity.Interface;
using System.Net;

namespace SuperPassword.DAL.Interfaces.Models
{


    public interface IDALResponse<T>
    {
        public HttpStatusCode? NetworkStatusCode { get; set; }
        public ResponseDataStatus? DataStatus { get; set; }
        public string? ServerMessage { get; set; }
        public T? Content { get; set; }
    }

    public interface IDALResponse : IDALResponse<object> { }
}
