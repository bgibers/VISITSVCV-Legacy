using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using visitsvc.DataAccess;
using visitsvc.Models;
using AutoMapper;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using visitsvc.Auth;
using visitsvc.Helpers;

namespace visitsvc.BusinessLogic
{
    public class UserBusinessLogic : IUserBusinessLogic
    {
        
        private readonly VisitContext _visitContext;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;
        
        public UserBusinessLogic(VisitContext visitContext, UserManager<User> userManager, IJwtFactory jwtFactory, IOptions<JwtIssuerOptions>  jwtOptions, IMapper mapper)
        {
            _visitContext = visitContext;
            _userManager = userManager;
            _mapper = mapper;
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;
        }

        public async Task<IActionResult> RegisterUser(RegistrationUserApi model)
        {
            var userIdentity = _mapper.Map<User>(model);

            var result = await _userManager.CreateAsync(userIdentity, model.Password);

            if (!result.Succeeded) return null;

            await _visitContext.SaveChangesAsync();

            return new OkObjectResult("Account created");        
        }
        
        private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return await Task.FromResult<ClaimsIdentity>(null);

            // get the user to verifty
            var userToVerify = await _userManager.FindByNameAsync(userName);

            if (userToVerify == null) return await Task.FromResult<ClaimsIdentity>(null);

            // check the credentials
            if (await _userManager.CheckPasswordAsync(userToVerify, password))
            {
                return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(userName, userToVerify.Id));
            }

            // Credentials are invalid, or account doesn't exist
            return await Task.FromResult<ClaimsIdentity>(null);
        }

        public async Task<string> LoginUser(CredentialsViewModel credentials)
        {
            var identity = await GetClaimsIdentity(credentials.UserName, credentials.Password);
            
            if (identity == null)
            {
                return null;
            }

            return await Tokens.GenerateJwt(identity, _jwtFactory, credentials.UserName, _jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            throw new System.NotImplementedException();
        }

        public async Task<User> GetCurrentUser(Claim user)
        {
            return await _userManager.FindByNameAsync(user.Value);
        }
    }
}