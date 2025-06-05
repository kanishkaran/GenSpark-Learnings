using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NotifyAPI.Models.Dtos;


namespace NotifyAPI.Interfaces
{
    public interface IFileHandleService
    {
        Task<string> PostFile(FileUploadDto fileItem, string userName);
        public Task<FileGetDto> DownloadFileById(int id);
    }
}