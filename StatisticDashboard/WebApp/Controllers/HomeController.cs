using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataGatheringModule;
using System.Collections.Generic;
using DataStoringModule.Interfaces;
using System;
using System.Linq;
using System.Threading;
using Newtonsoft.Json;

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
            //await LoadPlayersDataAsync();
            //await AddPlayersAsync(new List<string> {"titanik3d" });
            var sorClan = new List<string>
            {
                "_TT_c_u_X",
                "_APuCToKPaTKa_",
                "Vad_MC",
                "BRUNETKA_B_TAHKE",
                "esoxs_",
                "esoxs",
                "andrei352",
                "TwinkStrike",
                "Deman_ya",
                "__Miroxa__",
                "Holoin",
                "obo146",
                "Wohi",
                "_EMIL",
                "harconnens",
                "Megalodonnn77",
                "XXXmaestroXXX",
                "viktor971548",
                "Excello",
                "zykov4",
                "apiter",
                "levkaEKO",
                "KOJIEK1528",
                "Pro100Vezunchik_UA",
                "plohesh7",
                "vladislav3322",
                "MaTb_Storma",
                "3eJleHbIu_Bygy",
                "Dadyshkagoga",
                "Frankenste1n_ua",
                "sasha_ns",
                "Slavkadav",
                "1GREEK",
                "lapa24",
                "Ernsthaft",
                "_SAU_902",
                "TorNaDo_24RU",
                "11x11",
                "Snatchi",
                "gegok27",
                "mavrodi71",
                "x__KORVIN__x",
                "novichokcs1_and_world2",
                "shot_BROtishka",
                "_spaik13",
                "RulerOfTime",
                "flameinmyheart",
                "9I_KyCTIK_",
                "V_A_S_E_K_RUSSIA",
                "Buybrains",
                "B_E_R_4_I_K",
                "xX8ToRNAdO2Xx",
                "Legos46",
                "KIBORG_VS",
                "SuperDuperTank15",
                "k1zex",
                "_tazar_",
                "XxxTOR_N_ADOxxX",
                "svitoi92",
                "OhZaZa",
                "Reach99",
                "zzzruslan",
                "batlletoad",
                "Ha_cmBoJIe_BepmeJI",
                "YLbTPA_HARDKORIIINK"
            };
            //await AddPlayersAsync(checkClan);
            if (!IsRun)
            {
                var job = new Job(_playerRepository, _playerFrameDataRepository);
                Task.Run(() => job.Run());
                IsRun = true;
            }

            return View();
        }

        [NonAction]
        private async Task LoadPlayersDataAsync()
        {
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
            await AddPlayersAsync(players);
        }

        private async Task AddPlayersAsync(List<string> players)
        {
            WotService test = new WotService();
            var langs = UrlBuilder.WotLangs;
            var count = 0;
            foreach (var player in players)
            {
                foreach (var lang in langs)
                {
                    if (count == 20)
                    {
                        Thread.Sleep(1200);
                        count = 0;
                    }
                    count++;
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

        public async Task<IActionResult> Data()
        {
            return View(await _playerFrameDataRepository.GetAllFramesAsync());
        }
    }
}
