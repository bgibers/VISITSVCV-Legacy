using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using visitsvc.BusinessLogic;
using visitsvc.DataAccess;
using visitsvc.Models;

namespace visitsvc.Controllers
{
    [Route("api/Country")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryBusinessLogic _countryBusinessLogic;

        public CountryController(ICountryBusinessLogic countryBusinessLogic)
        {
            _countryBusinessLogic = countryBusinessLogic;
        }
        
        public async Task<ActionResult> PostCountries()
        {
             await _countryBusinessLogic.LoadCountries("test");
             return Accepted();
        }
//        // GET: api/Country
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Country>>> GetCountries()
//        {
//            return await _context.Country.ToListAsync();
//        }

//        // GET: api/Country/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<Country>> GetCountry(string id)
//        {
//            var country = await _context.Country.FindAsync(id);
//
//            if (country == null)
//            {
//                return NotFound();
//            }
//
//            return country;
//        }
//
//        //todo get country by name
//
//
//        private bool CountryExists(string id)
//        {
//            return _context.Country.Any(e => e.CountryId == id);
//        }
    }
}
