namespace AuthenticationSystemApi.Models
{
    public class AuthorizationSettings
    {
        public const string JwtSettings = nameof(JwtSettings);

        public string Secret { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
    }
}
