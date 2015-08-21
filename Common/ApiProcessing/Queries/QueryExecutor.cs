using System;
using System.Net.Http;
using System.Threading.Tasks;
using ApiProcessing.Contracts;
using Helpers.Exceptions;

namespace ApiProcessing.Queries
{
    public class QueryExecutor : IQueryExecutor
    {
        private HttpClient apiClient;

        public QueryExecutor()
            : this(new HttpClient())
        {
        }

        public QueryExecutor(HttpClient apiClient)
        {
            this.ApiClient = apiClient;
        }

        private HttpClient ApiClient
        { 
            get
            {
                return this.apiClient;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Api client must be initialized.");
                }

                this.apiClient = value;
            }
        }

        public async Task<IQueryResponse> ExecuteQuery(IQuery query)
        {
            var serverResponse = await this.ApiClient.GetAsync(query.GetQueryString());
            if (!serverResponse.IsSuccessStatusCode)
            {
                throw new FailedOperationException(string.Format("Request to server failed with status code {0}", serverResponse.StatusCode));
            }

            var contentString = await serverResponse.Content.ReadAsStringAsync();
            var queryResponse = new QueryResponse(contentString, query.ObjectType);
            return queryResponse;
        }
    }
}
