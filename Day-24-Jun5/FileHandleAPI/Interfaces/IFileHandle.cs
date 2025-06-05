using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileHandleAPI.Models;

namespace FileHandleAPI.Interfaces
{
    public interface IFileHandle
    {
        public Task<string> PostFile(IFormFile fileData, string fileType);

        public Task<FileData> DownloadFileById(int id);
    }
}