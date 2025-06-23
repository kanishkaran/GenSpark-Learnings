using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebWorker.Dto
{
    public class FileUploadReturnDto
    {
        public string? Inserted { get; set; }

        public string[]? Errors { get; set; }
    }
}