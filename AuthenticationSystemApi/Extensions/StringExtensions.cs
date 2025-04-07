using Microsoft.Extensions.Primitives;

namespace AuthenticationSystemApi.Extensions
{
    public static class StringExtensions
    {
        public static int ToInt(this StringValues? value) => int.Parse(value.ToString());
    }
}
