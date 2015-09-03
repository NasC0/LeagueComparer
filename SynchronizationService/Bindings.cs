using System.Net.Http;
using ApiProcessing.Contracts;
using ApiProcessing.Processing;
using ApiProcessing.Queries;
using Configuration;
using Data;
using Data.Contracts;
using Helpers;
using MongoDB.Driver;
using Ninject.Modules;

namespace SynchronizationService
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            var mongoDb = new MongoClient(Properties.Settings.Default.MongoConnection);
            var mongoConnection = mongoDb.GetDatabase(Properties.Settings.Default.DatabaseName);
            var masterConfiguration = ConfigurationManager.GetCurrentConfiguration(Properties.Settings.Default.MasterConfigLocation);
            Bind<IRepositoryFactory>().To<RepositoryFactory>().WithConstructorArgument(mongoConnection);
            Bind<IProcessingStrategyFactory>().To<ProcessingStrategyFactory>();
            Bind<IQueryExecutor>().To<QueryExecutor>().WithConstructorArgument(new HttpClient());
            Bind<IQueryBuilder>().To<QueryBuilder>()
                .WithConstructorArgument(Properties.Settings.Default.ApiKey)
                .WithConstructorArgument(ApiUrlBuilder.BuildApiStaticDataUrl(Regions.euw, masterConfiguration));
        }
    }
}
