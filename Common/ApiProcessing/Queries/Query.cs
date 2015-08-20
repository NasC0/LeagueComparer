using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApiProcessing.Contracts;
using ApiProcessing.Enumerations;
using ApiProcessing.Properties;

namespace ApiProcessing.Queries
{
    internal class Query : IQuery
    {
        private IEnumerable<QueryParameter> parameters;
        private string apiKey;
        private string host;

        public Query(ObjectType objectType, string apiKey, string host)
        {
            this.ObjectType = objectType;
            this.parameters = new List<QueryParameter>();
            this.ApiKey = apiKey;
            this.Host = host;
        }

        public Query(ObjectType objectType, string apiKey, string host, IEnumerable<QueryParameter> parameters)
            : this(objectType, apiKey, host)
        {
            this.Parameters = parameters;
        }

        public Query(ObjectType objectType, string apiKey, string host, params QueryParameter[] parameters)
            : this(objectType, apiKey, host)
        {
            this.Parameters = parameters;
        }

        public ObjectType ObjectType { get; private set; }

        internal IEnumerable<QueryParameter> Parameters
        {
            get
            {
                return this.parameters;
            }

            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Parameters must be initialized.");
                }
                else if (value.Count() <= 0)
                {
                    throw new ArgumentException("Parameters collection must have at least one element.");
                }

                this.parameters = value;
            }
        }

        internal string ApiKey
        {
            get
            {
                return this.apiKey;
            }

            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("ApiKey cannot be null, empty or plain whitespace.");
                }

                this.apiKey = value;
            }
        }

        internal string Host
        {
            get
            {
                return this.host;
            }

            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Host cannot be null, empty or plain whitespace.");
                }

                this.host = value;
            }
        }

        public string GetQueryString()
        {
            StringBuilder queryString = new StringBuilder();
            string queryType = this.ObjectType.ToString().ToLower();
            queryString.AppendFormat("{0}/{1}?", this.Host, queryType);

            string parameterFormat = "{0}={1}";
            if (this.Parameters.Count() > 0)
            {
                foreach (var parameter in this.Parameters)
                {
                    queryString.AppendFormat(parameterFormat, parameter.Name, parameter.Value);
                    queryString.Append('&');
                }
            }

            queryString.AppendFormat(parameterFormat, Resources.ApiKeyParameterName, this.ApiKey);

            return queryString.ToString();
        }
    }
}
