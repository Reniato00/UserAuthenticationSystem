using AuthenticationSystemApi.Models;
using AuthenticationSystemApi.Utils;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AuthenticationSystemApi.Middlewares
{
    public class ApiProcessingFilter : IActionFilter
    {
        private readonly IAuthorizationFactory authorizationFactory;
        private readonly Variables vars;
        public ApiProcessingFilter(IAuthorizationFactory authorizationFactory, Variables vars)
        {
            this.authorizationFactory = authorizationFactory;
            this.vars = vars;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("Request successfully executed");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var jwt = context.HttpContext.Request.Headers.Authorization.ToString().Replace("Bearer ", string.Empty);
            authorizationFactory.DecodeJwt(jwt);
            vars.SampleBool = true;
        }
    }
}
