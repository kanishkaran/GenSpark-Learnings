using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebWorker.Dto;
using WebWorker.interfaces;

namespace WebWorker.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileController : ControllerBase
    {
        private readonly IFileProcessingService _processingService;

        public FileController(IFileProcessingService processingService)
        {
            _processingService = processingService;
        }


        [HttpPost("FromCsv")]
        public async Task<IActionResult> BulkInsertFromCsv([FromBody] CsvUploadDto input)
        {
            return Ok(await _processingService.ProcessData(input));
        }
    }
}