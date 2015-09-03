using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiProcessing.Contracts;
using Data.Contracts;
using Models;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using MongoDB.Bson.Serialization;
using Newtonsoft.Json;
using log4net;
using Logging;
using Helpers;

namespace ApiProcessing.Processing
{
    public class ChampionProcessingStrategy : IGameObjectProcessingStrategy
    {
        private const string JsonDataEntryPoint = "data";
        private const string ChampionIdParameterName = "id";
        private const string ChampionAvailableParameterName = "Available";

        private IRepository<BsonDocument> championSavingRepository;
        private IRepository<Champion> championQueryingRepository;
        private ILog logger;

        public ChampionProcessingStrategy(IRepository<BsonDocument> championSavingRepository, IRepository<Champion> championQueryingRepository)
        {
            this.championSavingRepository = championSavingRepository;
            this.championQueryingRepository = championQueryingRepository;
            this.logger = SysLogger.GetLogger(typeof(ChampionProcessingStrategy));
        }

        public async Task ProcessQueryResponse(IQueryResponse response)
        {
            try
            {
                var championsFromResponse = await this.ConvertResponseContent(response.Content);
                var championsFromCollection = await this.championQueryingRepository.Find(c => c.Available == true);

                bool areCollectionsEqual = CollectionEquality.CheckForEquality<Champion, int>(championsFromResponse, championsFromCollection, c => c.ChampionId);

                if (!areCollectionsEqual)
                {
                    await this.ProcessDifferences(championsFromResponse, championsFromCollection);
                }

                this.logger.Info("Finished processing Champions response");
            }
            catch (Exception ex)
            {
                this.logger.FatalFormat("Failed to process Champions response: {0}", ex);
            }
        }

        protected async Task ProcessDifferences(IEnumerable<Champion> itemsFromApi, IEnumerable<Champion> itemsFromCollection)
        {
            var itemsDifferentFromDb = itemsFromApi.Except(itemsFromCollection);
            int itemsDifferentFromDbCount = itemsDifferentFromDb.Count();

            var itemsMissingFromApi = itemsFromCollection.Except(itemsFromApi);

            if (itemsDifferentFromDbCount > 0)
            {
                var itemsDifferentThanApi = itemsMissingFromApi.Where(c => itemsDifferentFromDb.All(i => i.ChampionId == c.ChampionId));
                itemsMissingFromApi = itemsMissingFromApi.Except(itemsDifferentThanApi);
            }

            var itemsMissingFromApiCount = itemsMissingFromApi.Count();

            if (itemsDifferentFromDbCount > 0)
            {
                this.logger.InfoFormat("Champions different in database count: {1}", itemsDifferentFromDbCount);
                await this.ProcessItemsMissingFromDb(itemsDifferentFromDb);
            }

            if (itemsMissingFromApiCount > 0)
            {
                this.logger.InfoFormat("Champions missing from API count: {1}", itemsMissingFromApiCount);
                await this.ProcessItemsMissingFromApi(itemsMissingFromApi);
            }
        }

        protected async Task<IEnumerable<Champion>> ConvertResponseContent(string responseContent)
        {
            try
            {
                var rootObject = JObject.Parse(responseContent);
                var parseableContent = rootObject[JsonDataEntryPoint];
                var convertedResponse = JsonConvert.DeserializeObject<IDictionary<string, Champion>>(parseableContent.ToString())
                                                   .Select(c => c.Value);

                // This is a dirty, dirty, DIRTY hack. Slow and underperforming.
                // TODO: Try to refactor this after a new version of the MongoDB driver
                var currentDbChampions = await this.championQueryingRepository.All();

                foreach (var champion in convertedResponse)
                {
                    var correspondingDbChampion = currentDbChampions.Where(c => c.ChampionId == champion.ChampionId).SingleOrDefault();

                    if (correspondingDbChampion != null)
                    {
                        champion._id = new ObjectId(correspondingDbChampion._id.ToString());
                    }
                    else
                    {
                        champion._id = ObjectId.GenerateNewId();
                    }
                }
                // End of DIRTY hack

                logger.InfoFormat("Successfully converted {0} response json to {1} objects from {0}", typeof(Champion).Name, "BSON documents");
                return convertedResponse;
            }
            catch (Exception ex)
            {
                logger.FatalFormat("Fatal error raised while trying to convert {0} response json to {1} objects from {0}", typeof(Champion).Name, "BSON documents");
                throw ex;
            }
        }

        protected async Task ProcessItemsMissingFromDb(IEnumerable<Champion> itemsMissingFromDb)
        {
            var jsonStringChampions = JsonConvert.SerializeObject(itemsMissingFromDb, Formatting.Indented);
            var bsonChampions = BsonSerializer.Deserialize<IEnumerable<BsonDocument>>(jsonStringChampions);

            foreach (var champion in bsonChampions)
            {
                await this.championSavingRepository.Replace(champion, c => c[ChampionIdParameterName] == champion[ChampionIdParameterName]);
            }
        }

        protected async Task ProcessItemsMissingFromApi(IEnumerable<Champion> itemsMissingFromApi)
        {
            var updateDefinitionBuilder = new UpdateDefinitionBuilder<BsonDocument>();
            var updateDefinition = updateDefinitionBuilder.Set(c => c[ChampionAvailableParameterName], false);

            var jsonStringChampions = JsonConvert.SerializeObject(itemsMissingFromApi, Formatting.Indented);
            var bsonChampions = BsonSerializer.Deserialize<IEnumerable<BsonDocument>>(jsonStringChampions);

            foreach (var champion in bsonChampions)
            {
                await this.championSavingRepository.Update(champion, c => c[ChampionIdParameterName] == champion[ChampionIdParameterName], updateDefinition);
            }
        }
    }
}
