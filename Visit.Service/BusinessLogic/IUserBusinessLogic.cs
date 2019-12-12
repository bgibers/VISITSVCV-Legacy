using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using visitsvc.Models;

namespace visitsvc.BusinessLogic
{
    public interface IUserBusinessLogic
    {
        Task<IActionResult> RegisterUser(RegistrationUserApi user);
        Task<JwtToken> LoginUser(CredentialsViewModel credentials);
        Task<User> GetCurrentUser(Claim user);
        Task<IEnumerable<User>> GetAllUsers();

    }
}