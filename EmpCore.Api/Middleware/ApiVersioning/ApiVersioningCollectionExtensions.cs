using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace EmpCore.Api.Middleware.ApiVersioning;

public static class ApiVersioningCollectionExtensions
{
    public static IServiceCollection AddApiVersioning(this IServiceCollection services)
    {
        services.AddControllersWithViews(o => {
            o.UseGeneralRoutePrefix("v{version:apiVersion}");
        });

        services.AddApiVersioning(o => {
            o.AssumeDefaultVersionWhenUnspecified = true;
            o.DefaultApiVersion = new ApiVersion(1, 0);
        });

        return services;
    }
}