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
    public class RuneTypes : AnalysisBase, IAnalysisBase
    {
        private const string OutputFileName = "RuneTypes.txt";
        private const string ApiMethod = "rune?runeListData=all&api_key=";

        public RuneTypes(HttpClient httpClient, Config currentConfiguration)
            : base(httpClient, currentConfiguration)
        {
            this.apiEndpoint = ApiUrlBuilder.BuildApiStaticDataUrl(Regions.euw, currentConfiguration);
            this.apiMethod = ApiMethod;
        }

        public async override Task ExecuteAnalysis()
        {
            var jsonQueryResponse = await this.PrepareAnalysisJson();
            var runeTypesList = new HashSet<string>();

            foreach (var rune in jsonQueryResponse["data"])
            {
                var runeObject = rune.First;
                var runeType = runeObject["rune"]["type"].Value<string>();
                runeTypesList.Add(runeType);
            }

            using (StreamWriter sw = new StreamWriter(AnalysisOutputLocation + OutputFileName))
            {
                foreach (var rune in runeTypesList)
                {
                    sw.WriteLine(rune);
                }
            }
        }
    }
}
