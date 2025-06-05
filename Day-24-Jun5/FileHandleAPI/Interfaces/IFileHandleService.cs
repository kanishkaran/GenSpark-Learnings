using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileHandleAPI.Models;
using FileHandleAPI.Models.Dtos;

namespace FileHandleAPI.Interfaces
{
    public interface IFileHandleService
    {
        public Task<string> PostFile(IFormFile fileData, string fileType);

        public Task<FileGetDto> DownloadFileById(int id);
    }
}