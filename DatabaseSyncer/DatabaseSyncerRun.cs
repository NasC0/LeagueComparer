using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Configuration;
using Models;
using MongoDB.Driver;
using MongoDB.Bson;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using Data;

namespace DatabaseSyncer
{
    public class DatabaseSyncerRun
    {
        private const string ApiKey = "ee57feb3-1da3-441d-807a-7486e0559e72";
        private const string ConfigFileLocation = "../../config/config.json";

        static void Main()
        {
            var config = ConfigurationManager.GetCurrentConfiguration(ConfigFileLocation);

            IMongoClient mongoClient = new MongoClient("mongodb://localhost:1801/LeagueComparer");
            var database = mongoClient.GetDatabase("LeagueComparer");

            var repoFactory = new RepositoryFactory(database);

            var itemsCollection = repoFactory.GetRepository<Item>();

            string items = Task<string>.Run(() => GetItems()).Result;

            var jsonObject = JObject.Parse(items);
            var jsonChampionString = jsonObject["data"].ToString();

            var resultItem = JsonConvert.DeserializeObject<IDictionary<string, Item>>(jsonChampionString)
                                        .Select(x => x.Value)
                                        .OrderBy(x => x.Name)
                                        .ToList();

            itemsCollection.Add(resultItem).Wait();
            var allItemsFilter = new BsonDocument();
            var mongoItems = itemsCollection.All().Result;
            foreach (var item in mongoItems)
            {
                Console.WriteLine(item.Id);
            }
        }

        static async Task<string> GetItems()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("https://global.api.pvp.net/api/lol/static-data/euw/v1.2/item?itemListData=all&api_key=ee57feb3-1da3-441d-807a-7486e0559e72");
            return await response.Content.ReadAsStringAsync();
        }
    }
}
