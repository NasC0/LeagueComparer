using System;
using System.Collections.Generic;
using System.Linq;
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

        protected virtual IEnumerable<T> ConvertResponseContent(string responseContent)
        {
            try
            {
                var rootObject = JObject.Parse(responseContent);
                var parseableContent = rootObject[JsonDataEntryPoint];
                var convertedResponse = JsonConvert.DeserializeObject<IDictionary<string, T>>(parseableContent.ToString())
                                                   .Select(c => c.Value)
                                                   .ToList();

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
