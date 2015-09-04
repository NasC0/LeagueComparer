using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ApiProcessing.Enumerations;
using ApiProcessing.Processing;
using ApiProcessing.Queries;
using Configuration;
using Data;
using Helpers;
using Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DatabaseSyncer
{
    public class DatabaseSyncerRun
    {
        private const string ApiKey = "ee57feb3-1da3-441d-807a-7486e0559e72";
        private const string ConfigFileLocation = "../../config/config.json";

        static void Main()
        {
            var config = ConfigurationManager.GetCurrentConfiguration(ConfigFileLocation);
            IMongoClient mongoClient = new MongoClient("mongodb://kickass:314159aass@spirital.tk:1801");
            var database = mongoClient.GetDatabase("LeagueComparer");
            var repoFactory = new RepositoryFactory(database);
            var strategyProcessingFactory = new ProcessingStrategyFactory(repoFactory);

            var queryBuilder = new QueryBuilder(config.ApiKey, ApiUrlBuilder.BuildApiStaticDataUrl(Regions.euw, config));
            var championQuery = queryBuilder.BuildQuery(ObjectType.Champion, new QueryParameter("champData", "all"));

            var queryExecutor = new QueryExecutor();
            var championQueryResponse = queryExecutor.ExecuteQuery(championQuery).Result;
            var championProcessingStrategy = strategyProcessingFactory.GetProcessingStrategy(championQueryResponse.QueryObjectType);
            championProcessingStrategy.ProcessQueryResponse(championQueryResponse).Wait();
        }
    }
}