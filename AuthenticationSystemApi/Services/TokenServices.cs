using AuthenticationSystemApi.Models;
using AuthenticationSystemApi.Utils;

namespace AuthenticationSystemApi.Services
{
    public interface ITokenServices
    {
        Authorization GetJwt();
    }

    public class TokenServices : ITokenServices
    {
        private readonly IAuthorizationFactory auth;
        public TokenServices(IAuthorizationFactory auth) 
        {
            this.auth = auth ?? throw new ArgumentNullException(nameof(auth));
        }

        public Authorization GetJwt()
        {
            var jwt = auth.GenerateJwtToken();
            return new() { Jwt = jwt};
        }
    }
}
