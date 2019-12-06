using System;
using System.Collections.Generic;

namespace visitsvc.Models
{
    public partial class Location
    {
        public Location()
        {
            UserLocation = new HashSet<UserLocation>();
        }

        public string LocationId { get; set; }
        public string Name { get; set; }
        public string Filename { get; set; }

        public virtual ICollection<UserLocation>? UserLocation { get; set; }
    }
}
