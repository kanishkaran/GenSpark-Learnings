using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using AzureBlobs.Interfaces;

namespace AzureBlobs.Services
{
    public class AzureBlobService : IAzureBlobService
    {
        private readonly BlobContainerClient _blobClient;

        public AzureBlobService(IConfiguration configuration)
        {
            var sasUrl = configuration["AzureBlob:ContainerSasUrl"] ?? throw new Exception("No Such Url for blob container");
            _blobClient = new BlobContainerClient(new Uri(sasUrl));
        }
        public async Task<Stream?> DownloadFile(string fileName)
        {
            var blobClient = _blobClient.GetBlobClient(fileName);
            if (await blobClient.ExistsAsync())
            {
                var downloadInfo = await blobClient.DownloadStreamingAsync();
                return downloadInfo.Value.Content;
            }
            return null;
        }

        public async Task UploadFile(Stream fileStream, string fileName)
        {
            var blobClient = _blobClient.GetBlobClient(fileName);
            await blobClient.UploadAsync(fileStream, overwrite: true);
        }
    }
}