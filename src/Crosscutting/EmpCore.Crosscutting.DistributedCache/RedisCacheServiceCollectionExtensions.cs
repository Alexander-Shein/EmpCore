using Microsoft.Extensions.DependencyInjection;

namespace EmpCore.Crosscutting.DistributedCache;

public static class RedisCacheServiceCollectionExtensions
{
    public static IServiceCollection AddRedisCache(this IServiceCollection services, string server, string instanceName)
    {
        if (String.IsNullOrWhiteSpace(server))
        {
            if (server == null) throw new ArgumentNullException(nameof(server));
            throw new ArgumentException("Cannot be empty.", nameof(server));
        }

        if (String.IsNullOrWhiteSpace(instanceName))
        {
            if (instanceName == null) throw new ArgumentNullException(nameof(instanceName));
            throw new ArgumentException("Cannot be empty.", nameof(instanceName));
        }

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = server;
            options.InstanceName = instanceName;
        });

        return services;
    }
}