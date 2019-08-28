[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(HeroStore.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(HeroStore.App_Start.NinjectWebCommon), "Stop")]

namespace HeroStore.App_Start
{
    using System;
    using System.Web;
    using System.Web.Http;
    using BLL.Abstract;
    using BLL.Concrete;
    using Domain.Abstract;
    using Domain.Concrete;
    using Domain.EXContexts;
    using HeroStore.Areas.Shop.Abstract;
    using HeroStore.Areas.Shop.Models;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(
                    ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                RegisterServices(kernel);
                GlobalConfiguration.Configuration.DependencyResolver =
                    new Ninject.Web.WebApi.NinjectDependencyResolver(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }
        
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IUnitOfWork>()
                .To<UnitOfWork>();
            kernel.Bind<ShopContext>()
                .ToSelf();
            kernel.Bind<IOrderService>()
                .To<OrderService>();
            kernel.Bind<IProductService>()
                .To<ProductService>();
            kernel.Bind<ICustomerService>()
                .To<CustomerService>();
        }        
    }
}