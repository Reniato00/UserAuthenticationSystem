using System.Text.Json.Serialization;

namespace AuthenticationSystemApi.Models
{
    public class Authorization
    {
        [JsonPropertyName("jwt")]
        public required string Jwt { get; set; }
    }
}
