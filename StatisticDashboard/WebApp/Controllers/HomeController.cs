using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataGatheringModule;
using System.Collections.Generic;
using DataStoringModule.Interfaces;
using System;
using System.Linq;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPlayerFrameDataRepository _playerFrameDataRepository;
        private readonly IPlayerRepository _playerRepository;

        static bool IsRun = false;

        public HomeController(IPlayerRepository playerRepo,
            IPlayerFrameDataRepository playerFrameDataRepository)
        {
            _playerRepository = playerRepo;
            _playerFrameDataRepository = playerFrameDataRepository;
        }

        public async Task<IActionResult> Index()
        {
            //await LoadPlayersData();
            var test = new WotService();
            if (!IsRun)
            {
                var job = new Job(_playerRepository, _playerFrameDataRepository);
                Task.Run(() => job.Run());
                IsRun = true;
            }

            return View();
        }

        [NonAction]
        private async Task LoadPlayersData()
        {
            var test = new WotService();
            var players = new List<string>//[IOOTH] One of The Hundred (2)
            {
                "First_of_the_hundred",
                "Nunude",
                "Arny96",
                "dolan9",
                "SwordSaw",
                "simpules",
                "alonsanfan",
                "RenamedUser_507621806",
                "army54321",
                "ochotny",
                "Knedlicek2002",
                "likmum001",
                "Butchers_Dog",
                "Brauny_CZ",
                "Hrdla19",
                "jendapank",
                "Drevox"
            };
            var langs = UrlBuilder.WotLangs;
            foreach (var player in players)
            {
                foreach (var lang in langs)
                {
                    var playerData = await test.LoadPlayerDataAsync(player, lang);
                    var accountId = playerData.Data?.FirstOrDefault()?.AccountId;
                    if (!string.IsNullOrEmpty(accountId))
                    {
                        await _playerRepository.AddAsync(new Models.Entities.Player
                        {
                            Id = Guid.NewGuid(),
                            WotId = accountId,
                            Nickname = player,
                            Lang = lang
                        });

                        break;
                    }
                }
            }
        }

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }
    }
}
