using Microsoft.Extensions.Caching.Memory;
using System.Collections.Concurrent;
using Usta.Infrastructure.Cache.Contracts;

namespace Usta.Infrastructure.Cache.InMemoryCache
{
    public class InMemoryCacheService : ICacheService
    {
        private readonly IMemoryCache _cache;
        private readonly ConcurrentDictionary<string, byte> _cacheKeys;

        public InMemoryCacheService(IMemoryCache cache)
        {
            _cache = cache;
            _cacheKeys = new ConcurrentDictionary<string, byte>();
        }

        public void SetSliding<T>(string key, T data, int expirationTime)
        {
            var options = new MemoryCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromMinutes(expirationTime)
            };

            _cacheKeys.TryAdd(key, 0);

            _cache.Set(key, data, options);
        }

        public T Get<T>(string key)
        {
            return _cache.Get<T>(key)!;
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
            _cacheKeys.TryRemove(key, out _);
        }

        public void Set<T>(string key, T data, int expirationTime)
        {
            var options = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(expirationTime)
            };

            _cacheKeys.TryAdd(key, 0);

            _cache.Set(key, data, options);
        }

        public void ClearAll()
        {
            foreach (var key in _cacheKeys.Keys.ToList())
            {
                _cache.Remove(key);
                _cacheKeys.TryRemove(key, out _);
            }
        }

        public void ClearByPattern(string pattern)
        {
            var keysToRemove = _cacheKeys.Keys
                .Where(key => key.StartsWith(pattern, StringComparison.OrdinalIgnoreCase))
                .ToList();

            foreach (var key in keysToRemove)
            {
                _cache.Remove(key);
                _cacheKeys.TryRemove(key, out _);
            }
        }
    }
}