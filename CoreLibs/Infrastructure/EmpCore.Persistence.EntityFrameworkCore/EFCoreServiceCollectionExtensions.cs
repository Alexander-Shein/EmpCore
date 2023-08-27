using System.Reflection;
using EmpCore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EmpCore.Persistence.EntityFrameworkCore;

public static class EFCoreServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationDbContext(
        this IServiceCollection services,
        string connectionString,
        Assembly persistenceAssembly)
    {
        services
            .AddScoped(_ => new ApplicationDbContext(connectionString, persistenceAssembly))
            .AddScoped<IUnitOfWork, UnitOfWork>();

        new ApplicationDbContext(connectionString, persistenceAssembly).Database.Migrate();

        return services;
    }
}