using System.Collections.Generic;
using System.Linq;

namespace Configuration
{
    public class Config
    {
        private const string BaseUrl = "https://{0}.api.pvp.net/api/lol/";

        public Config(IDictionary<string, string> apiData)
        {
            var properties = this.GetType().GetProperties();
            foreach (var property in apiData)
            {
                var propertyAvailable = properties
                                        .Where(x => x.Name.ToLower() == property.Key.ToLower())
                                        .SingleOrDefault();

                if (propertyAvailable != null)
                {
                    propertyAvailable.SetValue(this, property.Value);
                }
            }
        }

        public string ApiKey { get; private set; }
        public string ApiLatestChampionVersion { get; private set; }
        public string ApiLatestCurrentGameVersion { get; private set; }
        public string ApiLatestFeaturedGamesVersion { get; private set; }
        public string ApiLatestGameVersion { get; private set; }
        public string ApiLatestLeagueVersion { get; private set; }
        public string ApiLatestStaticDataVersion { get; private set; }
        public string ApiLatestStatusVersion { get; private set; }
        public string ApiLatestMatchVersion { get; private set; }
        public string ApiLatestMatchHistoryVersion { get; private set; }
        public string ApiLatestMatchListVersion { get; private set; }
        public string ApiLatestStatsVersion { get; private set; }
        public string ApiLatestSummonerVersion { get; private set; }
        public string ApiLatestTeamVersion { get; private set; }
        public string StaticData { get; private set; }

        public static string GetBaseUrl(Regions region)
        {
            string baseUrl = string.Format(BaseUrl, region.ToString());
            return baseUrl;
        }
    }
}
