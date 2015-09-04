using Configuration;
namespace Helpers
{
    public static class ApiUrlBuilder
    {
        public static string BuildApiStaticDataUrl(Regions region, Config currentConfiguration)
        {
            string staticDataFormat = "{0}{1}/{2}/{3}";
            string baseUrl = Config.GetBaseUrl(Regions.global);
            string staticDataUrl = string.Format(staticDataFormat, baseUrl, currentConfiguration.StaticData, region, currentConfiguration.ApiLatestStaticDataVersion);

            return staticDataUrl;
        }
    }
}
