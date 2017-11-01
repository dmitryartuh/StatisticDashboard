using System;
using System.Net.Http;
using System.Threading.Tasks;
using Models.DtoModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DataGatheringModule
{
    public sealed class WotService
    {
        private readonly HttpClient _httpClient;
        private readonly UrlBuilder _urlBuilder;

        public WotService()
        {
            _urlBuilder = new UrlBuilder("demo");
            _httpClient = new HttpClient();
            _httpClient.Timeout = new TimeSpan(0, 0, 0, 20);
            _httpClient.BaseAddress = new Uri(_urlBuilder.GetBaseUrl());
        }

        public async Task<PlayersSearchResultDto> LoadPlayerDataAsync(string searchPhrase)
        {
            return await Get<PlayersSearchResultDto>(_urlBuilder.GetSearchUrl(searchPhrase));
        }

        public async Task<PlayersSearchResultDto> LoadPersonalDataAsync(string accounntId)
        {
            return await Get<PlayersSearchResultDto>(_urlBuilder.GetPlayerDataUrl(accounntId));
        }

        public async Task<T> Get<T>(string url)
        {
            T model = default(T);
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
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