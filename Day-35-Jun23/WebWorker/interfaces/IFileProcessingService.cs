using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebWorker.Dto;

namespace WebWorker.interfaces
{

    public interface IFileProcessingService
    {
        public Task<FileUploadReturnDto> ProcessData(CsvUploadDto csvUploadDto);
    }

    
}