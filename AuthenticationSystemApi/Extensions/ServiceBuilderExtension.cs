using AuthenticationSystemApi.Middlewares;
using AuthenticationSystemApi.Models;
using AuthenticationSystemApi.Services;
using AuthenticationSystemApi.Utils;

namespace AuthenticationSystemApi.Extensions
{
    public static class ServiceBuilderExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IPersonServices, PersonServices>();
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<ITokenServices, TokenServices>();
            services.AddScoped<IProjectServices, ProjectServices>();
            services.AddScoped<Variables>();
            return services;
        }

        public static IServiceCollection AddFactories(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationFactory, AuthorizationFactory>();
            return services;
        }

        public static IApplicationBuilder AddCustomMiddlewares(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthorizationMiddleware>();
        }

        public static IServiceCollection AddCustomFilters(this IServiceCollection services)
        {
            services.AddScoped<ApiProcessingFilter>();
            services.AddMemoryCache();
            return services;
        }
    }
}
