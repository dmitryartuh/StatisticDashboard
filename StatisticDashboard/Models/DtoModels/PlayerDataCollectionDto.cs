using System;

namespace Models.DtoModels
{
    public class PlayerDataCollectionDto
    {
        public int? ClanId { get; set; }
        public string ClientLanguage { get; set; }
        public string CreatedAt { get; set; }
        public int GlobalRating { get; set; }
        public string LastBattleTime { get; set; }
        public string LogoutAt { get; set; }
        public string Nickname { get; set; }
        public string UpdatedAt { get; set; }
        public PlayerStatisticDto Statistics { get; set; }
    }
}
