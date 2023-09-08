using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace EmpCore.QueryStack.Middleware.Caching;

public static class CachingPipelineServiceCollectionExtensions
{
    public static IServiceCollection AddCachingPipeline(this IServiceCollection services, Assembly queryStackAssembly)
    {
        services.
            AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingPipelineBehavior<,>))
            .AddCachingPolicies(queryStackAssembly);

        return services;
    }
    
    private static IServiceCollection AddCachingPolicies(this IServiceCollection services, Assembly queryStackAssembly)
    {
        var baseType = typeof(CachePolicy<,>);

        foreach (var type in queryStackAssembly
                     .GetTypes()
                     .Where(t => t.IsClass && !t.IsAbstract)
                     .Where(t => t.IsAssignableToGenericType(baseType)))
        {
            var contract = baseType.MakeGenericType(
                type.BaseType.GenericTypeArguments[0], type.BaseType.GenericTypeArguments[1]);

            services.AddTransient(contract, type);
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