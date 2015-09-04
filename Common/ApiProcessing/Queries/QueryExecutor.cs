using System;
using System.Net.Http;
using System.Threading.Tasks;
using ApiProcessing.Contracts;
using Helpers.Exceptions;
using log4net;
using Logging;

namespace ApiProcessing.Queries
{
    public class QueryExecutor : IQueryExecutor
    {
        private ILog logger = SysLogger.GetLogger(typeof(QueryExecutor));
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
                var failedOperation = new FailedOperationException(string.Format("Request to server failed with status code {0}", serverResponse.StatusCode));
                this.logger.FatalFormat("Failed query execution for {0} with status code {1}: {2}", query.ObjectType, serverResponse.StatusCode, failedOperation);
                throw failedOperation;
            }

            var contentString = await serverResponse.Content.ReadAsStringAsync();
            var queryResponse = new QueryResponse(contentString, query.ObjectType);

            this.logger.InfoFormat("Successful query execution for {0}", query.ObjectType);
            return queryResponse;
        }
    }
}
