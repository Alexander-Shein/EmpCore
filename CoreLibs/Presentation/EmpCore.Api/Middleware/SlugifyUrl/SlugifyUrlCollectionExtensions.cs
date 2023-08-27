using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.DependencyInjection;

namespace EmpCore.Api.Middleware.SlugifyUrl;

public static class SlugifyUrlCollectionExtensions
{
    public static IServiceCollection SlugifyWebApiUrls(this IServiceCollection services)
    {
        services
            .AddControllers(options =>
                options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer())));

        return services;
    }
}