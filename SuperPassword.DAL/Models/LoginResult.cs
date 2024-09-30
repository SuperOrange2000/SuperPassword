using System.Text.Json.Serialization;

namespace SuperPassword.DAL.Models
{
    public class LoginResult
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }
        [JsonPropertyName("id")]
        public long Id { get; set; }
    }
}
