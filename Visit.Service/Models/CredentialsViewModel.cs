using FluentValidation.Attributes;
using visitsvc.Models.Validators;

namespace visitsvc.Models
{
        [Validator(typeof(CredentialsViewModelValidator))]
        public class CredentialsViewModel
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }
    
}