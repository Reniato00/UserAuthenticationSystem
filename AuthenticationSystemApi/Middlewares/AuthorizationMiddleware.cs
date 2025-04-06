using AuthenticationSystemApi.Utils;
using System.Net;
using System.Text.Json;

namespace AuthenticationSystemApi.Middlewares
{
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IAuthorizationFactory auth;
        public AuthorizationMiddleware(RequestDelegate next, IAuthorizationFactory auth)
        {
            this.next = next;
            this.auth = auth;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var endpoint = httpContext.GetEndpoint();

            if (endpoint?.Metadata?.GetMetadata<Microsoft.AspNetCore.Authorization.AllowAnonymousAttribute>() != null)
            {
                await next(httpContext);
                return;
            }

            if (!httpContext.Request.Headers.ContainsKey("Authorization"))
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
                httpContext.Response.ContentType = "application/json";
                await httpContext.Response.WriteAsync("Jwt is Required");
                return;
            }

            bool isJwtValid = auth.ValidateJwtToken(httpContext.Request.Headers.Authorization.ToString().Replace("Bearer ", string.Empty));
            if (!isJwtValid)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                httpContext.Response.ContentType = "application/json";
                await httpContext.Response.WriteAsync("Jwt is Expired or not Valid");
                return;
            }

            await next(httpContext);
        }
    }
}
