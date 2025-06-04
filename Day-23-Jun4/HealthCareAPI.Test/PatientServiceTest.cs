using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HealthCareAPI.Contexts;
using HealthCareAPI.Interfaces;
using HealthCareAPI.Models;
using HealthCareAPI.Models.DTOs;
using HealthCareAPI.Repositories;
using HealthCareAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace HealthCareAPI.Test
{
    public class PatientServiceTest
    {
        private HealthCareDbContext _context;
        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<HealthCareDbContext>()
                                .UseInMemoryDatabase("TestDB")
                                .Options;

            _context = new(options);
        }
        [Test]
        public async Task RegisterPatient()
        {
            // Arrange
            var patientRepo = new Mock<PatientRepository>(_context);
            var userRepoMock = new Mock<UserRepository>(_context);
            var loggerMock = new Mock<ILogger<PatientService>>();
            var encryptionServiceMock = new Mock<EncryptionService>();
            var mapperMock = new Mock<IMapper>();

            var patientDto = new PatientAddRequestDto
            {
                Age = 25,
                PhoneNumber = "1234567890",
                PatientName = "test",
                Password = "password123"
            };

            var username = "TestUser@gmail.com";
            var password = Encoding.UTF8.GetBytes("testpassword");

            var user = new User
            {
                Username = username,
                Password = password,
                HashKey = Guid.NewGuid().ToByteArray(),
                Role = "Patient"
            };

            var patient = new Patient { Id = 1, PatientName = "Test" };
            var encryptedModel = new EncryptModel();


            mapperMock.Setup(m => m.Map<PatientAddRequestDto, User>(It.IsAny<PatientAddRequestDto>())).Returns(user);
            mapperMock.Setup(m => m.Map<PatientAddRequestDto, Patient>(It.IsAny<PatientAddRequestDto>())).Returns(patient);
            encryptionServiceMock.Setup(en => en.EncryptData(It.IsAny<EncryptModel>())).ReturnsAsync(encryptedModel);
            userRepoMock.Setup(us => us.Add(It.IsAny<User>())).ReturnsAsync(user);
            patientRepo.Setup(ad => ad.Add(It.IsAny<Patient>())).ReturnsAsync(patient);


            var service = new PatientService(
                patientRepo.Object,
                userRepoMock.Object,
                loggerMock.Object,
                encryptionServiceMock.Object,
                mapperMock.Object
            );

            // Act
            var result = await service.RegisterPatient(patientDto);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.PatientName, Is.EqualTo("Test"));

        }


        [TearDown]
        public void DisposeContext()
        {
            _context.Dispose();
        }
    }
}