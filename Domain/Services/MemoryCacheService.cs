using Core;
using Microsoft.Extensions.Caching.Memory;

public class MemoryCacheService : ICacheService
{
    private readonly IMemoryCache memoryCache;

    private static readonly MemoryCacheEntryOptions CacheOptions = new MemoryCacheEntryOptions
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1),
        SlidingExpiration = TimeSpan.FromMinutes(10)
    };

    public MemoryCacheService(IMemoryCache memoryCache)
    {
        this.memoryCache = memoryCache;
    }

    public void Set<T>(string key, T value)
    {
        memoryCache.Set(key, value, CacheOptions);
    }

    public bool TryGetValue<T>(string key, out T? value)
    {
        if (memoryCache.TryGetValue(key, out T? memoryCacheValue))
        {
            value = memoryCacheValue;
            return true;
        }

        value = default(T);
        return false;
    }
}