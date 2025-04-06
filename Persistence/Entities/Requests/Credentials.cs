using System.Text.Json.Serialization;

namespace Persistence.Entities.Requests
{
    public class Credentials
    {
        [JsonPropertyName("username")]
        public required string UserName { get; set; }

        [JsonPropertyName("password")]
        public required string Password { get; set; }
    }
}
