
using Azure.Storage.Blobs;
using AzureBlobs.Dtos;
using AzureBlobs.Interfaces;

namespace AzureBlobs.Services
{
    public class AzureBlobService : IAzureBlobService
    {

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<AzureBlobService> _logger;

        public AzureBlobService(
            IHttpClientFactory httpClientFactory,
            ILogger<AzureBlobService> logger)
        {

            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        private async Task<BlobClient> GetBlobClientWithSas(string fileName)
        {
            string functionUrl = $"https://dotnet-function-kanish.azurewebsites.net/api/generate-sas/{fileName}?code=";
            var client = _httpClientFactory.CreateClient();
            var sasResponse = await client.GetAsync(functionUrl);
            if (!sasResponse.IsSuccessStatusCode)
            {
                var error = await sasResponse.Content.ReadAsStringAsync();
                _logger.LogError($"Failed to get SAS URL: {error}");
                throw new InvalidOperationException("Could not obtain SAS URL.");
            }

            var sasData = await sasResponse.Content.ReadFromJsonAsync<SasResponse>();
            if (sasData == null || string.IsNullOrWhiteSpace(sasData.sasUrl))
            {
                throw new InvalidOperationException("SAS URL response invalid.");
            }

            _logger.LogInformation($"SAS URL obtained: {sasData.sasUrl}");

           
            return new BlobClient(new Uri(sasData.sasUrl));
        }

        public async Task UploadFile(Stream fileStream, string fileName)
        {
            var blobClient = await GetBlobClientWithSas(fileName);
            await blobClient.UploadAsync(fileStream, overwrite: true);
        }

        public async Task<Stream?> DownloadFile(string fileName)
        {
            var blobClient = await GetBlobClientWithSas(fileName);
            if (await blobClient.ExistsAsync())
            {
                var downloadInfo = await blobClient.DownloadStreamingAsync();
                return downloadInfo.Value.Content;
            }
            return null;
        }
    }
}