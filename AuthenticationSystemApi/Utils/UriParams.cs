using AuthenticationSystemApi.Extensions;
using Microsoft.Extensions.Primitives;

namespace AuthenticationSystemApi.Utils
{
    public interface IUriParams
    {
        public StringValues? this[string key]
        {
            get;
        }
    }

    class UriParams : IUriParams
    {
        private readonly IHttpContextAccessor context;
        public UriParams(IHttpContextAccessor context)
        {
            this.context = context;
        }

        public StringValues? this[string key] =>GetUriParams(key);

        private StringValues? GetUriParams(string key)
        {
            if (context.HttpContext?.Request.Query.TryGetValue(key, out var queryValue) ?? false)
                return queryValue;

            else if (context.HttpContext?.Request.RouteValues.TryGetValue(key, out var routeValue) ?? false)
                return routeValue?.ToString();
            else return default;
        }
    }

    public static class IUriExtensions
    {
        public static StringValues? Example(this IUriParams uriParams) => uriParams["example"];
        
        public static int? Skip(this IUriParams uriParams) => uriParams["skip"].ToInt();

        public static int? Take(this IUriParams uriParams) => uriParams["take"].ToInt();
    }
}