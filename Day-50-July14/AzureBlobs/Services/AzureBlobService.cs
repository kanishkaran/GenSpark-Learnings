using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Azure.Storage.Blobs;
using AzureBlobs.Interfaces;

namespace AzureBlobs.Services
{
    public class AzureBlobService : IAzureBlobService
    {
        private BlobContainerClient _blobClient;
        private readonly IConfiguration _configuration;

        public AzureBlobService(IConfiguration configuration)
        {
            _configuration = configuration;
            // var sasUrl = configuration["AzureBlob:ContainerSasUrl"] ?? throw new Exception("No Such Url for blob container");
            // _blobClient = new BlobContainerClient(new Uri(sasUrl));
        }

        private async Task GetContainerClient()
        {
            var keyVaultUrl = _configuration["AzureBlob:KeyVaultUrl"] ?? throw new Exception("No key Vault Url Found");
            SecretClient secretClient = new SecretClient(new Uri(keyVaultUrl), new DefaultAzureCredential());
            KeyVaultSecret vaultSecret = await secretClient.GetSecretAsync(_configuration["AzureBlob:SecretName"]);
            var containerSasUrl = vaultSecret.Value;
            _blobClient = new BlobContainerClient(new Uri(containerSasUrl));
        }
        public async Task<Stream?> DownloadFile(string fileName)
        {
            await GetContainerClient();
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
            await GetContainerClient();
            var blobClient = _blobClient.GetBlobClient(fileName);
            await blobClient.UploadAsync(fileStream, overwrite: true);
        }
    }
}