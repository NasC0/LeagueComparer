using Configuration;
namespace Helpers
{
    public static class ApiUrlBuilder
    {
        private const string ImgBaseUrlFormat = "http://ddragon.leagueoflegends.com/cdn/";
        private const string ImgWithVersionUrlFormat = "http://ddragon.leagueoflegends.com/cdn/{0}/img/{1}/{2}";
        private const string ItemImageType = "item";
        private const string ChampionImageType = "champion";
        private const string MasteryImageType = "mastery";
        private const string RuneImageType = "rune";
        private const string ProfileImageType = "profileicon";
        private const string PassiveSpellImageType = "passive";
        private const string ChampionSpellImageType = "spell";
        private const string SummonnerSpellImageType = "spell";

        public static string BuildApiStaticDataUrl(Regions region, Config currentConfiguration)
        {
            string staticDataFormat = "{0}{1}/{2}/{3}";
            string baseUrl = Config.GetBaseUrl(Regions.global);
            string staticDataUrl = string.Format(staticDataFormat, baseUrl, currentConfiguration.StaticData, region, currentConfiguration.ApiLatestStaticDataVersion);

            return staticDataUrl;
        }

        public static string GetItemImageUrl(string imageName)
        {
            return BuildApiImageUrlWithVersion(ItemImageType, imageName);
        }

        public static string GetChampionImageUrl(string imageName)
        {
            return BuildApiImageUrlWithVersion(ChampionImageType, imageName);
        }

        public static string GetMasteryImageUrl(string imageName)
        {
            return BuildApiImageUrlWithVersion(MasteryImageType, imageName);
        }

        public static string GetRuneImageUrl(string imageName)
        {
            return BuildApiImageUrlWithVersion(RuneImageType, imageName);
        }

        public static string GetProfileIconImageUrl(string imageName)
        {
            return BuildApiImageUrlWithVersion(ProfileImageType, imageName);
        }

        public static string GetChampionSplashImageUrl(string imageName)
        {
            string splashImageUrl = string.Format(ImgBaseUrlFormat + "img/champion/splash/{0}", imageName);
            return splashImageUrl;
        }

        public static string GetChampionLoadingScreenImageUrl(string imageName)
        {
            string loadingScreenImageUrl = string.Format(ImgBaseUrlFormat + "img/champion/loading/{0}", imageName);
            return loadingScreenImageUrl;
        }

        public static string GetPassiveSpellImageUrl(string imageName)
        {
            return BuildApiImageUrlWithVersion(PassiveSpellImageType, imageName);
        }

        public static string GetChampionSpellImageUrl(string imageName)
        {
            return BuildApiImageUrlWithVersion(ChampionSpellImageType, imageName);
        }

        public static string GetSummonnerSpellImageUrl(string imageName)
        {
            return BuildApiImageUrlWithVersion(SummonnerSpellImageType, imageName);
        }

        private static string BuildApiImageUrlWithVersion(string objectType, string imageName)
        {
            string gameVersion = GameVersionChecker.GetGameVersion();
            string imageUrl = string.Format(ImgWithVersionUrlFormat, gameVersion, objectType, imageName);
            return imageUrl;
        }
    }
}
