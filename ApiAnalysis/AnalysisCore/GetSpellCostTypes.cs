using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Configuration;
using Helpers;
using Newtonsoft.Json.Linq;

namespace AnalysisCore
{
    public class GetSpellCostTypes : AnalysisBase, IAnalysisBase
    {
        private const string OutputFileName = "ChampionSpellCost.txt";
        private const string ApiMethod = "champion?champData=spells&api_key=";

        public GetSpellCostTypes(HttpClient apiQueryExecutor, Config currentConfiguration)
            : base(apiQueryExecutor, currentConfiguration)
        {
            this.apiEndpoint = ApiUrlBuilder.BuildApiStaticDataUrl(Regions.euw, this.currentConfiguration);
            this.apiMethod = ApiMethod;
        }

        public async override Task ExecuteAnalysis()
        {
            var jsonQueryResponse = await this.PrepareAnalysisJson();
            var spellsList = new HashSet<string>();

            foreach (var champion in jsonQueryResponse["data"])
            {
                foreach (var spell in champion.First["spells"])
                {
                    var spellValues = spell["costType"].Value<string>();
                    spellsList.Add(spellValues);
                }
            }

            using (StreamWriter sw = new StreamWriter(AnalysisOutputLocation + OutputFileName))
            {
                foreach (var spell in spellsList)
                {
                    sw.WriteLine(spell);
                }
            }
        }
    }
}
