﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LegacyMongoDriverSync
{
    class LegacyMongoDriverSync
    {
        static void Main()
        {
            var dbClient = new MongoClient("mongodb://kickass:314159aass@spirital.tk:1801");
            var server = dbClient.GetServer();
            var database = server.GetDatabase("LeagueComparer");
            var collection = database.GetCollection<Champion>("champions");

            string items = Task<string>.Run(() => GetItems()).Result;

            var jsonObject = JObject.Parse(items);
            var jsonChampionString = jsonObject["data"].ToString();

            var resultItem = JsonConvert.DeserializeObject<IDictionary<string, Champion>>(jsonChampionString)
                                        .Select(x => x.Value)
                                        .OrderBy(x => x.ChampionId)
                                        .ToList();

            foreach (var champ in resultItem)
            {
                champ._id = ObjectId.GenerateNewId();
            }

            var resultString = JsonConvert.SerializeObject(resultItem, Formatting.Indented);
            var bsonDocument = BsonSerializer.Deserialize<IEnumerable<BsonDocument>>(resultString).ToList();

            collection.InsertBatch<BsonDocument>(bsonDocument);

            var champions = collection.AsQueryable<Champion>();
            int champCount = 0;
            foreach (var cham in champions)
            {
                Console.WriteLine(cham.Title);
                champCount++;
            }

            Console.WriteLine(champCount);
        }

        static async Task<string> GetItems()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("https://global.api.pvp.net/api/lol/static-data/euw/v1.2/champion?champData=all&api_key=ee57feb3-1da3-441d-807a-7486e0559e72");
            return await response.Content.ReadAsStringAsync();
        }
    }
}
