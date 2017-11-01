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

        public async Task<PlayersSearchResultDto> LoadDateAsync(string searchPhrase)
        {
            var model = default(PlayersSearchResultDto);
            var response =
                await
                    _httpClient.GetAsync(_urlBuilder.GetSearchUrl(searchPhrase));
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                model = JsonConvert.DeserializeObject<PlayersSearchResultDto>(json,
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