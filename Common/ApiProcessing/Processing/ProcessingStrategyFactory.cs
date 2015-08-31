using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private ILog logger = SysLogger.GetLogger(typeof(ProcessingStrategyFactory));
        private IRepositoryFactory repoFactory;

        public ProcessingStrategyFactory(IRepositoryFactory repoFactory)
        {
            this.repoFactory = repoFactory;
        }

        public IGameObjectProcessingStrategy GetProcessingStrategy(IQueryResponse queryResponse)
        {
            IGameObjectProcessingStrategy currentStrategy = null;

            switch (queryResponse.QueryObjectType)
            {
                case ObjectType.Champion:
                    break;
                case ObjectType.Item:
                    currentStrategy = this.GetItemStrategy(queryResponse);
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

        private IGameObjectProcessingStrategy GetChampionStrategy(IQueryResponse queryResponse)
        {
            throw new NotImplementedException();
        }

        private IGameObjectProcessingStrategy GetItemStrategy(IQueryResponse queryResponse)
        {
            var itemsRepo = this.repoFactory.GetRepository<Item>();
            var itemsProcessingStrategy = new ItemProcessingStrategy(itemsRepo);
            return itemsProcessingStrategy;
        }
    }
}
