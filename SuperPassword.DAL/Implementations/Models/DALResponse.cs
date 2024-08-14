using SuperPassword.DAL.Interfaces.Models;
using SuperPassword.Entity.Interface;
using System.Net;

namespace SuperPassword.DAL.Implementations.Models
{
    public class DALResponse<T> : IDALResponse<T>
    {
        public HttpStatusCode? NetworkStatusCode { get; set; }
        public ResponseDataStatus? DataStatus { get; set; }
        public string? ServerMessage { get; set; }
        public T? Content { get; set; }
    }

    public class DALResponse : DALResponse<object>, IDALResponse { }
}
