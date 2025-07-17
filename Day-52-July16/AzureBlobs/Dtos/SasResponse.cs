using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureBlobs.Dtos
{
    public class SasResponse
    {
        public string sasUrl { get; set; } = string.Empty;
        public DateTimeOffset expiresOn { get; set; }
    }
}