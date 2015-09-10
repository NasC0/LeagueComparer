using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ComparerAPI.Infrastructure;
using Data;
using Data.Contracts;
using MongoDB.Driver;
using Ninject;

namespace ComparerAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var kernel = CreateKernel();

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configuration.DependencyResolver = new NinjectResolver(kernel);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();

            var mongoDb = new MongoClient(Properties.Settings.Default.MongoConnection);
            var mongoConnection = mongoDb.GetDatabase(Properties.Settings.Default.DatabaseName);

            kernel.Bind<IRepositoryFactory>().To<RepositoryFactory>()
                .WithConstructorArgument(mongoConnection);

            return kernel;
        }
    }
}
