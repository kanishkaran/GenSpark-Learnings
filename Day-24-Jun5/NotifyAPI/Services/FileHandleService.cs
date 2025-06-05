using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using NotifyAPI.Contexts;
using NotifyAPI.Hubs;
using NotifyAPI.Interfaces;
using NotifyAPI.Models;
using NotifyAPI.Models.Dtos;

namespace NotifyAPI.Services
{
    public class FileHandleService : IFileHandleService
    {
        private readonly NotifyDBContext _context;
        private readonly IRepository<int, Employee> _employeeRepo;
        private readonly IHubContext<NotificationHub> _hub;

        public FileHandleService(NotifyDBContext context, IRepository<int, Employee> repository,
                                IHubContext<NotificationHub> hubContext)
        {
            _context = context;
            _employeeRepo = repository;
            _hub = hubContext;
        }
        public async Task<FileGetDto> DownloadFileById(int id)
        {
            try
            {
                var file = await _context.Documents.FirstOrDefaultAsync(file => file.DocumentId == id);

                if (file == null)
                {
                    throw new FileNotFoundException("file is not found");
                }

                var content = Encoding.UTF8.GetString(file.DocumentContent);

                return new FileGetDto
                {
                    FileContent = content,
                    FileId = file.DocumentId,
                    FileType = file.DocumentType
                };
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public async Task<string> PostFile(FileUploadDto fileItem, string userName)
        {
            try
            {
                var employees = await _employeeRepo.GetAll();

                var emp = employees.FirstOrDefault(ep => ep.Email == userName) ?? throw new Exception("No such user");



                var fileData = fileItem.FileDetails;
                var fileType = fileItem.FileType;
                var file = new Document
                {
                    DocumentName = fileData.FileName,
                    DocumentType = fileType,
                    UploadedById = emp.Id

                };

                using (var stream = new MemoryStream())
                {
                    fileData.CopyTo(stream);
                    file.DocumentContent = stream.ToArray();
                }



                var result = await _context.AddAsync(file);
                await _context.SaveChangesAsync();

                var message = "New Files have been uploaded";
                var uploadLoadedBy = emp.EmployeeName;

                await _hub.Clients.Group("StaffGroup").SendAsync("ReceiveNotification", message, uploadLoadedBy);

                return "File saved";
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}