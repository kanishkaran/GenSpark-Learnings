using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileHandleAPI.Context;
using FileHandleAPI.Interfaces;
using FileHandleAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FileHandleAPI.Services
{
    public class FIleHandle : IFileHandle
    {
        private readonly FileHandleContext _context;

        public FIleHandle(FileHandleContext context)
        {
            _context = context;
        }
        public async Task<FileData> DownloadFileById(int id)
        {
            try
            {
                var file = await _context.Files.FirstOrDefaultAsync(file => file.Id == id);

                if (file == null)
                {
                    throw new FileNotFoundException("file is not found");
                }
                return file;
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public async Task<string> PostFile(IFormFile fileData, string fileType)
        {
            try
            {
                var file = new FileData
                {
                    FileName = fileData.FileName,
                    FileType = fileType
                };

                using (var stream = new MemoryStream())
                {
                    fileData.CopyTo(stream);
                    file.FileContent = stream.ToArray();
                }

                var result = await _context.AddAsync(file);
                await _context.SaveChangesAsync();
                return "File saved"; 
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}