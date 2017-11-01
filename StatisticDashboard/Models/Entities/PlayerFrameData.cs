using System;

namespace Models.Entities
{
    public class PlayerFrameData : BaseEntity
    {
        public Guid PlayerId { get; set; }

        public string Json { get; set; }

        public DateTime DateTime { get; set; }
    }
}