using System.Text.Json.Serialization;

namespace SuperPassword.DAL.Implementations.Models
{
    internal class ServerResponse<T>
    {
        [JsonPropertyName("message")]
        internal string Message { get; set; }

        [JsonPropertyName("content")]
        internal T Content { get; set; }
    }
}
