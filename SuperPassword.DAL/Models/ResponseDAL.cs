using System.Net;

namespace SuperPassword.DAL.Models
{
    public class ResponseDAL
    {
        public HttpStatusCode Status { get; set; }

        public string? Content { get; set; }
    }
}
