using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using visitsvc.BusinessLogic;
using visitsvc.Models.ConfigModels;

namespace visitsvc.Controllers
{
    [Route("storage")]
    [ApiController]
    public class BlobStorageController : ControllerBase
    {
        private readonly IBlobStorageBusinessLogic _blobStorageBusinessLogic;

        public BlobStorageController(IBlobStorageBusinessLogic blobStorageBusinessLogic)
        {
            this._blobStorageBusinessLogic = blobStorageBusinessLogic;
        }

        [HttpGet("ListFiles")]
        public async Task<List<string>> ListFiles()
        {
            return await _blobStorageBusinessLogic.ListFiles();
        }

        [HttpPost("UploadFile")]
        public async Task<IActionResult> UploadFile(string fileName, IFormFile asset)
        {
            return Ok(await _blobStorageBusinessLogic.UploadFile(fileName, asset));
        }
        
        [HttpGet("DownloadFile/{fileName}")]
        public async Task<string> DownloadFile(string fileName)
        {
            return await _blobStorageBusinessLogic.GetFileByName(fileName);
        }


        [Route("DeleteFile/{fileName}")]
        [HttpGet]
        public async Task<IActionResult> DeleteFile(string fileName)
        {
            return Ok(await _blobStorageBusinessLogic.DeleteFile(fileName));
        }
    }
}