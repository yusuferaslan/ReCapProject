using Core.DependencyResolvers;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    //Extension(genişletme) metodu yazabilmek için classın static olması gerekir.
    //IServiceCollection Asp.net(WebApi) uygulamamızın servis bağımlılıklarını ya da araya girmesini istediğimiz servisleri eklediğimiz koleksiyondur.
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencyResolvers(this IServiceCollection serviceCollection, ICoreModule[] modules)
        {
            foreach (var module in modules)
            {
                module.Load(serviceCollection); //(ICoreModule[] modules)'deki eklenen her bir modül için (module) yükle.
            }

            return ServiceTool.Create(serviceCollection);
        }
        // bu yaptıklarımız core katmanı dahil olmak üzere ekleyeceğimiz bütün injektion'lari bir arada toplayabileceğimiz bir yapı
    }
}
