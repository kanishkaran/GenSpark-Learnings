using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCrypt.Net;
using FileHandleAPI.Interfaces;
using FileHandleAPI.Models;
using FileHandleAPI.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FileHandleAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileController : Controller
    {
        private readonly IFileHandleService _fileHandleService;

        public FileController(IFileHandleService fileHandle)
        {
            _fileHandleService = fileHandle;
        }

        [HttpPost("upload")]
        public async Task<ActionResult<string>> UploadDocument([FromForm] FileUploadDto file)
        {
            return file.FileDetails.ContentType + " "+ Path.GetExtension(file.FileDetails.FileName);
        }


        [HttpGet("GetFile")]
        public async Task<ActionResult<string>> GetFile(int id)
        {
            try
            {
                return BCrypt.Net.BCrypt.HashPassword("admin123");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}