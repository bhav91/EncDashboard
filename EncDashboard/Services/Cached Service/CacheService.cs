using Microsoft.Extensions.Caching.Memory;

namespace EncDashboard.Services.Cached_Service
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _cache;
        private static readonly object _locker = new object();
        private readonly IConfiguration _apiConfig;
        public CacheService(IMemoryCache cache, IConfiguration configuration)
        {
            _cache = cache;
            _apiConfig = configuration;
        }

        public void SetKey(string cacheKey, object value)
        {
            lock (_locker)
            {
                double expiryTime;
                double.TryParse(_apiConfig["APIConfig:CacheExpiryMin"], out expiryTime);
                _cache.Set(cacheKey, value, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(expiryTime)));
            }
        }

        public TItem Get<TItem>(string cacheKey)
        {
            lock (_locker)
            {
                return _cache.Get<TItem>(cacheKey);
            }
        }

        public void Remove(string key)
        {
            lock (_locker)
            {
                if (true)
                {
                    if (_cache.TryGetValue(key, out var item))
                    {
                        if (item != null)
                        {
                            _cache.Remove(key);
                        }
                    }
                }
            }
        }

        public void BulkRemove()
        {
            lock (_locker)
            {
                List<string> cacheKeys = _cache.GetKeys<string>().ToList();

                foreach (string item in cacheKeys)
                {
                    _cache.Remove(item);
                }
            }
        }
    }
}

