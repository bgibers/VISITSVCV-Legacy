using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace visitsvc.BusinessLogic
{
    public interface ICountryBusinessLogic
    {
        Task<IActionResult> LoadCountries(string filename);
    }
}