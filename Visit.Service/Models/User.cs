using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace visitsvc.Models
{
    public sealed partial class User : IdentityUser
    {
        public User()
        {
            UserLocation = new HashSet<UserLocation>();
        }
        public string FName { get; set; }
        public string LName { get; set; }
        public DateTime Birthday { get; set; }
        public string BirthPlace { get; set; }
        public string ResidesIn { get; set; }
        public string Education { get; set; }
        public string OccupationTitle { get; set; }
        public string Avi { get; set; }
        public ulong? FacebookId { get; set; }

        public ICollection<UserLocation>? UserLocation { get; set; }
    }
}
