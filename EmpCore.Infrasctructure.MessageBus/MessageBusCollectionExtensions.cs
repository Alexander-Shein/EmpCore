using Microsoft.Extensions.DependencyInjection;

namespace EmpCore.Infrastructure.MessageBus.CAP;

public static class MessageBusCollectionExtensions
{
    public static IServiceCollection AddCapMessageBus(
        this IServiceCollection services, string sqlServerConnectionString, string azureServiceBusConnectionString)
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
            .AddCap(options =>
            {
                options.UseSqlServer(sqlServerConnectionString);
                options.UseAzureServiceBus(azureServiceBusConnectionString);
            });

        return services;
    }
}