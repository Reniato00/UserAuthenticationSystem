using Microsoft.AspNetCore.Mvc;

namespace AuthenticationSystemApi.Extensions
{
    public static class StatusCodeExtensions
    {
        public static IActionResult GetStatusCode<T>(this T? response)
        {
            return response != null ? new OkObjectResult(response) : new NotFoundResult();
        }
    }
}
