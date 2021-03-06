using System.Web.Hosting;
using DataAccess;
using PersonSearchServices;
using PersonSearchServices.Interfaces;
using PersonSearchServices.NinjectModule;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(PersonSearch.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(PersonSearch.App_Start.NinjectWebCommon), "Stop")]

namespace PersonSearch.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            var rootDirectory = HostingEnvironment.ApplicationPhysicalPath;

            // I would to have liked not having to have a reference to the data project in the web project,
            // but there is no way to specify InRequestScope in a NinjectModule that is not in the web project
            kernel.Bind<IPersonSearchService>().To<PersonSearchService>().WithConstructorArgument("rootDirectory", rootDirectory);
            kernel.Bind<PersonContext>().ToSelf().InRequestScope();

            kernel.Load(new PersonSearchNinjectModule());
        }        
    }
}
