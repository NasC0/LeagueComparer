using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Configuration;
using Helpers;
using Newtonsoft.Json.Linq;

namespace AnalysisCore
{
    public class ChampionResourcesAnalyser : AnalysisBase, IAnalysisBase
    {
        private const string OutputFileName = "ChampionResources.txt";
        private const string ApiMethod = "champion?champData=partype&api_key=";

        public ChampionResourcesAnalyser(HttpClient apiQueryExecutor, Config currentConfiguration)
            : base(apiQueryExecutor, currentConfiguration)
        {
            this.apiEndpoint = ApiUrlBuilder.BuildApiStaticDataUrl(Regions.euw, currentConfiguration);
            this.apiMethod = ApiMethod;
        }

        public async override Task ExecuteAnalysis()
        {
            var jsonQueryResponse = await this.PrepareAnalysisJson();
            var resourcesList = new HashSet<string>();

            foreach (var champion in jsonQueryResponse["data"])
            {
                var championObject = champion.First;
                var championResource = championObject["partype"].Value<string>();
                resourcesList.Add(championResource);
            }

            using (StreamWriter sw = new StreamWriter(AnalysisOutputLocation + OutputFileName))
            {
                foreach (var resource in resourcesList)
                {
                    sw.WriteLine(resource);
                }
            }
        }
    }
}
