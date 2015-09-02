using System;
using ApiProcessing.Contracts;
using ApiProcessing.Enumerations;
using Data.Contracts;
using log4net;
using Logging;
using Models;

namespace ApiProcessing.Processing
{
    public class ProcessingStrategyFactory : IProcessingStrategyFactory
    {
        private const string ChampionsCollectionName = "champions";

        private ILog logger = SysLogger.GetLogger(typeof(ProcessingStrategyFactory));
        private IRepositoryFactory repoFactory;

        public ProcessingStrategyFactory(IRepositoryFactory repoFactory)
        {
            this.repoFactory = repoFactory;
        }

        public IGameObjectProcessingStrategy GetProcessingStrategy(ObjectType queryResponseType)
        {
            IGameObjectProcessingStrategy currentStrategy = null;

            switch (queryResponseType)
            {
                case ObjectType.Champion:
                    currentStrategy = this.GetChampionStrategy();
                    break;
                case ObjectType.Item:
                    currentStrategy = this.GetItemStrategy();
                    break;
                case ObjectType.Rune:
                    break;
                case ObjectType.Mastery:
                    break;
                default:
                    var exception = new ArgumentException("Invalid argument supplied for query object type.");
                    this.logger.Fatal("Cannot get strategy for query object type: {0}", exception);
                    throw exception;
            }

            return currentStrategy;
        }

        private IGameObjectProcessingStrategy GetChampionStrategy()
        {
            var championsBsonRepo = this.repoFactory.GetRepository(ChampionsCollectionName);
            var championsObjectsRepo = this.repoFactory.GetRepository<Champion>();
            var championsProcessingStrategy = new ChampionProcessingStrategy(championsBsonRepo, championsObjectsRepo);
            return championsProcessingStrategy;
        }

        private IGameObjectProcessingStrategy GetItemStrategy()
        {
            var itemsRepo = this.repoFactory.GetRepository<Item>();
            var itemsProcessingStrategy = new ItemProcessingStrategy(itemsRepo);
            return itemsProcessingStrategy;
        }
    }
}
