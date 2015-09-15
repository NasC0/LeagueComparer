using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Data.Contracts;
using Microsoft.Owin;
using Models;
using MongoDB.Driver;
using Ninject;
using Owin;

[assembly: OwinStartup(typeof(ComparerAPI.Startup))]

namespace ComparerAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var kernel = CreateKernel();
            ConfigureAuth(app, kernel);
        }

        public static IKernel CreateKernel()
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
