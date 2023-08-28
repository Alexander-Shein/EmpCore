using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace EmpCore.WebApi.Swagger
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSwaggerDocs(this IServiceCollection services, SwaggerOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));
            services.AddSingleton(options);
            if (!options.Enabled) return services;
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(options.Name, new OpenApiInfo { Title = options.Title, Version = options.Version });
                if (options.OAuth2 != null)
                {
                    c.AddSecurityDefinition("OAuth2", new OpenApiSecurityScheme
                    {
                        Type = SecuritySchemeType.OAuth2,
                        Flows = new OpenApiOAuthFlows
                        {
                            Implicit = new OpenApiOAuthFlow
                            {
                                AuthorizationUrl = new Uri(options.OAuth2.AuthorizationUrl),
                                TokenUrl = new Uri(options.OAuth2.TokenUrl),
                                Scopes = options.OAuth2.Scopes.ToDictionary(x => x, x => x)
                            }
                        }
                    });

                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference {Type = ReferenceType.SecurityScheme, Id = "OAuth2"}
                            },
                            options.OAuth2.Scopes
                        }
                    });
                }
                else if (options.Jwt != null)
                {
                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Description =
                            "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey
                    });
                }
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerDocs(this IApplicationBuilder builder)
        {
            var options = builder.ApplicationServices.GetService<SwaggerOptions>();
            if (options == null) throw new ArgumentNullException(nameof(options));
            if (!options.Enabled) return builder;

            var routePrefix = string.IsNullOrWhiteSpace(options.RoutePrefix) ? string.Empty : options.RoutePrefix;

            builder.UseStaticFiles()
                .UseSwagger(c =>
                {
                    c.RouteTemplate = string.Concat(routePrefix, "/{documentName}/swagger.json");
                    c.SerializeAsV2 = options.SerializeAsOpenApiV2;
                });

            return builder.UseSwaggerUI(c =>
            {
                c.RoutePrefix = routePrefix;
                c.OAuthClientId(options.OAuth2.ClientId);
                c.OAuthAppName(options.Name);
                c.SwaggerEndpoint($"/{routePrefix}/{options.Name}/swagger.json".FormatEmptyRoutePrefix(),
                    options.Title);
                c.DisplayRequestDuration();
                c.EnablePersistAuthorization();
                c.EnableTryItOutByDefault();
            });
        }

        /// <summary>
        ///     Replaces leading double forward slash caused by an empty route prefix
        /// </summary>
        /// <param name="route"></param>
        /// <returns></returns>
        private static string FormatEmptyRoutePrefix(this string route)
        {
            return route.Replace("//", "/");
        }
    }
}