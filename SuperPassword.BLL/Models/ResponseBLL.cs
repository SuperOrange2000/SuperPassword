using System.Net;
using System.Text.Json.Serialization;

namespace SuperPassword.BLL.Models
{
    public class ResponseBLL<T>
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }

        public HttpStatusCode Status { get; set; }
        [JsonPropertyName("content")]
        public T? Content { get; set; }
    }
}
