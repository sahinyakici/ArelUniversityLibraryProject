using Core.Utilities.Results.Abstract;

namespace Core.CrossCuttingConcerns.Caching;

public interface ICacheManager
{
    IDataResult<T> Get<T>(string key);
    IDataResult<object> Get(string key);
    IResult Add(string key, object value, int duration);
    IResult IsAdd(string key);
    IResult Remove(string key);
    IResult RemoveByPattern(string pattern);
}