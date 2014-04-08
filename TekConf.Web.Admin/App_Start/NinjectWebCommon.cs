using Common.Logging;
using TekConf.Data;
using TekConf.Data.Models;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(TekConf.Web.Admin.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(TekConf.Web.Admin.App_Start.NinjectWebCommon), "Stop")]

namespace TekConf.Web.Admin.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

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
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

            kernel.Bind<ILog>().ToMethod(context => LogManager.GetLogger(context.Request.Target.Member.DeclaringType));
            kernel.Bind<ITekConfContext>().To<TekConfContext>().InRequestScope();

            kernel.Bind<UserManager<User>>().ToMethod(ctx =>
            {
                var manager = new UserManager<User>(new UserStore<User>((TekConfContext)kernel.TryGet<ITekConfContext>()));
                manager.UserValidator = new UserValidator<User>(manager) { AllowOnlyAlphanumericUserNames = false };
                return manager;
            }).InRequestScope();

            kernel.Bind<RoleManager<Role>>().ToMethod(ctx => new RoleManager<Role>(new RoleStore<Role>((TekConfContext)kernel.TryGet<ITekConfContext>()))).InRequestScope();


            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
        }        
    }
}
