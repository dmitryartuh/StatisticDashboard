namespace Models.Entities
{
    public class Player : BaseEntity
    {
        public string WotId { get; set; }
        public string Nickname { get; set; }
        public string Lang { get; set; }
    }
}