using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using visitsvc.DataAccess;
using visitsvc.Models.ConfigModels;

namespace visitsvc.BusinessLogic
{
    public class BlobStorageBusinessLogic : IBlobStorageBusinessLogic
    {
        private readonly VisitContext _context;
        private readonly IOptions<BlobConfig> _config;


        public BlobStorageBusinessLogic(IOptions<BlobConfig> config, VisitContext context)
        {
            _config = config;
            _context = context;
        }

        public async Task<List<string>> ListFiles()
        {
            List<string> blobs = new List<string>();
            try
            {
                if (CloudStorageAccount.TryParse(_config.Value.StorageConnection,
                    out CloudStorageAccount storageAccount))
                {
                    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                    CloudBlobContainer container = blobClient.GetContainerReference(_config.Value.Container);

                    BlobResultSegment resultSegment = await container.ListBlobsSegmentedAsync(null);
                    foreach (IListBlobItem item in resultSegment.Results)
                    {
                        if (item.GetType() == typeof(CloudBlockBlob))
                        {
                            CloudBlockBlob blob = (CloudBlockBlob) item;
                            blobs.Add(blob.Name);
                        }
                        else if (item.GetType() == typeof(CloudPageBlob))
                        {
                            CloudPageBlob blob = (CloudPageBlob) item;
                            blobs.Add(blob.Name);
                        }
                        else if (item.GetType() == typeof(CloudBlobDirectory))
                        {
                            CloudBlobDirectory dir = (CloudBlobDirectory) item;
                            blobs.Add(dir.Uri.ToString());
                        }
                    }
                }
            }
            catch
            {
            }

            return blobs;
        }

        public async Task<bool> UploadFile(string fileName, IFormFile asset)
        {
            try
            {
                if (CloudStorageAccount.TryParse(_config.Value.StorageConnection,
                    out CloudStorageAccount storageAccount))
                {
                    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                    CloudBlobContainer container = blobClient.GetContainerReference(_config.Value.Container);

                    CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);
    
                    await blockBlob.UploadFromStreamAsync(asset.OpenReadStream());

                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<string> GetFileByName(string fileName)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                if (CloudStorageAccount.TryParse(_config.Value.StorageConnection,
                    out CloudStorageAccount storageAccount))
                {
                    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                    CloudBlobContainer container = blobClient.GetContainerReference(_config.Value.Container);
                    if (await container.ExistsAsync())
                    {
                        CloudBlob file = container.GetBlobReference(fileName);
                        await file.FetchAttributesAsync();
                        byte[] arr = new byte[file.Properties.Length];
                        await file.DownloadToByteArrayAsync(arr, 0);
                        var fileBase64 = Convert.ToBase64String(arr);
                        return fileBase64;
                    }

                    throw new StorageException("Container does not exist");
                }
            }
            catch (Exception)
            {
            }
            return null;
        }
        
        public async Task<bool> DeleteFile(string fileName)
        {
            try
            {
                if (CloudStorageAccount.TryParse(_config.Value.StorageConnection,
                    out CloudStorageAccount storageAccount))
                {
                    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                    CloudBlobContainer container = blobClient.GetContainerReference(_config.Value.Container);

                    if (await container.ExistsAsync())
                    {
                        CloudBlob file = container.GetBlobReference(fileName);

                        if (await file.ExistsAsync())
                        {
                            await file.DeleteAsync();
                        }
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}