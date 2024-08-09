using SuperPassword.BLL.Interfaces.Models;
using SuperPassword.Entity.Interface;
using System.Text.Json.Serialization;

namespace SuperPassword.BLL.Implementations.Models
{
    public class BLLResponse<T> : IBLLResponse<T>
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("content")]
        public T? Content { get; set; }
        public ResponseStatus Status { get; set; }
    }

    public class BLLResponse : IBLLResponse
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("content")]
        public object? Content { get; set; }
        public ResponseStatus Status { get; set; }
    }
}
