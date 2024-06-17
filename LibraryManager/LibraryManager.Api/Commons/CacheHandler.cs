using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace LibraryManager.Api.Commons
{
    public class CacheHandler
    {
        private readonly IDistributedCache _cache;

        public CacheHandler(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task SetCacheObject<T>(string key, T value)
        {
            var objectJson =  JsonSerializer.Serialize(value);

            await _cache.SetStringAsync(key, objectJson);
        }

        public async Task<T> GetCacheObject<T>(string key)
        {
            var fetchedJson = await _cache.GetAsync(key);

            var objectDeserialized = JsonSerializer.Deserialize<T>(fetchedJson);

            return objectDeserialized;
        }
    }
}
