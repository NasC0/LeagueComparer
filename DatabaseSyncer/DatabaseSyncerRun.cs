using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Models;
using MongoDB.Driver;

namespace DatabaseSyncer
{
    public class DatabaseSyncerRun
    {
        private const string ApiKey = "ee57feb3-1da3-441d-807a-7486e0559e72";

        static void Main()
        {
            IMongoClient mongoClient = new MongoClient("mongodb://kickass:314159aass@spirital.tk:1801");
            var database = mongoClient.GetDatabase("LeagueComparer");
            var itemsCollection = database.GetCollection<Item>("Items");

            string items = Task<string>.Run(() => GetItems()).Result;
        }

        static async Task<string> GetItems()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("https://global.api.pvp.net/api/lol/static-data/euw/v1.2/item?itemListData=all&api_key=ee57feb3-1da3-441d-807a-7486e0559e72");
            return await response.Content.ReadAsStringAsync();
        }
    }
}
