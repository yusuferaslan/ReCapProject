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
    public class CacheRemoveAspect : MethodInterception
    {
        private string _pattern;
        private ICacheManager _cacheManager;

        public CacheRemoveAspect(string pattern) //Data eklenirse güncellenirse veya silinirse bellekteki data değiştiği için metotlara CacheRemoveAspect uygulanır.
        {
            _pattern = pattern;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        protected override void OnSuccess(IInvocation invocation) //Onsuccess metot başarılı olursa git cacheden sil demek. yani ürün eklenmezse cacheden veriyi silmez.
        {
            _cacheManager.RemoveByPattern(_pattern);
        }
    }
}
