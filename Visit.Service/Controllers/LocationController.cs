using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using visitsvc.BusinessLogic;
using visitsvc.DataAccess;
using visitsvc.Models;

namespace visitsvc.Controllers
{
    [Route("Location")]
    [EnableCors("CorsPolicy")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationBusinessLogic _locationBusinessLogic;

        public LocationController(ILocationBusinessLogic locationBusinessLogic)
        {
            _locationBusinessLogic = locationBusinessLogic;
        }
        
        // POST: Location/load
        /// <summary>
        /// Post countries to the DB
        /// </summary>
        [Route("load")]
        [HttpPost]
        public async Task<ActionResult> PostCountries()
        {
             await _locationBusinessLogic.LoadCountries("test");
             //todo add some verification to check if all locations were loaded 405??
             return Accepted();
        }
        
        /// <summary>
        /// Get location by ID
        /// </summary>
        [Authorize(Policy = "VisitUser")]
        [HttpGet("Code/{locationCode}")]
        public async Task<ActionResult<Location>> GetLocation(string locationCode)
        {
            return Ok(await _locationBusinessLogic.GetLocationById(locationCode));
        }

        /// <summary>
        /// Get location by Name
        /// </summary>
        //[Authorize(Policy = "VisitUser")]
        [HttpGet("Name/{name}")]
        public async Task<ActionResult<Location>> GetLocationByName(string name)
        {
            return Ok(await _locationBusinessLogic.GetLocationByName(name));
        }
        
        /// <summary>
        /// Get locations by Username
        /// </summary>
        [HttpGet("Username/{name}")]
        public async Task<IEnumerable<UserLocation>> GetLocationsByUserName(string name)
        {
            var result = await _locationBusinessLogic.GetAllLocationsByUsername(name).ToListAsync();

            return result;
        }
        
        
        /// <summary>
        /// Get locations by UserId
        /// </summary>
        [Authorize(Policy = "VisitUser")]
        [HttpGet("User/{id}")]
        public async Task<IEnumerable<UserLocation>> GetLocationsByUserId(string id)
        {
            var result = await _locationBusinessLogic.GetAllLocationsByUserId(id).ToListAsync();

            return result;
        }
    }
}
