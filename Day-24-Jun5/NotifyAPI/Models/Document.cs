using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NotifyAPI.Models
{
    public class Document
    {
        [Key]
        public int DocumentId { get; set; }
        public string DocumentName { get; set; } = string.Empty;
        public byte[]? DocumentContent { get; set; }
        public string DocumentType { get; set; } = string.Empty;
        public int UploadedById { get; set; }

        public Employee? UploadedBy { get; set; }
    }
}