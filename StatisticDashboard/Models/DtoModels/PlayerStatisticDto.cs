namespace Models.DtoModels
{
    public class PlayerStatisticDto
    {
        public PlayerDataDto Clan { get; set; }
        public PlayerDataDto All { get; set; }
        public PlayerDataDto RegularTeam { get; set; }
        public int TreesCut { get; set; }
        public PlayerDataDto Company { get; set; }
        public PlayerDataDto StrongholdSkirmish { get; set; }
        public PlayerDataDto StrongholdDefense { get; set; }
        public PlayerDataDto Team { get; set; }
    }
}
