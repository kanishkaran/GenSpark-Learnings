using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileHandleAPI.Models.Dtos
{
    public class FileGetDto
    {
        public int FileId { get; set; }
        public string FileContent { get; set; }

        public string FileType { get; set; } = string.Empty;
    }
}