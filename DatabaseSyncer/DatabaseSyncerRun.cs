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
using MongoDB.Bson.Serialization;

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

            var itemsCollection = repoFactory.GetRepository<Item>();

            string items = Task<string>.Run(() => GetItems()).Result;

            var jsonObject = JObject.Parse(items);
            var jsonChampionString = jsonObject["data"].ToString();

            var resultItem = JsonConvert.DeserializeObject<IDictionary<string, Item>>(jsonChampionString)
                                        .Select(x => x.Value)
                                        .OrderBy(x => x.Name)
                                        .ToList();

            var newItemToAdd = new Item()
            {
                Depth = 1,
                Description = "Tehehehe",
                From = resultItem[0].From,
                Gold = resultItem[0].Gold,
                Group = resultItem[1].Group,
                Image = resultItem[0].Image,
                Into = resultItem[1].Into,
                ItemId = 9901,
                Name = "Tinkywinky",
                SanitizedDescription = resultItem[0].SanitizedDescription,
                Stats = resultItem[1].Stats,
                Tags = resultItem[2].Tags
            };

            itemsCollection.Add(resultItem).Wait();

            var allItems = itemsCollection.All().Result.ToList();
            resultItem.Add(newItemToAdd);

            //var deprecatedItemToAdd = new Item()
            //{
            //    Depth = 2,
            //    Description = "Mana regen",
            //    From = resultItem[0].From,
            //    Gold = resultItem[0].Gold,
            //    Group = resultItem[1].Group,
            //    Image = resultItem[0].Image,
            //    Into = resultItem[1].Into,
            //    ItemId = 9901,
            //    Name = "Meki Pendant",
            //    SanitizedDescription = resultItem[0].SanitizedDescription,
            //    Stats = resultItem[1].Stats,
            //    Tags = resultItem[2].Tags
            //};

            //allItems.Add(deprecatedItemToAdd);

            allItems.RemoveRange(0, 10);

            var difference = resultItem.Except(allItems);
        }

        static async Task<string> GetItems()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("https://global.api.pvp.net/api/lol/static-data/euw/v1.2/item?itemListData=all&api_key=ee57feb3-1da3-441d-807a-7486e0559e72");
            return await response.Content.ReadAsStringAsync();
        }
    }
}
