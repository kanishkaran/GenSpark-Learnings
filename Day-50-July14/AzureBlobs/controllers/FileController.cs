using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureBlobs.Dtos;
using AzureBlobs.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AzureBlobs.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileController : ControllerBase
    {
        private readonly IAzureBlobService _blobService;

        public FileController(IAzureBlobService azureBlobService)
        {
            _blobService = azureBlobService;
        }

         [HttpGet]
        public async Task<IActionResult> Download(string fileName)
        {
            var stream = await _blobService.DownloadFile(fileName);
            if (stream == null) 
                return NotFound();
            return File(stream, "application/octet-stream", fileName);
        }

    
        
        [HttpPost("upload")]

        public async Task<IActionResult> Upload([FromForm] UploadRequestDto request)
        {
            if (request.File == null || request.File.Length == 0)
                return BadRequest("No file to upload");
            using var stream = request.File.OpenReadStream();
            await _blobService.UploadFile(stream, request.File.FileName);
            return Ok("File uploaded");
        }
    }
}