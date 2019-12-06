using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using visitsvc.DataAccess;
using visitsvc.Models;

namespace visitsvc.BusinessLogic
{
    public class LocationBusinessLogic : ILocationBusinessLogic
    {
        private readonly visitContext _context;

        public LocationBusinessLogic(visitContext context)
        {
            this._context = context;
        }
        
        public async Task<IActionResult> LoadCountries(string filename)
        {
            var canadaJson = File.ReadAllText("../Visit.Service/Assets/canada.json");
            var countriesJson = File.ReadAllText("../Visit.Service/Assets/countries.json");
            var russiaJson = File.ReadAllText("../Visit.Service/Assets/russia.json");
            var unitedStatesJson = File.ReadAllText("../Visit.Service/Assets/unitedstates.json");
            
            var filesList = new List<string>() {canadaJson,countriesJson,russiaJson,unitedStatesJson};

            foreach (var file in filesList)
            {
                var myJsonLocations = JsonConvert.DeserializeObject<LocationJsonList>(file);
                foreach (var obj in myJsonLocations.LocationList)
                {
                    Location location = new Location()
                    {
                        LocationId = obj.Id,
                        Filename = ".json",
                        Name = obj.Name
                    };
                    await _context.Location.AddAsync(location);
                    await _context.SaveChangesAsync();
                }
            }
            return new AcceptedResult();
        }

        public async Task<ActionResult<Location>> GetLocationById(string locationId)
        {
            var location = await _context.Location.FindAsync(locationId);
            
            return location;
        }

        public async Task<ActionResult<Location>> GetLocationByName(string locationName)
        {
            var location = await _context.Location.FirstAsync( l => l.Name == locationName);
            return location;
        }

        public IQueryable<UserLocation> GetAllLocationsByUsername(string username)
        {
            var locations =  _context.UserLocation.Where(l => l.User.Uname == username);

            return locations;
        }

        public IQueryable<UserLocation> GetAllLocationsByUserId(int userId)
        {
            var locations =  _context.UserLocation.Where(l => l.User.UserId == userId);

            return locations;
        }

        public bool LocationExistsByName(string location)
        {
            throw new System.NotImplementedException();
        }
    }
}