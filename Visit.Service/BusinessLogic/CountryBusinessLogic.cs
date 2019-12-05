using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using visitsvc.DataAccess;
using visitsvc.Models;

namespace visitsvc.BusinessLogic
{
    public class CountryBusinessLogic : ICountryBusinessLogic
    {
        private readonly visitContext _context;

        public CountryBusinessLogic(visitContext context)
        {
            this._context = context;
        }
        
        public async Task<IActionResult> LoadCountries(string filename)
        {
            Country country = new Country()
            {
                CountryId = "test",
                Filename = "test",
                Name = "tst",
                UserCountry = new List<UserCountry>()
            };
            await _context.Country.AddAsync(country);
            await _context.SaveChangesAsync();

            return new AcceptedResult();
        }
    }
}