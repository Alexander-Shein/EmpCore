﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;

namespace EmpCore.Api.Middleware.Security;

public static class SecurityCollectionExtensions
{
    public static IServiceCollection AddPrincipalUser(this IServiceCollection services)
    {
        services
            .AddHttpContextAccessor()
            .AddScoped<IPrincipalUser, PrincipalUser>();

        return services;
    }

    public static IServiceCollection AddApiAuth(this IServiceCollection services, Uri identityServerUrl, string audience)
    {
        if (identityServerUrl == null) throw new ArgumentNullException(nameof(identityServerUrl));
        if (String.IsNullOrWhiteSpace(audience))
        {
            if (audience == null) throw new ArgumentNullException(nameof(audience));
            throw new ArgumentException("Cannot be empty.", nameof(audience));
        }

        services
            .AddAuthorization()
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = identityServerUrl.OriginalString;
                options.Audience = audience;
                options.RequireHttpsMetadata = true;
            });

        return services;
    }
}