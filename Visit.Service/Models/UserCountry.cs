using System;
using System.Collections.Generic;

namespace visitsvc.Models
{
    public partial class UserCountry
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string CountryId { get; set; }
        public ulong ToVisit { get; set; }
        public ulong Visited { get; set; }
        public string SpecialCase { get; set; }

        public virtual Country Country { get; set; }
        public virtual User User { get; set; }
    }
}
