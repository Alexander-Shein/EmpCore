using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace EmpCore.Application.Middleware.DomainEventsDispatcher;

public static class SecurityCollectionExtensions
{
    public static IServiceCollection AddDomainEventsDispatcherPipeline(this IServiceCollection services)
    {
        services
            .AddScoped<DomainEventsDispatcher>()
            .AddScoped<IDomainEventsDispatcher>(s => s.GetService<DomainEventsDispatcher>())
            .AddScoped<IDomainEventsHolder>(s => s.GetService<DomainEventsDispatcher>())
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(DomainEventsDispatcherPipelineBehavior<,>));

        return services;
    }
}
