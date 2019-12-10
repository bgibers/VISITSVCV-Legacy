using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using visitsvc.Models;

namespace visitsvc.BusinessLogic
{
    public interface IUserBusinessLogic
    {
        Task<IActionResult> RegisterUser(RegistrationUserApi user);
        Task<string> LoginUser(CredentialsViewModel credentials);
        Task<IEnumerable<User>> GetAllUsers();

    }
}