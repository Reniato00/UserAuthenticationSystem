using AuthenticationSystemApi.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthenticationSystemApi.Utils
{
    public interface IAuthorizationFactory
    {
        public string GenerateJwtToken();
        public bool ValidateJwtToken(string token);
        void DecodeJwt(string jwt);
    }

    public class AuthorizationFactory : IAuthorizationFactory
    {
        private readonly AuthorizationSettings settings;
        private readonly IHttpContextAccessor contextAccessor;
        public AuthorizationFactory(IOptions<AuthorizationSettings> settings, IHttpContextAccessor contextAccessor)
        {
            this.settings = settings.Value;
            this.contextAccessor = contextAccessor;

        }
        public string GenerateJwtToken()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
            issuer: settings.Issuer,
            audience: settings.Audience,
            claims: new List<Claim>(),
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool ValidateJwtToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(settings.Secret);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = settings.Issuer,
                    ValidAudience = settings.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public void DecodeJwt(string jwt)
        {
            var handler = new JwtSecurityTokenHandler();
            if (handler.CanReadToken(jwt))
            {
                var token = handler.ReadJwtToken(jwt);
                var claims = token.Claims.ToList();
                var identity = new ClaimsIdentity(claims, "jwt");
                var principal = new ClaimsPrincipal(identity);
                contextAccessor!.HttpContext!.User = principal;
                Console.WriteLine("JWT successfully decoded and claims added to HttpContext.User");
            }
            else
            {
                Console.WriteLine("Invalid JWT");
            }
        }
    }
}
