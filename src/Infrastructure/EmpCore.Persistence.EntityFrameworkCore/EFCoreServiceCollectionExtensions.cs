using System.Reflection;
using EmpCore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EmpCore.Persistence.EntityFrameworkCore;

public static class EFCoreServiceCollectionExtensions
{
    public static IServiceCollection AddAppDbContext(
        this IServiceCollection services,
        string connectionString,
        Assembly persistenceAssembly)
    {
        services
            .AddScoped(_ => new AppDbContext(connectionString, persistenceAssembly))
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddDomainRepositories(persistenceAssembly);

        new AppDbContext(connectionString, persistenceAssembly).Database.Migrate();

        return services;
    }

    private static IServiceCollection AddDomainRepositories(this IServiceCollection services, params Assembly[] assemblies)
    {
        foreach (var @interface in assemblies
            .SelectMany(a => a.GetTypes())
            .Where(t => t.IsInterface)
            .Where(t => t.IsAssignableToGenericType(typeof(IDomainRepository<,>))))
        {
            var implementations = assemblies
                .SelectMany(s => s.GetTypes())
                .Where(t => t.IsClass && !t.IsAbstract)
                .Where(t => @interface.IsAssignableFrom(t));

            if (!implementations.Any())
                throw new InvalidOperationException($"Cannot find an implementation for '{@interface}'.");

            if (implementations.Count() > 1)
                throw new InvalidOperationException($"Multiple implementation for '{@interface}' is not supported.");

            services.AddTransient(@interface, implementations.Single());
        }

        return services;
    }

    private static bool IsAssignableToGenericType(this Type givenType, Type genericType)
    {
        var interfaceTypes = givenType.GetInterfaces();

        foreach (var it in interfaceTypes)
        {
            if (it.IsGenericType && it.GetGenericTypeDefinition() == genericType)
                return true;
        }

        if (givenType.IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
            return true;

        Type baseType = givenType.BaseType;
        if (baseType == null) return false;

        return IsAssignableToGenericType(baseType, genericType);
    }
}