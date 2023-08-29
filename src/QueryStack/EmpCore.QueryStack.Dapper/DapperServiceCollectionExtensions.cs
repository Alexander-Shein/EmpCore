using Microsoft.Extensions.DependencyInjection;

namespace EmpCore.QueryStack.Dapper;

public static class DapperServiceCollectionExtensions
{
    public static IServiceCollection AddConnectionFactory(
        this IServiceCollection services,
        string readOnlySqlConnectionString)
    {
        services
            .AddSingleton(new ReadOnlyConnectionString(readOnlySqlConnectionString))
            .AddScoped<IConnectionFactory, SqlConnectionFactory>();

        return services;
    }
}