using System.Reflection;
using DotNetCore.CAP;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.DependencyInjection;

namespace EmpCore.Infrastructure.MessageBus.CAP;

public static class MessageBusCollectionExtensions
{
    public static IServiceCollection AddCapMessageBus(
        this IServiceCollection services,
        string sqlServerConnectionString,
        string azureServiceBusConnectionString,
        Assembly applicationAssembly)
    {
        if (string.IsNullOrWhiteSpace(sqlServerConnectionString))
        {
            if (sqlServerConnectionString == null) throw new ArgumentNullException(nameof(sqlServerConnectionString));
            throw new ArgumentException("Cannot be empty.", nameof(sqlServerConnectionString));
        }

        if (string.IsNullOrWhiteSpace(azureServiceBusConnectionString))
        {
            if (sqlServerConnectionString == null) throw new ArgumentNullException(nameof(azureServiceBusConnectionString));
            throw new ArgumentException("Cannot be empty.", nameof(azureServiceBusConnectionString));
        }

        services
            .AddTransient<IMessageBus, CapMessageBus>()
            .AddCapSubscribers(applicationAssembly)
            .AddCap(options =>
            {
                options.UseSqlServer(sqlServerConnectionString);
                options.UseAzureServiceBus(opt =>
                {
                    opt.ConnectionString = azureServiceBusConnectionString;
                    opt.TopicPath = "subscribe-application-events"; // TODO: Move to config
                });
            });

        return services;
    }
    
    private static IServiceCollection AddCapSubscribers(this IServiceCollection services, params Assembly[] assemblies)
    {
        var implementations = assemblies
            .SelectMany(s => s.GetTypes())
            .Where(t => t.IsClass && !t.IsAbstract)
            .Where(t => typeof(ICapSubscribe).IsAssignableFrom(t));

        foreach (var implementation in implementations)
        {
            services.AddTransient(implementation);
        }

        return services;
    }
}