using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using visitsvc.BusinessLogic;
using visitsvc.DataAccess;
using visitsvc.Models;

namespace visitsvc.Controllers
{
    [Route("Location")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationBusinessLogic _LocationBusinessLogic;

        public LocationController(ILocationBusinessLogic LocationBusinessLogic)
        {
            _LocationBusinessLogic = LocationBusinessLogic;
        }
        
        // POST: Location/load
        /// <summary>
        /// Post countries to the DB
        /// </summary>
        [Route("load")]
        [HttpPost]
        public async Task<ActionResult> PostCountries()
        {
             await _LocationBusinessLogic.LoadCountries("test");
             return Accepted();
        }
        
        /// <summary>
        /// Get location by ID
        /// </summary>
        [HttpGet("Code/{locationCode}")]
        public async Task<ActionResult<Location>> GetLocation(string locationCode)
        {
            var result = await _LocationBusinessLogic.GetLocationById(locationCode);

            return result.Value == null ? NotFound() : result;
        }

        /// <summary>
        /// Get location by Name
        /// </summary>
        [HttpGet("Name/{name}")]
        public async Task<ActionResult<Location>> GetLocationByName(string name)
        {
            var result = await _LocationBusinessLogic.GetLocationByName(name);

            return result.Value == null ? NotFound() : result;
        }
        
        /// <summary>
        /// Get locations by Username
        /// </summary>
        [Authorize(Policy = "VisitUser")]
        [HttpGet("Username/{name}")]
        public async Task<IEnumerable<UserLocation>> GetLocationsByUserName(string name)
        {
            var result = await _LocationBusinessLogic.GetAllLocationsByUsername(name).ToListAsync();

            return result;
        }
        
        
        /// <summary>
        /// Get locations by UserId
        /// </summary>
        [HttpGet("User/{id}")]
        public async Task<IEnumerable<UserLocation>> GetLocationsByUserId(string id)
        {
            var result = await _LocationBusinessLogic.GetAllLocationsByUserId(id).ToListAsync();

            return result;
        }
    }
}
