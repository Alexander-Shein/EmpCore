using EmpCore.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace EmpCore.Persistence.EntityFrameworkCore;

public static class EFCoreServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, string connectionString)
    {
        services
            .AddScoped(_ => new ApplicationDbContext(connectionString))
            .AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}