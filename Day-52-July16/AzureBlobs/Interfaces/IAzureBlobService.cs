using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureBlobs.Interfaces
{
    public interface IAzureBlobService
    {
        Task UploadFile(Stream fileStream, string fileName);
        Task<Stream?> DownloadFile(string fileName);
        
    }
}