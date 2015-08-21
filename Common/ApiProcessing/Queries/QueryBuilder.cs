using System;
using System.Collections.Generic;
using ApiProcessing.Contracts;
using ApiProcessing.Enumerations;
using Helpers;

namespace ApiProcessing.Queries
{
    public class QueryBuilder : IQueryBuilder
    {
        private const string ApiKeyParameterName = "api_key";

        private readonly QueryParameter RequiredItemParameter = new QueryParameter("itemListData", "all");
        private readonly QueryParameter RequiredChampionParameter = new QueryParameter("champData", "all");
        private readonly QueryParameter RequiredRuneParameter = new QueryParameter("runeListData", "all");
        private readonly QueryParameter RequiredMasteryParameter = new QueryParameter("masteryListData", "all");
        private readonly QueryParameter RequiredApiKeyParameter;

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
            List<QueryParameter> parameters = new List<QueryParameter>();
            return this.BuildQuery(objectType, parameters);
        }

        public IQuery BuildQuery(ObjectType objectType, ICollection<QueryParameter> parameters)
        {
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
            parameters.Add(RequiredItemParameter);

            return parameters;
        }

        private ICollection<QueryParameter> GetChampionParameters(ICollection<QueryParameter> parameters)
        {
            parameters.Add(RequiredChampionParameter);

            return parameters;
        }

        private ICollection<QueryParameter> GetRuneParameters(ICollection<QueryParameter> parameters)
        {
            parameters.Add(RequiredRuneParameter);

            return parameters;
        }

        private ICollection<QueryParameter> GetMasteryParameters(ICollection<QueryParameter> parameters)
        {
            parameters.Add(RequiredMasteryParameter);

            return parameters;
        }
    }
}
