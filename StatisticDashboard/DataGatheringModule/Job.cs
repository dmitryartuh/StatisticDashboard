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
            try
            {
                while (true)
                {
                    var count = 0;
                    var users = await _playerRepository.GetAllAsync();
                    var frameId = Guid.NewGuid();
                    //Console.WriteLine($"Started {frameId}");
                    foreach (var user in users)
                    {
                        if (count == 19)
                        {
                            Thread.Sleep(2000);
                            count = 0;
                        }
                        count++;
                        var playerData = await _wotService.LoadPersonalDataAsync(user.WotId, user.Lang);
                        if (playerData.Status == "error")
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"{user.Nickname} - {playerData?.Error?.Message}");
                            //Console.Beep(500, 1000);
                            Console.ForegroundColor = ConsoleColor.Black;
                            continue;
                        }
                        var oldPlayerDataJson = await _playerFrameDataRepository.GetLastFrameForPlayerAsync(user.Id);
                        if (oldPlayerDataJson != null)
                        {
                            var playerStatisticDto = JsonConvert.DeserializeObject<Models.DtoModels.PlayerStatisticDto>(oldPlayerDataJson.Json);

                            if (playerStatisticDto.All.Battles == playerData.Data.Data.Statistics.All.Battles)
                            {
                                continue;
                            }
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"{user.Nickname} - {playerData.Data.Data.Statistics.All.Battles - playerStatisticDto.All.Battles}");
                            Console.ForegroundColor = ConsoleColor.Black;
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
                    //Console.ForegroundColor = ConsoleColor.Yellow;
                    //Console.WriteLine($"Finished {frameId}");
                    //Console.ForegroundColor = ConsoleColor.Black;
                    Thread.Sleep(30 * 1000);
                }
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e);
                Console.Beep(500, 1000);
                Console.ForegroundColor = ConsoleColor.Black;
            }
        }
    }
}
