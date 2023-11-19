using System.Reflection;
using System.Text.RegularExpressions;
using Core.Utilities.IoC;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace Core.CrossCuttingConcerns.Caching.Microsoft;

public class MemoryCacheManager : ICacheManager
{
    private IMemoryCache _memoryCache;

    public MemoryCacheManager()
    {
        _memoryCache = ServiceTool.ServiceProvider.GetService<IMemoryCache>();
    }

    public IDataResult<T> Get<T>(string key)
    {
        var result = _memoryCache.Get<T>(key);
        return new SuccessDataResult<T>(result);
    }

    public IDataResult<object> Get(string key)
    {
        var result = _memoryCache.Get(key);
        return new SuccessDataResult<object>(result);
    }

    public IResult Add(string key, object value, int duration)
    {
        _memoryCache.Set(key, value, TimeSpan.FromMinutes(duration));
        return new SuccessResult();
    }

    public IResult IsAdd(string key)
    {
        bool result = _memoryCache.TryGetValue(key, out _);
        if (result)
        {
            return new SuccessResult();
        }

        return new ErrorResult();
    }

    public IResult Remove(string key)
    {
        _memoryCache.Remove(key);
        return new SuccessResult();
    }

    public IResult RemoveByPattern(string pattern)
    {
        dynamic cacheEntriesCollection = null;
        var cacheEntriesFieldCollectionDefinition = typeof(MemoryCache).GetField("_coherentState",
            BindingFlags.NonPublic | BindingFlags.Instance);

        if (cacheEntriesFieldCollectionDefinition != null)
        {
            var coherentStateValueCollection = cacheEntriesFieldCollectionDefinition.GetValue(_memoryCache);
            var entriesCollectionValueCollection = coherentStateValueCollection.GetType()
                .GetProperty("EntriesCollection",
                    BindingFlags.NonPublic | BindingFlags.Instance);
            cacheEntriesCollection = entriesCollectionValueCollection.GetValue(coherentStateValueCollection);
        }

        List<ICacheEntry> cacheCollectionValues = new List<ICacheEntry>();

        foreach (var cacheItem in cacheEntriesCollection)
        {
            ICacheEntry cacheItemValue = cacheItem.GetType().GetProperty("Value").GetValue(cacheItem, null);
            cacheCollectionValues.Add(cacheItemValue);
        }

        var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
        var keysToRemove = cacheCollectionValues.Where(d => regex.IsMatch(d.Key.ToString())).Select(d => d.Key)
            .ToList();

        foreach (var key in keysToRemove)
        {
            _memoryCache.Remove(key);
        }

        return new SuccessResult();
    }
}