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
                var itemsFromCollection = await this.repository.Find(i => i.Available == true);

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

        protected override async Task ProcessDifferences(IEnumerable<Item> itemsFromApi, IEnumerable<Item> itemsFromCollection)
        {
            var itemsDifferentFromDb = itemsFromApi.Except(itemsFromCollection);
            int itemsDifferentFromDbCount = itemsDifferentFromDb.Count();

            var itemsMissingFromApi = itemsFromCollection.Except(itemsFromApi);

            if (itemsDifferentFromDbCount > 0)
            {
                var itemsDifferentThanApi = itemsMissingFromApi.Where(c => itemsDifferentFromDb.All(i => i.ItemId == c.ItemId));
                itemsMissingFromApi = itemsMissingFromApi.Except(itemsDifferentThanApi);
            }

            if (itemsDifferentFromDbCount > 0)
            {
                await this.ProcessItemsMissingFromDb(itemsDifferentFromDb);
            }

            if (itemsMissingFromApi.Count() > 0)
            {
                await this.ProcessItemsMissingFromApi(itemsMissingFromApi);
            }
        }

        protected override async Task ProcessItemsMissingFromDb(IEnumerable<Item> itemsMissingFromDb)
        {
            this.logger.Info("Processing items missing from database");
            foreach (var item in itemsMissingFromDb)
            {
                await this.repository.Replace(item, i => i.ItemId == item.ItemId);
            }
        }

        protected override async Task ProcessItemsMissingFromApi(IEnumerable<Item> itemsMissingFromApi)
        {
            this.logger.Info("Processing items missing from API");

            var updateDefinitionBuilder = new UpdateDefinitionBuilder<Item>();
            var updateDefinition = updateDefinitionBuilder.Set<bool>(x => x.Available, false);

            foreach (var item in itemsMissingFromApi)
            {
                await this.repository.Update(item, i => i.ItemId == item.ItemId, updateDefinition);
            }
        }
    }
}
