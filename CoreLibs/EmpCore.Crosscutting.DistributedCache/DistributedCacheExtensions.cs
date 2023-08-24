using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;

namespace EmpCore.Crosscutting.DistributedCache;

public static class DistributedCacheExtensions
{
    public static async Task<T?> GetAsync<T>(this IDistributedCache cache, string key, CancellationToken ct = default)
    {
        if (cache == null) throw new ArgumentNullException(nameof(cache));
        ValidateKey(key);
        var value = await cache.GetAsync(key, ct).ConfigureAwait(false);
        if (value == null) return default;

        var valueString = Encoding.UTF8.GetString(value);
        return JsonSerializer.Deserialize<T>(valueString);
    }

    public static async Task SetAsync(this IDistributedCache cache, string key, object value, TimeSpan? expirationTime = null)
    {
        if (cache == null) throw new ArgumentNullException(nameof(cache));
        if (value == null) throw new ArgumentNullException(nameof(value));
        ValidateKey(key);
        ValidateExpirationTime(expirationTime);

        var serialized = JsonSerializer.Serialize(value);

        if (expirationTime.HasValue)
        {
            await cache.SetStringAsync(key, serialized, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expirationTime.Value
            }).ConfigureAwait(false);
        }
        else
        {
            await cache.SetStringAsync(key, serialized).ConfigureAwait(false);
        }
    }

    public static async Task<T> GetOrSetAsync<T>(this IDistributedCache cache, string key, Func<Task<T>> factory, TimeSpan? expirationTime = null)
    {
        if (cache == null) throw new ArgumentNullException(nameof(cache));
        if (factory == null) throw new ArgumentNullException(nameof(factory));
        ValidateKey(key);
        ValidateExpirationTime(expirationTime);

        var value = await cache.GetAsync<T>(key).ConfigureAwait(false);
        if (value != null)
        {
            return value;
        }

        value = await factory().ConfigureAwait(false);
        await cache.SetAsync(key, value, expirationTime).ConfigureAwait(false);
        return value;
    }

    private static void ValidateKey(string key)
    {
        if (!String.IsNullOrWhiteSpace(key)) return;

        if (key == null) throw new ArgumentNullException(nameof(key));
        throw new ArgumentException("Cannot be empty.", nameof(key));
    }

    private static void ValidateExpirationTime(TimeSpan? expirationTime)
    {
        if (expirationTime.HasValue && expirationTime <= TimeSpan.Zero)
            throw new ArgumentOutOfRangeException(nameof(expirationTime), "Must be greater than 0.");
    }
}