using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.Autofac.Caching
{
    public class CacheAspect : MethodInterception
    {
        private int _duration;
        private ICacheManager _cacheManager;

        public CacheAspect(int duration = 60) // default değer 60 dakika. süre vermezsek kendiliğinden 60 dakika bellekte duracak.
        {
            _duration = duration;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        public override void Intercept(IInvocation invocation)
        {
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}"); //invocation.Method.ReflectedType.FullName >> namespace + classın ismini verir. Method.Name>< metodun ismini verir. key oluşturmaya çalışıyoruz. örn:(Northwind.Business.ICarService.GetAll)
            var arguments = invocation.Arguments.ToList(); //metodun parametresi varsa listeye çevir
            var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})"; //parametre değeri varsa GetAll() içine ekle >> Northwind.Business.ICarService.GetAll(x) .. ?? >> yoksa "<Null>" ekle demek.
            if (_cacheManager.IsAdd(key)) //böyle bir cache anahtarı bellekte varmı 
            {
                invocation.ReturnValue = _cacheManager.Get(key);  //eğer varsa cachemanagerden key'i getir
                return;
            }
            invocation.Proceed(); // ama yoksa metodu devam ettir. metot çalışınca veritabanına gider. veri tabanından datayı getirir
            _cacheManager.Add(key, invocation.ReturnValue, _duration); // demekki önceden cache eklenmemiş şuan eklenmesi gerekiyor ve ekler.
        }
    }
}
