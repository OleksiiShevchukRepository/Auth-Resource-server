using System;
using System.Web;
using Core.Interfaces;
using Ninject;
using Ninject.Web.Common;
using ResourceServer;
using Services;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(NinjectWebCommon), "Stop")]
namespace ResourceServer
{
    public class NinjectWebCommon 
    {
        public static Bootstrapper Bootstrapper { get; } = new Bootstrapper();

        public static void Start()
        {
            
        }

        public static void Stop()
        {

        }

        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                return kernel;
            }
            catch (Exception)
            {
                kernel.Dispose();
                throw;
            }
        }

        public static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IWebApplicationConfig>().To<WebApplicationConfig>();
            ServiceInitializer.RegisterMongoServices(kernel);
        }
    }
}