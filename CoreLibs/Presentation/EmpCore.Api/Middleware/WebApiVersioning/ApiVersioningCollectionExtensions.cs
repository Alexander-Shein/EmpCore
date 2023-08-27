using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace EmpCore.Api.Middleware.WebApiVersioning;

public static class WebApiVersioningCollectionExtensions
{
    public static IServiceCollection AddWebApiVersioning(this IServiceCollection services)
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