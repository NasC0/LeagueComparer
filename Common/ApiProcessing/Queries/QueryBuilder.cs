using System;
using System.Collections.Generic;
using ApiProcessing.Contracts;
using ApiProcessing.Enumerations;
using Helpers;
using log4net;
using Logging;

namespace ApiProcessing.Queries
{
    public class QueryBuilder : IQueryBuilder
    {
        private const string ApiKeyParameterName = "api_key";

        private readonly QueryParameter RequiredApiKeyParameter;

        private ILog logger = SysLogger.GetLogger(typeof(QueryBuilder));
        private string apiKey;
        private string host;

        public QueryBuilder(string apiKey, string host)
        {
            this.ApiKey = apiKey;
            this.Host = host;
            this.RequiredApiKeyParameter = new QueryParameter(ApiKeyParameterName, this.ApiKey);
        }

        public string ApiKey
        {
            get
            {
                return this.apiKey;
            }

            private set
            {
                if (value.IsInvalid())
                {
                    throw new ArgumentException("Api Key must not be null, empty or plain whitespace.");
                }

                this.apiKey = value;
            }
        }

        public string Host
        {
            get
            {
                return this.host;
            }

            private set
            {
                if (value.IsInvalid())
                {
                    throw new ArgumentException("Host must not be null, empty or plain whitespace.");
                }

                this.host = value;
            }
        }

        public IQuery BuildQuery(ObjectType objectType)
        {
            this.LogQueryBuilding(objectType);
            List<QueryParameter> parameters = new List<QueryParameter>();
            return this.BuildQuery(objectType, parameters.ToArray());
        }

        public IQuery BuildQuery(ObjectType objectType, params QueryParameter[] queryParameters)
        {
            this.LogQueryBuilding(objectType);
            ICollection<QueryParameter> processedParameters = new List<QueryParameter>(queryParameters);

            processedParameters.Add(this.RequiredApiKeyParameter);
            return new Query(objectType, this.Host, processedParameters);
        }

        public IQuery BuildQuery(ObjectType objectType, ICollection<QueryParameter> queryParameters)
        {
            this.LogQueryBuilding(objectType);
            queryParameters.Add(this.RequiredApiKeyParameter);
            return new Query(objectType, this.Host, queryParameters);
        }

        private void LogQueryBuilding(ObjectType queryType)
        {
            this.logger.InfoFormat("Building {0} query", queryType.ToString());
        }
    }
}
