using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using System.Threading.Tasks;

namespace FileHandleAPI.Models
{
    public class FileData
    {
        [Key]
        public int Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public byte[]? FileContent { get; set; }
        public string FileType { get; set; } = string.Empty;
    }
}