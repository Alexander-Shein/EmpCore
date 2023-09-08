using System.Reflection;
using EmpCore.Application.Middleware.Transactions;
using Microsoft.Extensions.DependencyInjection;

namespace EmpCore.Application;

public static class ApplicationCollectionExtensions
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services,
        params Assembly[] applicationAssemblies)
    {
        services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(applicationAssemblies))
            .AddUnitOfWorkBehavior();
        
        return services;
    }
}
