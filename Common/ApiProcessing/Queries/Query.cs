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
        private ICollection<QueryParameter> parameters;
        private string host;

        public Query(ObjectType objectType, string host)
        {
            this.ObjectType = objectType;
            this.parameters = new List<QueryParameter>();
            this.Host = host;
        }

        public Query(ObjectType objectType, string host, ICollection<QueryParameter> parameters) 
            : this(objectType, host)
        {
            this.Parameters = parameters;
        }

        public Query(ObjectType objectType, string host, params QueryParameter[] parameters) 
            : this(objectType, host)
        {
            this.Parameters = parameters;
        }

        public ObjectType ObjectType { get; private set; }

        internal ICollection<QueryParameter> Parameters
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
                else if (value.Count <= 0)
                {
                    throw new ArgumentException("Parameters collection must have at least one element.");
                }

                this.parameters = value;
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
            var queryString = new StringBuilder();
            var queryType = this.ObjectType.ToString().ToLower();
            queryString.AppendFormat("{0}/{1}?", this.Host, queryType);

            var parameterFormat = "{0}={1}";

            // ReSharper disable once InvertIf
            if (this.Parameters.Any())
            {
                foreach (var parameter in this.Parameters)
                {
                    queryString.AppendFormat(parameterFormat, parameter.Name, parameter.Value);
                    queryString.Append('&');
                }

                queryString.Remove(queryString.Length - 1, 1);
            }

            return queryString.ToString();
        }
    }
}
