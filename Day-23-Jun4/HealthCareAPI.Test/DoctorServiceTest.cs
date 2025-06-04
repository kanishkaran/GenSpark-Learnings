using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HealthCareAPI.Contexts;
using HealthCareAPI.Interfaces;
using HealthCareAPI.Misc;
using HealthCareAPI.Models.DTOs;
using HealthCareAPI.Repositories;
using HealthCareAPI.Services;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace HealthCareAPI.Test
{
    public class DoctorServiceTest
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


        [TestCase("ENT")]
        public async Task TestDoctorsBySpeciality(string speciality)
        {
            Mock<DoctorRepository> doctorRepoMock = new Mock<DoctorRepository>(_context);
            Mock<SpecializationRepository> specializationRepoMock = new Mock<SpecializationRepository>(_context);
            Mock<DoctorSpecializationRepository> doctorSpecializationRepoMock = new Mock<DoctorSpecializationRepository>(_context);
            Mock<OtherFunctionalities> otherFunctionalitiesRepoMock = new Mock<OtherFunctionalities>(_context);
            Mock<UserRepository> userRepoMock = new Mock<UserRepository>(_context);
            Mock<EncryptionService> encryptionServiceMock = new Mock<EncryptionService>();
            Mock<IMapper> mapperMock = new Mock<IMapper>();

            DoctorService doctorService = new DoctorService(doctorRepoMock.Object,
                                                        specializationRepoMock.Object,
                                                        doctorSpecializationRepoMock.Object,
                                                        otherFunctionalitiesRepoMock.Object,
                                                        userRepoMock.Object,
                                                        encryptionServiceMock.Object,
                                                        mapperMock.Object);

            Assert.That(doctorService, Is.Not.Null);

            otherFunctionalitiesRepoMock.Setup(of => of.GetDoctorsBySpeciality(It.IsAny<string>()))
                                        .ReturnsAsync((string speciality) => new List<DoctorBySpecialityDto>
                                        {
                                            new DoctorBySpecialityDto{
                                                Id = 1,
                                                Name = "Test"
                                            }
                                        });

            var result = await doctorService.GetDoctorsBySpeciality(speciality);

            Assert.That(result, Has.Count.EqualTo(1));
        }

        [TearDown]
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}