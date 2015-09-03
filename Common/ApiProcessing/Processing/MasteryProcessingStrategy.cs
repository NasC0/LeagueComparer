using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiProcessing.Contracts;
using Data.Contracts;
using Helpers;
using Models;
using MongoDB.Driver;

namespace ApiProcessing.Processing
{
    public class MasteryProcessingStrategy : BaseProcessingStrategy<Mastery>, IGameObjectProcessingStrategy
    {
        public MasteryProcessingStrategy(IRepository<Mastery> masteryRepo)
            : base(masteryRepo, typeof(MasteryProcessingStrategy))
        {
        }

        public async Task ProcessQueryResponse(IQueryResponse response)
        {
            try
            {
                var itemsFromResponse = await this.ConvertResponseContent(response.Content);
                var itemsFromCollection = await this.Repository.Find(i => i.Available == true);

                bool areCollectionsEqual = CollectionEquality.CheckForEquality<Mastery, int>(itemsFromResponse, itemsFromCollection, i => i.MasteryId);

                if (!areCollectionsEqual)
                {
                    await this.ProcessDifferences(itemsFromResponse, itemsFromCollection);
                }

                this.logger.Info("Finished processing Masteries response");
            }
            catch (Exception ex)
            {
                this.logger.FatalFormat("Failed to process Masteries response: {0}", ex);
            }
        }

        protected override IEnumerable<Mastery> GetViableItemsMissingFromDb(IEnumerable<Mastery> itemsMissingFromApi, IEnumerable<Mastery> itemsMissingFromDb)
        {
            var itemsDifferentThanApi = itemsMissingFromApi.Where(m => itemsMissingFromDb.All(i => i.MasteryId == m.MasteryId));
            var viableItems = itemsMissingFromApi.Except(itemsDifferentThanApi);
            return viableItems;
        }

        protected override async Task ProcessItemsMissingFromDb(IEnumerable<Mastery> itemsMissingFromDb)
        {
            this.logger.Info("Processing masteries missing from DB");
            foreach (var item in itemsMissingFromDb)
            {
                await this.Repository.Replace(item, m => m.MasteryId == item.MasteryId);
            }
        }

        protected override async Task ProcessItemsMissingFromApi(IEnumerable<Mastery> itemsMissingFromApi)
        {
            this.logger.Info("Processing masteries missing from API");

            var updateDefinitionBuilder = new UpdateDefinitionBuilder<Mastery>();
            var updateDefinition = updateDefinitionBuilder.Set<bool>(m => m.Available, false);

            foreach (var item in itemsMissingFromApi)
            {
                await this.Repository.Update(item, m => m.MasteryId == item.MasteryId, updateDefinition);
            }
        }
    }
}
