using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileHandleAPI.Models.Dtos
{
    public class FileUploadDto
    {
        public IFormFile FileDetails { get; set; }
        public string FileType { get; set; } = string.Empty;
    }
}