using Autofac;
using NLayer.Caching;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Repository;
using NLayer.Repository.Repositories;
using NLayer.Repository.UnitOfWork;
using NLayer.Service.Mapping;
using NLayer.Service.Services;
using System.Reflection;
using Module = Autofac.Module;  //Aşagıdaki Modul Autosafc'deki Module karşılık gelsin 

namespace NLayer.API.Modules
{
    public class RepoServiceModul:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(Service<>)).As(typeof(IService<>)).InstancePerLifetimeScope();

            builder.RegisterType<UnitOFWork>().As<IUnitOfWork>();

            var apiAssembly = Assembly.GetExecutingAssembly();

            var repoAssembly = Assembly.GetAssembly(typeof(AppDbContext));

            var serviceAssembly = Assembly.GetAssembly(typeof(MapProfile));

            //class larda sonu Repository olarak bitenleri al o class lara karşı yine sonu Repository olarak biten Interface leri  al dedik bu kodu yazarak.
            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x=>x.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerLifetimeScope();

            //class larda sonu Service olarak bitenleri al o class lara karşı yine sonu Service olarak biten Interface leri  al dedik bu kodu yazarak.
            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerLifetimeScope();


            //InstancePerLifetimeScope()  => Scope (Asp.Net Core daki Scope karşı geliyor)
            //InstancePerDependency()     => transient  (aynı işi yapıyorlar ama sadece methodları farklı , transiemte karşı geliyor)

            builder.RegisterType<ProductServiceWithCaching>().As<IProductService>();

            base.Load(builder);
        }
    }
}
