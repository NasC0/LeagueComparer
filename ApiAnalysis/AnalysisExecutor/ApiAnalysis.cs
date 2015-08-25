using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AnalysisCore;

namespace AnalysisExecutor
{
    public class ApiAnalysis
    {
        private const string ConfigFileLocation = "../../config/config.json";

        static void Main()
        {
            var config = Configuration.ConfigurationManager.GetCurrentConfiguration(ConfigFileLocation);
            var currentHttpClient = new HttpClient();
            var spellCostAnalyser = new GetSpellCostTypes(currentHttpClient, config);
            var championResourcesAnalyser = new ChampionResourcesAnalyser(currentHttpClient, config);
            var runeTypesAnalyser = new RuneTypes(currentHttpClient, config);

            spellCostAnalyser.ExecuteAnalysis().Wait();
            championResourcesAnalyser.ExecuteAnalysis().Wait();
            runeTypesAnalyser.ExecuteAnalysis().Wait();
        }
    }
}
