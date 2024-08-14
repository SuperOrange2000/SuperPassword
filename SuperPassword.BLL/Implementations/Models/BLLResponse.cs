using Mapster;
using SuperPassword.BLL.Interfaces.Models;
using SuperPassword.DAL.Interfaces.Models;
using SuperPassword.Entity.Interface;
using System.Net;

namespace SuperPassword.BLL.Implementations.Models
{
    public class BLLResponse<T> : IBLLResponse<T>
    {
        public HttpStatusCode? NetworkStatusCode { get; set; }
        public string? ServerMessage { get; set; }
        public T? Content { get; set; }
        public ResponseDataStatus? DataStatus { get; set; }

        public BLLResponse() { }

        public BLLResponse(IDALResponse DALResponse)
        {
            DALResponse.Adapt(this);
        }
    }

    public class BLLResponse : BLLResponse<object>, IBLLResponse
    {
        public BLLResponse() { }
        public BLLResponse(IDALResponse DALResponse) : base(DALResponse) { }
    }
}
