using System;

namespace visitsvc.Models
{
    public class RegistrationUserApi
    {
        public string FName { get; set; }
        public string LName { get; set; }
        public string UserName { get; set; }
        public string BirthPlace { get; set; }
        public string ResidesIn { get; set; }
        public string Education { get; set; }
        public string OccupationTitle { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Birthday { get; set; }
    }
}