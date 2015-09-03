using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Contracts;
using Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using log4net;

namespace ApiProcessing.Processing
{
    public abstract class BaseProcessingStrategy<T>
    {
        private const string JsonDataEntryPoint = "data";

        protected ILog logger;
        protected IRepository<T> repository;

        public BaseProcessingStrategy(IRepository<T> repository, Type loggerType)
        {
            this.logger = SysLogger.GetLogger(loggerType);
            this.Repository = repository;
        }

        protected IRepository<T> Repository
        {
            get
            {
                return this.repository;
            }

            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Repository value cannot be null");
                }

                this.repository = value;
            }
        }

        protected virtual async Task ProcessDifferences(IEnumerable<T> itemsFromApi, IEnumerable<T> itemsFromCollection)
        {
            var itemsDifferentFromDb = itemsFromApi.Except(itemsFromCollection);
            int itemsDifferentFromDbCount = itemsDifferentFromDb.Count();
            var itemsMissingFromApi = itemsFromCollection.Except(itemsFromApi);

            if (itemsDifferentFromDbCount > 0)
            {
                itemsMissingFromApi = this.GetViableItemsMissingFromDb(itemsMissingFromApi, itemsDifferentFromDb);
            }

            var itemsMissingFromApiCount = itemsMissingFromApi.Count();

            if (itemsDifferentFromDbCount > 0)
            {
                this.logger.InfoFormat("{0} different in database count: {1}", typeof(T).Name, itemsDifferentFromDbCount);
                await this.ProcessItemsMissingFromDb(itemsDifferentFromDb);
            }

            if (itemsMissingFromApiCount > 0)
            {
                this.logger.InfoFormat("{0} missing from API count: {1}", typeof(T).Name, itemsMissingFromApiCount);
                await this.ProcessItemsMissingFromApi(itemsMissingFromApi);
            }
        }

        protected abstract IEnumerable<T> GetViableItemsMissingFromDb(IEnumerable<T> itemsMissingFromApi, IEnumerable<T> itemsMissingFromDb);

        protected abstract Task ProcessItemsMissingFromDb(IEnumerable<T> itemsMissingFromDb);

        protected abstract Task ProcessItemsMissingFromApi(IEnumerable<T> itemsMissingFromApi);

        protected async virtual Task<IEnumerable<T>> ConvertResponseContent(string responseContent)
        {
            try
            {
                var parseableContent = JObject.Parse(responseContent)
                                              .SelectToken(JsonDataEntryPoint)
                                              .ToString();

                var convertedResponse = JsonConvert.DeserializeObject<IDictionary<string, T>>(parseableContent)
                                                   .Select(c => c.Value);

                logger.InfoFormat("Successfully converted {0} response json to {0} objects", typeof(T).Name);
                return convertedResponse;
            }
            catch (Exception ex)
            {
                logger.FatalFormat("Fatal error raised while trying to convert {0} response json to {0} objects", typeof(T).Name);
                throw ex;
            }
        }
    }
}
