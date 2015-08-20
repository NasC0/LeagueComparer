using System;
using System.Collections.Generic;
using ApiProcessing.Contracts;
using ApiProcessing.Enumerations;
using Helpers;

namespace ApiProcessing.Queries
{
    public class QueryBuilder : IQueryBuilder
    {
        private readonly QueryParameter RequiredItemParameter = new QueryParameter("itemListData", "all");
        private readonly QueryParameter RequiredChampionParameter = new QueryParameter("champData", "all");
        private readonly QueryParameter RequiredRuneParameter = new QueryParameter("runeListData", "all");
        private readonly QueryParameter RequiredMasteryParameter = new QueryParameter("masteryListData", "all");

        private string apiKey;
        private string host;

        public QueryBuilder(string apiKey, string host)
        {
            this.ApiKey = apiKey;
            this.Host = host;
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

        public IQuery BuildQuery(ObjectType objectType, IEnumerable<QueryParameter> parameters)
        {
            IEnumerable<QueryParameter> processedParameters = null;

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

            return new Query(objectType, this.ApiKey, this.Host, processedParameters);
        }

        private IEnumerable<QueryParameter> GetItemParameters(IEnumerable<QueryParameter> parameters)
        {
            var queryParameters = new List<QueryParameter>(parameters);
            queryParameters.Add(RequiredItemParameter);

            return queryParameters;
        }

        private IEnumerable<QueryParameter> GetChampionParameters(IEnumerable<QueryParameter> parameters)
        {
            var queryParameters = new List<QueryParameter>(parameters);
            queryParameters.Add(RequiredChampionParameter);

            return queryParameters;
        }

        private IEnumerable<QueryParameter> GetRuneParameters(IEnumerable<QueryParameter> parameters)
        {
            var queryParameters = new List<QueryParameter>(parameters);
            queryParameters.Add(RequiredRuneParameter);

            return queryParameters;
        }

        private IEnumerable<QueryParameter> GetMasteryParameters(IEnumerable<QueryParameter> parameters)
        {
            var queryParameters = new List<QueryParameter>(parameters);
            queryParameters.Add(RequiredMasteryParameter);

            return queryParameters;
        }
    }
}
