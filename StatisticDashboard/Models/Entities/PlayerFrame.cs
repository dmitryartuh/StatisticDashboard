using System;

namespace Models.Entities
{
    public class PlayerFrame
    {
        public string Nickname { get; set; }
        public string Clan { get; set; }

        public Guid FrameId { get; set; }

        public string Json { get; set; }

        public DateTime DateTime { get; set; }

    }
}