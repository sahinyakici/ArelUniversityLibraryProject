using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.Caching;

public class CacheAspect : MethodInterception
{
    private ICacheManager _cacheManager;
    private int _duration;

    public CacheAspect(int duration = 144000)
    {
        _duration = duration;
        _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
    }

    public override void Intercept(IInvocation invocation)
    {
        var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}");
        var arguments = invocation.Arguments.ToList();
        var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})";
        if (_cacheManager.IsAdd(key).Success)
        {
            var result = _cacheManager.Get(key);
            if (result.Success)
            {
                invocation.ReturnValue = result.Data;
                return;
            }

            invocation.ReturnValue = null;
            return;
        }

        invocation.Proceed();
        _cacheManager.Add(key, invocation.ReturnValue, _duration);
    }
}