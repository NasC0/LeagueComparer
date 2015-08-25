using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Configuration;
using Models;
using Models.Common.Champion;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
            var itemsCollection = database.GetCollection<Item>("Items");

            string items = Task<string>.Run(() => GetItems()).Result;

            var jsonObject = JObject.Parse(items);
            var jsonChampionString = jsonObject["data"].ToString();

            var resultItem = JsonConvert.DeserializeObject<IDictionary<string, Champion>>(jsonChampionString)
                                        .Select(x => x.Value)
                                        .OrderBy(x => x.Name)
                                        .ToList();
        }

        static async Task<string> GetItems()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("https://global.api.pvp.net/api/lol/static-data/euw/v1.2/champion?champData=altimages,image,info,lore,partype,passive,spells,stats,tags&api_key=ee57feb3-1da3-441d-807a-7486e0559e72");
            return await response.Content.ReadAsStringAsync();
        }
    }
}
