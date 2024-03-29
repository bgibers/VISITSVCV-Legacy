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
        private readonly VisitContext _context;

        public LocationBusinessLogic(VisitContext context)
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
                        Filename = obj.Id + ".jpg",
                        Name = obj.Name
                    };
                    await _context.Location.AddAsync(location);
                    await _context.SaveChangesAsync();
                }
            }
            return new AcceptedResult();
        }

        public async Task<Location> GetLocationById(string locationId)
        {
            var location = await _context.Location.FindAsync(locationId);
            
            return location;
        }

        public async Task<Location> GetLocationByName(string locationName)
        {
            var location = await _context.Location.FirstAsync( l => l.Name == locationName);
            return location;
        }

        public IQueryable<UserLocation> GetAllLocationsByUsername(string username)
        {
            var locations =  _context.UserLocation.Where(l => l.User.UserName == username);

            return locations;
        }

        public IQueryable<UserLocation> GetAllLocationsByUserId(string userId)
        {
            var locations =  _context.UserLocation.Where(l => l.User.Id == userId);

            return locations;
        }

        public bool LocationExistsByName(string location)
        {
            throw new System.NotImplementedException();
        }
    }
}