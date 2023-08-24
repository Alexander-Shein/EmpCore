using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace EmpCore.Application.Middleware.Caching;

public class CachingPipelineBehavior<TRequest, TResponse> :
    IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IDistributedCache _distributedCache;
    private readonly IEnumerable<CachePolicy<TRequest, TResponse>> _cachePolicies;

    public CachingPipelineBehavior(
        IDistributedCache distributedCache,
        IEnumerable<CachePolicy<TRequest, TResponse>> cachePolicies)
    {
        _distributedCache = distributedCache ?? throw new ArgumentNullException(nameof(distributedCache));
        _cachePolicies = cachePolicies ?? throw new ArgumentNullException(nameof(cachePolicies));
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken ct)
    {
        var cachePolicy = _cachePolicies.FirstOrDefault();
        if (cachePolicy == null)
        {
            return await next().ConfigureAwait(false);
        }

        var cacheKey = cachePolicy.GetCacheKey(request);
        var cachedJson = await _distributedCache.GetStringAsync(cacheKey, ct).ConfigureAwait(false);

        if (cachedJson != null)
        {
            var cachedResponse = JsonSerializer.Deserialize<TResponse>(cachedJson);
            return cachedResponse;
        }

        var response = await next().ConfigureAwait(false);

        var options = new DistributedCacheEntryOptions
        {
            SlidingExpiration = cachePolicy.SlidingExpiration,
            AbsoluteExpiration = cachePolicy.AbsoluteExpiration,
            AbsoluteExpirationRelativeToNow = cachePolicy.AbsoluteExpirationRelativeToNow
        };
        var responseJson = JsonSerializer.Serialize(response);
        await _distributedCache.SetStringAsync(cacheKey, responseJson, options, ct).ConfigureAwait(false);
        return response;
    }
}
