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
            try
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
                        currentStrategy = this.GetRuneStrategy();
                        break;
                    case ObjectType.Mastery:
                        currentStrategy = this.GetMasteryStrategy();
                        break;
                    default:
                        var exception = new ArgumentException("Invalid argument supplied for query object type.");
                        this.logger.Fatal("Cannot get strategy for query object type: {0}", exception);
                        throw exception;
                }

                return currentStrategy;
            }
            catch (Exception ex)
            {
                this.logger.FatalFormat("Exception raised while getting processing strategy for {0}: {1}", queryResponseType.ToString(), ex);
                throw ex;
            }
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

        private IGameObjectProcessingStrategy GetRuneStrategy()
        {
            var runesRepo = this.repoFactory.GetRepository<Rune>();
            var runeProcessingStrategy = new RuneProcessingStrategy(runesRepo);
            return runeProcessingStrategy;
        }

        private IGameObjectProcessingStrategy GetMasteryStrategy()
        {
            var masteriesRepo = this.repoFactory.GetRepository<Mastery>();
            var masteriesProcessingStrategy = new MasteryProcessingStrategy(masteriesRepo);
            return masteriesProcessingStrategy;
        }
    }
}
