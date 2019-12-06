using System;
using System.Collections.Generic;

namespace visitsvc.Models
{
    public partial class UserLocation
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string LocationId { get; set; }
        public ulong ToVisit { get; set; }
        public ulong Visited { get; set; }
        public string SpecialCase { get; set; }

        public virtual Location Location { get; set; }
        public virtual User User { get; set; }
    }
}
