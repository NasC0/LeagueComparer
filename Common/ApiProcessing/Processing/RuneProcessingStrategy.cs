using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiProcessing.Contracts;
using Data.Contracts;
using Helpers;
using Models;
using MongoDB.Driver;

namespace ApiProcessing.Processing
{
    public class RuneProcessingStrategy : BaseProcessingStrategy<Rune>, IGameObjectProcessingStrategy
    {
        public RuneProcessingStrategy(IRepository<Rune> runesRepo)
            : base(runesRepo, typeof(RuneProcessingStrategy))
        {
        }

        public async Task ProcessQueryResponse(IQueryResponse response)
        {
            try
            {
                var itemsFromResponse = await this.ConvertResponseContent(response.Content);
                var itemsFromCollection = await this.Repository.Find(i => i.Available == true);

                bool areCollectionsEqual = CollectionEquality.CheckForEquality<Rune, int>(itemsFromResponse, itemsFromCollection, r => r.RuneId);

                if (!areCollectionsEqual)
                {
                    await this.ProcessDifferences(itemsFromResponse, itemsFromCollection);
                }

                this.logger.Info("Finished processing Runes response");
            }
            catch (Exception ex)
            {
                this.logger.FatalFormat("Failed to process Runes response: {0}", ex);
            }
        }

        protected override IEnumerable<Rune> GetViableItemsMissingFromDb(IEnumerable<Rune> itemsMissingFromApi, IEnumerable<Rune> itemsMissingFromDb)
        {
            var itemsDifferentThanApi = itemsMissingFromApi.Where(rune => itemsMissingFromDb.All(dbRune => rune.RuneId == dbRune.RuneId));
            var viableItems = itemsMissingFromApi.Except(itemsDifferentThanApi);
            return viableItems;
        }

        protected override async Task ProcessItemsMissingFromDb(IEnumerable<Rune> itemsMissingFromDb)
        {
            this.logger.Info("Processing runes missing from database");
            foreach (var rune in itemsMissingFromDb)
            {
                await this.Repository.Replace(rune, r => r.RuneId == rune.RuneId);
            }
        }

        protected override async Task ProcessItemsMissingFromApi(IEnumerable<Rune> itemsMissingFromApi)
        {
            this.logger.Info("Processing runes missing from API");
            var updateDefinitionBuilder = new UpdateDefinitionBuilder<Rune>();
            var updateDefinition = updateDefinitionBuilder.Set<bool>(r => r.Available, true);

            foreach (var rune in itemsMissingFromApi)
            {
                await this.Repository.Update(rune, r => r.RuneId == rune.RuneId, updateDefinition);
            }
        }
    }
}
