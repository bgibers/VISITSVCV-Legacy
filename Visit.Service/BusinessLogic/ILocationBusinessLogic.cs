using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using visitsvc.Models;

namespace visitsvc.BusinessLogic
{
    public interface ILocationBusinessLogic
    {
        //todo this should be locations instad of countries 
        Task<IActionResult> LoadCountries(string filename);
        Task<ActionResult<Location>> GetLocationById(string locationId);
        Task<ActionResult<Location>> GetLocationByName(string locationName);
        IQueryable<UserLocation> GetAllLocationsByUsername(string username);
        IQueryable<UserLocation> GetAllLocationsByUserId(string userId);
        bool LocationExistsByName(string location);

    }
}