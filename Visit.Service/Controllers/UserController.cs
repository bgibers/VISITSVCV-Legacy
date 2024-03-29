using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using visitsvc.BusinessLogic;
using visitsvc.Models;

namespace visitsvc.Controllers
{
    [Route("User")]
    [EnableCors("CorsPolicy")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBusinessLogic _userBusinessLogic;

        public UserController(IUserBusinessLogic userBusinessLogic)
        {
            _userBusinessLogic = userBusinessLogic;
        }

//        // GET: api/User
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
//        {
//            return await _context.User.ToListAsync();
//        }
//
//        // GET: api/User/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<User>> GetUser(int id)
//        {
//            var user = await _context.User.FindAsync(id);
//            
//            if (user == null)
//            {
//                return NotFound();
//            }
//
//            return user;
//        }
//
//
//        [HttpGet("tst")]
//        public async Task<ActionResult<User>> Test()
//        {
//            return await _context.User.FirstAsync(u => u.Uname == "test");
//        }

       

        // POST: api/User
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<User>> RegisterUser([FromBody]RegistrationUserApi user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _userBusinessLogic.RegisterUser(user));
        }

        [HttpGet("exists")]
        public async Task<bool> DoesUserExist(string login)
        {
            return await _userBusinessLogic.UserNameEmailTaken(login);
        }

        [Authorize(Policy = "VisitUser")]
        [HttpGet("self")]
        public async Task<LoggedInUser> GetCurrentUser()
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier);

            return await _userBusinessLogic.GetCurrentUser(user);
        }
        
        [Authorize(Policy = "VisitUser")]
        [HttpPost("update/profileimage")]
        public async Task<IdentityResult> UpdateProfileImage(IFormFile image)
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier);

            return await _userBusinessLogic.UploadProfileImage(image,user);
        }

        // POST api/auth/login
        [HttpPost("login")]
        public async Task<JwtToken> LoginUser([FromBody]CredentialsViewModel credentials)
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            return await _userBusinessLogic.LoginUser(credentials);
        }
        
        
//        // DELETE: api/User/5
//        [HttpDelete("{id}")]
//        public async Task<ActionResult<User>> DeleteUser(int id)
//        {
//            var user = await _context.User.FindAsync(id);
//            if (user == null)
//            {
//                return NotFound();
//            }
//
//            _context.User.Remove(user);
//            await _context.SaveChangesAsync();
//
//            return user;
//        }
//
//        private bool UserExists(string id)
//        {
//            return _context.User.Any(e => e.Id == id);
//        }
    }
}
