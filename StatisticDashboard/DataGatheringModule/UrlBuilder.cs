namespace DataGatheringModule
{
    public sealed class UrlBuilder
    {
        private readonly string WotDomen = "api.worldoftanks";
        public static string[] WotLangs = new string[] { "ru", "eu", "na", "asia" };
        private readonly string Shema = "https";
        private readonly string SearchPlayersUrl = "/wot/account/list/?application_id={app}&search={phrase}";
        private readonly string PlayerStatisticUrl = "/wot/account/info/?application_id={app}&account_id={accountId}&extra=statistics.random,statistics.globalmap_absolute,statistics.globalmap_champion,statistics.globalmap_middle,"; 

        private readonly string _appId;

        public UrlBuilder(string appId)
        {
            _appId = appId;
        }

        public string GetSearchUrl(string searchPhrase, string lang)
        {
            return $"{Shema}://{WotDomen}.{lang}{SearchPlayersUrl.Replace("{app}", _appId).Replace("{phrase}", searchPhrase)}";
        }

        public string GetPlayerDataUrl(string accountId, string lang)
        {
            return $"{Shema}://{WotDomen}.{lang}{PlayerStatisticUrl.Replace("{app}", _appId).Replace("{accountId}", accountId)}";
        }
    }
}
