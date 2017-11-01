namespace DataGatheringModule
{
    public sealed class UrlBuilder
    {
        private readonly string WotDomen = "api.worldoftanks.ru";
        private readonly string Shema = "https";
        private readonly string SearchPlayersUrl = "/wot/account/list/?application_id={app}&search={phrase}";

        private readonly string _appId;

        public UrlBuilder(string appId)
        {
            _appId = appId;
        }

        public string GetBaseUrl()
        {
            return $"{Shema}://{WotDomen}";
        }

        public string GetSearchUrl(string searchPhrase)
        {
            return $"{SearchPlayersUrl.Replace("{app}", _appId).Replace("{phrase}", searchPhrase)}";
        }
    }
}
