
using Autofac;
using ECommerceApp.ApplicationLayer.Services.Concrete;
using ECommerceApp.ApplicationLayer.Services.Interface;
using ECommerceApp.DomainLayer.UnitOfWork;
using ECommerceApp.InfrastructureLayer.UnitOfWork;

namespace ECommerceApp.ApplicationLayer.IoC
{
    public class AutoFactContainer:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AppUserService>().As<IAppUserService>().InstancePerLifetimeScope();
            builder.RegisterType<CategoryService>().As<ICategoryService>().InstancePerLifetimeScope();
            builder.RegisterType<ProductService>().As<IProductService>().InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();


        }
    }
}
