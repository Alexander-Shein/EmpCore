﻿using MediatR;

namespace EmpCore.Application.Middleware.Caching;

public abstract class CachePolicy<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public virtual DateTimeOffset? AbsoluteExpiration => null;
    public virtual TimeSpan? AbsoluteExpirationRelativeToNow => TimeSpan.FromMinutes(5);
    public virtual TimeSpan? SlidingExpiration => TimeSpan.FromSeconds(30);

    public virtual string GetCacheKey(TRequest request)
    {
        var r = new { request };
        var props = r.request.GetType().GetProperties().Select(pi => $"{pi.Name}:{pi.GetValue(r.request, null)}");
        return $"{typeof(TRequest).FullName}{{{String.Join(",", props)}}}";
    }
}