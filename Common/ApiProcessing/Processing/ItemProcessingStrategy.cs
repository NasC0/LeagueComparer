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
    public class ItemProcessingStrategy : BaseProcessingStrategy<Item>, IGameObjectProcessingStrategy
    {
        public ItemProcessingStrategy(IRepository<Item> repository)
            : base(repository, typeof(ItemProcessingStrategy))
        {
        }

        public async Task ProcessQueryResponse(IQueryResponse response)
        {
            try
            {
                var itemsFromResponse = await this.ConvertResponseContent(response.Content);
                var itemsFromCollection = await this.Repository.Find(i => i.Available == true);

                bool areCollectionsEqual = CollectionEquality.CheckForEquality<Item, int>(itemsFromResponse, itemsFromCollection, i => i.ItemId);

                if (!areCollectionsEqual)
                {
                    await this.ProcessDifferences(itemsFromResponse, itemsFromCollection);
                }

                this.logger.Info("Finished processing Items response");
            }
            catch (Exception ex)
            {
                this.logger.FatalFormat("Failed to process Item response: {0}", ex);
            }
        }

        protected override IEnumerable<Item> GetViableItemsMissingFromDb(IEnumerable<Item> itemsMissingFromApi, IEnumerable<Item> itemsMissingFromDb)
        {
            var itemsDifferentThanApi = itemsMissingFromApi.Where(c => itemsMissingFromDb.All(i => i.ItemId == c.ItemId));
            var viableItems = itemsMissingFromApi.Except(itemsDifferentThanApi);
            return viableItems;
        }

        protected override async Task ProcessItemsMissingFromDb(IEnumerable<Item> itemsMissingFromDb)
        {
            this.logger.Info("Processing items missing from database");
            foreach (var item in itemsMissingFromDb)
            {
                await this.Repository.Replace(item, i => i.ItemId == item.ItemId);
            }
        }

        protected override async Task ProcessItemsMissingFromApi(IEnumerable<Item> itemsMissingFromApi)
        {
            this.logger.Info("Processing items missing from API");

            var updateDefinitionBuilder = new UpdateDefinitionBuilder<Item>();
            var updateDefinition = updateDefinitionBuilder.Set<bool>(x => x.Available, false);

            foreach (var item in itemsMissingFromApi)
            {
                await this.Repository.Update(item, i => i.ItemId == item.ItemId, updateDefinition);
            }
        }
    }
}
