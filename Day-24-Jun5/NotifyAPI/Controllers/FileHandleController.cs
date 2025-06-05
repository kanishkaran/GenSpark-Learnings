using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotifyAPI.Interfaces;
using NotifyAPI.Models;
using NotifyAPI.Models.Dtos;

namespace NotifyAPI.Controllers
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
        [Authorize(Roles ="HR")]
        public async Task<ActionResult<string>> UploadDocument([FromForm] FileUploadDto file)
        {
            try
            {
                var user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var result = await _fileHandleService.PostFile(file, user);
                return result;
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }


        [HttpGet("GetFile")]
        [Authorize]
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