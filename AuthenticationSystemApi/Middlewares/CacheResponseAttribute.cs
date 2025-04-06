using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;

namespace AuthenticationSystemApi.Middlewares
{
    public class CacheResponseAttribute : Attribute, IActionFilter
    {
        private readonly int duration;
        public CacheResponseAttribute(int duration) => this.duration = duration;

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is ObjectResult objectResult)
            {
                var cache = context.HttpContext.RequestServices.GetRequiredService<IMemoryCache>();
                var cacheKey = context.HttpContext.Request.Path.ToString();
                cache.Set(cacheKey, objectResult.Value, TimeSpan.FromSeconds(duration));
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var cache = context.HttpContext.RequestServices.GetRequiredService<IMemoryCache>();
            var cacheKey = context.HttpContext.Request.Path.ToString();

            if(cache.TryGetValue(cacheKey, out object? cachedValue))
            {
                context.Result = new JsonResult(cachedValue);
            }
        }
    }
}
