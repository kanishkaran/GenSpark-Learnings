using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCareAPI.Contexts;
using HealthCareAPI.Interfaces;
using HealthCareAPI.Models;
using HealthCareAPI.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HealthCareAPI.Test
{
    public class PatientRepoTest
    {
        private HealthCareDbContext _context;
        IRepository<int, Patient> patientRepository;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<HealthCareDbContext>()
                    .UseInMemoryDatabase("TestPatientDB")
                    .Options;
            _context = new HealthCareDbContext(options);

            patientRepository = new PatientRepository(_context);
        }

        [Test]
        public async Task TestAddPatient()
        {
            var username = "TestUser@gmail.com";
            var password = Encoding.UTF8.GetBytes("testpassword");

            var user = new User
            {
                Username = username,
                Password = password,
                HashKey = Guid.NewGuid().ToByteArray(),
                Role = "Patient"
            };

            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            var testPatient = new Patient
            {
                PatientName = "TestPatient",
                PhoneNumber = "11111 22222",
                Age = 22,
                Email = username
            };

            testPatient = await patientRepository.Add(testPatient);

            Assert.That(testPatient, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(testPatient.Email, Is.EqualTo(username));
                Assert.That(testPatient.Id, Is.EqualTo(1));
            });


        }

        [TestCase(1)]
        //[TestCase(2)]
        public async Task TestGetPatientById(int id)
        {
            var result = await patientRepository.GetById(id);

            Assert.That(result, Is.Not.Null, $"No Patient with id: {id}");
        }

        [TestCase(2)]
        public async Task TestGetPatientByIdException(int id)
        {
            await Assert.ThatAsync(() => patientRepository.GetById(id), Throws.TypeOf<KeyNotFoundException>());
        }


        [TearDown]
        public void DisposeContext()
        {
            _context.Dispose();
        }
    }
}