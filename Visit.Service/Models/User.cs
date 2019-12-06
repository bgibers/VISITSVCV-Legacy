using System;
using System.Collections.Generic;

namespace visitsvc.Models
{
    public sealed partial class User
    {
        public User()
        {
            UserLocation = new HashSet<UserLocation>();
        }

        public int UserId { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Uname { get; set; }
        public DateTime Birthday { get; set; }
        public string Email { get; set; }
        public string PwordSalt { get; set; }
        public string PwordHash { get; set; }
        public byte[] Avi { get; set; }
        public ulong? FacebookId { get; set; }

        public ICollection<UserLocation> UserLocation { get; set; }
    }
}
