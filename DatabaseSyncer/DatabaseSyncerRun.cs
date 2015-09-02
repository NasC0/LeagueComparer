using System.Net.Http;
using System.Threading.Tasks;
using ApiProcessing.Enumerations;
using ApiProcessing.Processing;
using ApiProcessing.Queries;
using Configuration;
using Data;
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
            var newId = ObjectId.GenerateNewId().ToString();

            IMongoClient mongoClient = new MongoClient("mongodb://kickass:314159aass@spirital.tk:1801");
            var database = mongoClient.GetDatabase("LeagueComparer");
            var repoFactory = new RepositoryFactory(database);
            var itemsRepo = repoFactory.GetRepository<Item>();
            var allItems = itemsRepo.All().Result;

            var strategyFactory = new ProcessingStrategyFactory(repoFactory);

            string items = Task<string>.Run(() => GetItems()).Result;

            var itemsStrategy = strategyFactory.GetProcessingStrategy(ObjectType.Item);
            itemsStrategy.ProcessQueryResponse(new QueryResponse(items, ObjectType.Item)).Wait();
        }
         
        static async Task<string> GetItems()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("https://global.api.pvp.net/api/lol/static-data/euw/v1.2/item?itemListData=all&api_key=ee57feb3-1da3-441d-807a-7486e0559e72");
            return await response.Content.ReadAsStringAsync();
        }
    }
}
