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
            return this.BuildQuery(objectType, parameters);
        }

        public IQuery BuildQuery(ObjectType objectType, ICollection<QueryParameter> parameters)
        {
            this.LogQueryBuilding(objectType);
            ICollection<QueryParameter> processedParameters = null;

            if (objectType == ObjectType.Item)
            {
                processedParameters = GetItemParameters(parameters);
            }
            else if(objectType == ObjectType.Champion)
            {
                processedParameters = GetChampionParameters(parameters);
            }
            else if(objectType == ObjectType.Rune)
            {
                processedParameters = GetRuneParameters(parameters);
            }
            else if(objectType == ObjectType.Mastery)
            {
                processedParameters = GetMasteryParameters(parameters);
            }

            processedParameters.Add(this.RequiredApiKeyParameter);
            return new Query(objectType, this.Host, processedParameters);
        }

        private ICollection<QueryParameter> GetItemParameters(ICollection<QueryParameter> parameters)
        {
            return parameters;
        }

        private ICollection<QueryParameter> GetChampionParameters(ICollection<QueryParameter> parameters)
        {
            return parameters;
        }

        private ICollection<QueryParameter> GetRuneParameters(ICollection<QueryParameter> parameters)
        {
            return parameters;
        }

        private ICollection<QueryParameter> GetMasteryParameters(ICollection<QueryParameter> parameters)
        {
            return parameters;
        }

        private void LogQueryBuilding(ObjectType queryType)
        {
            this.logger.InfoFormat("Building {0} query", queryType.ToString());
        }
    }
}
