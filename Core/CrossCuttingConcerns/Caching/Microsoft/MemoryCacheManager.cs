using Core.Utilities.IoC;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Caching.Microsoft
{
    public class MemoryCacheManager : ICacheManager
    {
        //Adapter Pattern
        IMemoryCache _memoryCache;

        public MemoryCacheManager()
        {
            _memoryCache = ServiceTool.ServiceProvider.GetService<IMemoryCache>();
        }

        public void Add(string key, object value, int duration)
        {
            _memoryCache.Set(key, value, TimeSpan.FromMinutes(duration)); //ne kadar süre verirsek(duration) o kadar süre cache'de kalacak
        }

        public T Get<T>(string key) //cache'den belli bir türdeki veriyi getirir
        {
            return _memoryCache.Get<T>(key);
        }

        public object Get(string key) //cache'den veriyi getirir ama dönecek tür object olduğu için tür dönüşümü yapılması gerekir
        {
            return _memoryCache.Get(key);
        }

        public bool IsAdd(string key) //belirli bir anahtar değeri cache'de var mı diye kontrol eder. varsa true yoksa false döner. (out _) ifadesi, değerin ne olduğunu döndürme sadece varlığını kontrol et demek.
        {
            return _memoryCache.TryGetValue(key, out _);
        }

        public void Remove(string key) //cache'deki veriyi siler
        {
            _memoryCache.Remove(key);
        }

        public void RemoveByPattern(string pattern) //verdiğimiz bir patterne göre çalışma anında cacheden silme işlemini yapar. örnek>> [CacheRemoveAspect("ICarService.Get)")]
        {
            
            dynamic cacheEntriesCollection = null;
            var cacheEntriesFieldCollectionDefinition = typeof(MemoryCache).GetField("_coherentState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            if (cacheEntriesFieldCollectionDefinition != null)
            {
                var coherentStateValueCollection = cacheEntriesFieldCollectionDefinition.GetValue(_memoryCache);
                var entriesCollectionValueCollection = coherentStateValueCollection.GetType().GetProperty("EntriesCollection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                cacheEntriesCollection = entriesCollectionValueCollection.GetValue(coherentStateValueCollection);
            }

            List<ICacheEntry> cacheCollectionValues = new List<ICacheEntry>();


            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = cacheCollectionValues.Where(d => regex.IsMatch(d.Key.ToString())).Select(d => d.Key).ToList();

            foreach (var key in keysToRemove)
            {
                _memoryCache.Remove(key);
            }

            
        }
    }
}
