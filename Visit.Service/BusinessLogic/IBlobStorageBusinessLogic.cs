using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace visitsvc.BusinessLogic
{
    public interface IBlobStorageBusinessLogic
    {
        Task<List<string>> ListFiles();
        Task<bool> UploadFile(string fileName, IFormFile asset);
        Task<string> GetFileByName(string fileName);
        Task<bool> DeleteFile(string fileName);
    }
}