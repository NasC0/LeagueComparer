using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Configuration;
using Newtonsoft.Json.Linq;

namespace AnalysisCore
{
    public abstract class AnalysisBase : IAnalysisBase
    {
        protected const string AnalysisOutputLocation = "../../analysed/";

        protected string apiMethod;
        protected HttpClient apiQueryExecutor;
        protected Config currentConfiguration;
        protected string apiEndpoint;

        public AnalysisBase(HttpClient apiQueryExecutor, Config currentConfiguration)
        {
            this.apiQueryExecutor = apiQueryExecutor;
            this.currentConfiguration = currentConfiguration;
        }

        public abstract Task ExecuteAnalysis();

        protected async Task<JObject> PrepareAnalysisJson()
        {
            string fullApiMethod = string.Format("{0}{1}{2}", this.apiEndpoint, this.apiMethod, this.currentConfiguration.ApiKey);
            var queryResponse = await this.apiQueryExecutor.GetAsync(fullApiMethod);
            string queryString = await queryResponse.Content.ReadAsStringAsync();

            JObject jsonQueryResponse = JObject.Parse(queryString);

            return jsonQueryResponse;
        }
    }
}
