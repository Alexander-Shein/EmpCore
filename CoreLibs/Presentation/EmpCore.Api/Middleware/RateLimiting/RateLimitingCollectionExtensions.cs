using Microsoft.Extensions.DependencyInjection;

namespace EmpCore.Api.Middleware.RateLimiting;

public static class RateLimitingCollectionExtensions
{
    public static IServiceCollection AddRateLimiting(this IServiceCollection services)
    {
        return services;
    }
}
