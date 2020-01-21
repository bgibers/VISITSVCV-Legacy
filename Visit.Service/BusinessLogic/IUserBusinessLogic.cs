using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using visitsvc.Models;

namespace visitsvc.BusinessLogic
{
    public interface IUserBusinessLogic
    {
        Task<IActionResult> RegisterUser(RegistrationUserApi user);
        Task<JwtToken> LoginUser(CredentialsViewModel credentials);
        Task<LoggedInUser> GetCurrentUser(Claim user);
        Task<IdentityResult> UploadProfileImage(IFormFile image, Claim user);
        Task<bool> UserNameEmailTaken(string login);
        Task<IEnumerable<User>> GetAllUsers();
    }
}