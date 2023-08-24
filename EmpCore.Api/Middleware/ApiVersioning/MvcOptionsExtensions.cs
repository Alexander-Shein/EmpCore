using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace EmpCore.Api.Middleware.ApiVersioning;

public static class MvcOptionsExtensions
{
    public static void UseGeneralRoutePrefix(this MvcOptions opts, IRouteTemplateProvider routeAttribute)
    {
        opts.Conventions.Add(new RoutePrefixConvention(routeAttribute));
    }

    public static void UseGeneralRoutePrefix(this MvcOptions opts, string prefix)
    {
        opts.UseGeneralRoutePrefix(new RouteAttribute(prefix));
    }
}