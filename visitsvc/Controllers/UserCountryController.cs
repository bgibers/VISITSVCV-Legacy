using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using visitsvc.DataAccess;
using visitsvc.Models;

namespace visitsvc.Controllers
{
    [Route("api/UserCountry")]
    [ApiController]
    public class UserCountryController : ControllerBase
    {
        private readonly visitContext _context;

        public UserCountryController(visitContext context)
        {
            _context = context;
        }

        // GET: api/UserCountry
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserCountry>>> GetUserCountry()
        {
            return await _context.UserCountry.ToListAsync();
        }

        // GET: api/UserCountry/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserCountry>> GetUserCountry(int id)
        {
            var userCountry = await _context.UserCountry.FindAsync(id);

            if (userCountry == null)
            {
                return NotFound();
            }

            return userCountry;
        }

        // PUT: api/UserCountry/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserCountry(int id, UserCountry userCountry)
        {
            if (id != userCountry.Id)
            {
                return BadRequest();
            }

            _context.Entry(userCountry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserCountryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/UserCountry
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<UserCountry>> PostUserCountry(UserCountry userCountry)
        {
            _context.UserCountry.Add(userCountry);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserCountry", new { id = userCountry.Id }, userCountry);
        }

        // DELETE: api/UserCountry/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserCountry>> DeleteUserCountry(int id)
        {
            var userCountry = await _context.UserCountry.FindAsync(id);
            if (userCountry == null)
            {
                return NotFound();
            }

            _context.UserCountry.Remove(userCountry);
            await _context.SaveChangesAsync();

            return userCountry;
        }

        private bool UserCountryExists(int id)
        {
            return _context.UserCountry.Any(e => e.Id == id);
        }
    }
}
