using DataStoringModule.Interfaces;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DataGatheringModule
{
    public sealed class Job
    {
        private readonly WotService _wotService;
        private readonly IPlayerFrameDataRepository _playerFrameDataRepository;
        private readonly IPlayerRepository _playerRepository;

        public Job(IPlayerRepository playerRepo,
            IPlayerFrameDataRepository playerFrameDataRepository)
        {
            _playerRepository = playerRepo;
            _playerFrameDataRepository = playerFrameDataRepository;
            _wotService = new WotService();
        }

        public async Task Run()
        {
            while (true)
            {
                var users = await _playerRepository.GetAllAsync();
                var frameId = Guid.NewGuid();
                foreach (var user in users)
                {
                    var playerData = await _wotService.LoadPersonalDataAsync(user.WotId, user.Lang);
                    var oldPlayerDataJson = await _playerFrameDataRepository.GetLastFrameForPlayer(user.Id);
                    if(oldPlayerDataJson != null)
                    {
                        var oldPlayerData = JsonConvert.DeserializeObject<Models.DtoModels.PlayerDataCollectionDto>(oldPlayerDataJson.Json);
                        if(oldPlayerData.Statistics.All.Battles == playerData.Data.Data.Statistics.All.Battles)
                        {
                            continue;
                        }
                    }
                    await _playerFrameDataRepository.InsertAsync(new Models.Entities.PlayerFrameData
                    {
                        Id = Guid.NewGuid(),
                        FrameId = frameId,
                        DateTime = DateTime.UtcNow,
                        Json = JsonConvert.SerializeObject(playerData.Data.Data.Statistics),
                        PlayerId = user.Id
                    });
                }

                Thread.Sleep(20 * 1000);
            }         
        }
    }
}
