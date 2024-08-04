using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LibraryManager.Data.Helper
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
            var objectJson = JsonSerializer.Serialize(value);

            await _cache.SetStringAsync(key, objectJson);
        }

        public async Task<T?> GetCacheObject<T>(string key)
        {
            var fetchedJson = await _cache.GetAsync(key);

            if (fetchedJson is null)
                return default;

            var objectDeserialized = JsonSerializer.Deserialize<T>(fetchedJson);

            return objectDeserialized;
        }

        public async Task RefreshCache(string keyValue)
            => await _cache.RefreshAsync(keyValue);

        public async Task RemoveCache(string keyValue)
            => await _cache.RemoveAsync(keyValue);
    }
}
