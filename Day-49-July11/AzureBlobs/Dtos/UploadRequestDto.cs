using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureBlobs.Dtos
{
    public class UploadRequestDto
    {
        public IFormFile? File { get; set; }
    }
}