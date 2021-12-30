using HelenSposa.Core.Utilities.IoC;
using HelenSposa.Core.Utilities.Result;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelenSposa.Core.CrossCuttingConcerns.Caching.Redis
{
    public class RedisCacheManager : ICacheManager
    {
        private static JsonSerializerSettings _settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
        IDistributedCache _cache;

        public RedisCacheManager()
        {
            _cache = ServiceTool.ServiceProvider.GetService<IDistributedCache>(); ;
        }

        public void Add(string key, object data, int duration)
        {
            var json = JsonConvert.SerializeObject(data,_settings);

            var dataForCache = Encoding.UTF8.GetBytes(json);

            var options = new DistributedCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(duration));

            _cache.Set(key, dataForCache, options);
        }

        public T Get<T>(string key)
        {
            var dataFromCache = _cache.Get(key);
            var json = Encoding.UTF8.GetString(dataFromCache);
            var data = JsonConvert.DeserializeObject<T>(json);

            return data;
        }

        public object Get(string key)
        {
            var dataFromCache=_cache.Get(key);
            var json = Encoding.UTF8.GetString(dataFromCache);
            var data = JsonConvert.DeserializeObject(json,_settings);

            return data;
        }

        public bool isExist(string key)
        {
            return (_cache.Get(key) != null ? true : false);
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        public void RemoveByPattern(string pattern)
        {
            throw new NotImplementedException();
        }
    }
}
