using System;
using ApiProcessing.Contracts;
using ApiProcessing.Enumerations;
using Helpers;

namespace ApiProcessing.Queries
{
    public class QueryResponse : IQueryResponse
    {
        private string content;

        public QueryResponse(string content, ObjectType queryObjectType)
        {
            this.Content = content;
            this.QueryObjectType = queryObjectType;
        }

        public string Content
        {
            get
            {
                return this.content;
            }

            private set
            {
                if (value.IsInvalid())
                {
                    throw new ArgumentException("Response content must not be null, empty or plain whitespace.");
                }

                this.content = value;
            }
        }

        public ObjectType QueryObjectType { get; private set; }
    }
}
