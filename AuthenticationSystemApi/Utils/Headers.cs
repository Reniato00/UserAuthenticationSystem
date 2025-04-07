using Microsoft.Extensions.Primitives;

namespace AuthenticationSystemApi.Utils
{
    public interface IHeaders
    {
        public StringValues? this[string key] { get; }
    }

    public class Headers : IHeaders
    {
        private readonly IHttpContextAccessor context;

        public Headers(IHttpContextAccessor context)
        {
            this.context = context;
        }

        public StringValues? this[string key] =>
            (context.HttpContext?.Request.Headers.TryGetValue(key, out var value)?? false) ? value : default;
    }

    public static class IheadersExtensions
    {
        public static StringValues? Example(this IHeaders headers) => headers["example"];
    }
}
