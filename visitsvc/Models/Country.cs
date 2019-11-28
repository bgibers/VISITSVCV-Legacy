using System;
using System.Collections.Generic;

namespace visitsvc.Models
{
    public partial class Country
    {
        public Country()
        {
            UserCountry = new HashSet<UserCountry>();
        }

        public string CountryId { get; set; }
        public string Name { get; set; }
        public string Filename { get; set; }

        public virtual ICollection<UserCountry> UserCountry { get; set; }
    }
}
