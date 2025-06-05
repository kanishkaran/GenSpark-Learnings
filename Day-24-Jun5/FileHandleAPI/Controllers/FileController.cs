using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            try
            {
                var result = await _fileHandleService.PostFile(file.FileDetails, file.FileType);
                return result;
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }


        [HttpGet("GetFile")]
        public async Task<ActionResult<FileGetDto>> GetFile(int id)
        {
            try
            {
                var result = await _fileHandleService.DownloadFileById(id);
                return result;
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}