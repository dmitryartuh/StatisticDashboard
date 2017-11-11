using System;
using System.Net.Http;
using System.Threading.Tasks;
using Models.DtoModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text.RegularExpressions;

namespace DataGatheringModule
{
    public sealed class WotService
    {
        private readonly HttpClient _httpClient;
        private readonly UrlBuilder _urlBuilder;

        public WotService()
        {
            _urlBuilder = new UrlBuilder("e0d6e145de3fbf53ca18ee14a9e50444");
            _httpClient = new HttpClient();
            _httpClient.Timeout = new TimeSpan(0, 0, 0, 20);
        }

        public async Task<PlayersSearchResultDto> LoadPlayerDataAsync(string searchPhrase, string lang)
        {
            return await Get<PlayersSearchResultDto>(_urlBuilder.GetSearchUrl(searchPhrase, lang));
        }

        public async Task<PlayerDataResultDto> LoadPersonalDataAsync(string accounntId, string lang)
        {
            return await Get<PlayerDataResultDto>(_urlBuilder.GetPlayerDataUrl(accounntId, lang), accounntId);
        }

        public async Task<T> Get<T>(string url, string accountId = null)
        {
            T model = default(T);
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                if(accountId != null)
                {
                    var regex = new Regex(Regex.Escape(accountId));
                    json = regex.Replace(json, "data", 1);
                }
                model = JsonConvert.DeserializeObject<T>(json,
                    new JsonSerializerSettings
                    {
                        ContractResolver = new DefaultContractResolver
                        {
                            NamingStrategy = new SnakeCaseNamingStrategy()
                        }
                    });
            }

            return model;
        }
    }
}