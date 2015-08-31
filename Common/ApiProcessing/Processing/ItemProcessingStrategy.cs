using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiProcessing.Contracts;
using Data.Contracts;
using log4net;
using Logging;
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
            var itemsFromResponseOrdered = this.ConvertResponseContent(response.Content)
                                               .OrderBy(i => i.ItemId);
            var itemsFromCollection = await this.repository.All();
            var orderedItemsFromCollection = itemsFromCollection.OrderBy(i => i.ItemId);

            bool areCollectionsEqual = orderedItemsFromCollection.SequenceEqual(itemsFromResponseOrdered);

            if (!areCollectionsEqual)
            {

            }
        }

        private async Task ProcessDifferences(IEnumerable<Item> itemsFromApi, IEnumerable<Item> itemsFromCollection)
        {
            var itemsMissingFromDb = itemsFromApi.Except(itemsFromCollection);
            var itemsMissingFromApi = itemsFromCollection.Except(itemsFromApi);

            if (itemsMissingFromDb.Count() > 0)
            {

            }

            if (itemsMissingFromApi.Count() > 0)
            {
                this.ProcessItemsMissingFromApi(itemsMissingFromApi);
            }
        }

        private async Task ProcessItemsMissingFromDb(IEnumerable<Item> itemsMissingFromDb)
        {

        }

        private async Task ProcessItemsMissingFromApi(IEnumerable<Item> itemsMissingFromApi)
        {
            var updateDefinitionBuilder = new UpdateDefinitionBuilder<Item>();
            var updateDefinition = updateDefinitionBuilder.Set<bool>(x => x.Available, false);

            foreach (var item in itemsMissingFromApi)
            {
                await this.repository.Update(item, updateDefinition);
            }
        }
    }
}
