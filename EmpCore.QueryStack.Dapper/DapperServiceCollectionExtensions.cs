using Microsoft.Extensions.DependencyInjection;

namespace EmpCore.QueryStack.Dapper;

public static class DapperServiceCollectionExtensions
{
    public static IServiceCollection AddConnectionFactory(this IServiceCollection services, string connectionString)
    {
        services
            .AddSingleton(new ReadOnlyConnectionString(connectionString))
            .AddScoped<IConnectionFactory, SqlConnectionFactory>();

        return services;
    }
}