using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebWorker.contexts;
using WebWorker.Dto;
using WebWorker.interfaces;

namespace WebWorker.services
{
    public class FileProcessingService : IFileProcessingService
    {
        private readonly WebWorkerDBContext _context;

        public FileProcessingService(WebWorkerDBContext context)
        {
            _context = context;
        }
        public async Task<FileUploadReturnDto> ProcessData(CsvUploadDto csvUploadDto)
        {
            var connection = _context.Database.GetDbConnection();
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM process_csv(:csv_input)";

            command.CommandType = CommandType.Text;

            var param = command.CreateParameter();
            param.ParameterName = "csv_input";
            param.Value = csvUploadDto.CsvContent;
            command.Parameters.Add(param);

            using var reader = await command.ExecuteReaderAsync();

            var errorRows = new List<string>();
            while (await reader.ReadAsync())
            {
                errorRows.Add(reader.GetString(0));
            }
            return new FileUploadReturnDto{ Inserted = "CSV Processed", Errors = errorRows.ToArray() };
        }
    }
}