using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Contracts;
using log4net;
using Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
            this.repository = repository;
        }

        protected virtual async Task ProcessDifferences(IEnumerable<T> itemsFromApi, IEnumerable<T> itemsFromCollection)
        {
            var itemsMissingFromDb = itemsFromApi.Except(itemsFromCollection);
            var itemsMissingFromApi = itemsFromCollection.Except(itemsFromApi);

            if (itemsMissingFromDb.Count() > 0)
            {
                await this.ProcessItemsMissingFromDb(itemsMissingFromDb);
            }

            if (itemsMissingFromApi.Count() > 0)
            {
                await this.ProcessItemsMissingFromApi(itemsMissingFromApi);
            }
        }

        protected abstract Task ProcessItemsMissingFromDb(IEnumerable<T> itemsMissingFromDb);

        protected abstract Task ProcessItemsMissingFromApi(IEnumerable<T> itemsMissingFromApi);

        protected async virtual Task<IEnumerable<T>> ConvertResponseContent(string responseContent)
        {
            try
            {
                var parseableContent = JObject.Parse(responseContent).SelectToken(JsonDataEntryPoint).ToString();
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
