﻿using System;
using System.Collections.Generic;

namespace visitsvc.Models
{
    public partial class User
    {
        public User()
        {
            UserCountry = new HashSet<UserCountry>();
        }

        public int UserId { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public DateTime Birthday { get; set; }
        public string Email { get; set; }
        public string PwordSalt { get; set; }
        public string PwordHash { get; set; }
        public byte[]? Avi { get; set; }
        public ulong? FacebookId { get; set; }

        public virtual ICollection<UserCountry>? UserCountry { get; set; }
    }
}
