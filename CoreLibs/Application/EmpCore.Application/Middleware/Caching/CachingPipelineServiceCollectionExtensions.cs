using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EmpCore.Application.Middleware.Caching;

public static class CachingPipelineServiceCollectionExtensions
{
    public static IServiceCollection AddCachingPipeline(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingPipelineBehavior<,>));

        // TODO: Scan Assemblies

        return services;
    }
}