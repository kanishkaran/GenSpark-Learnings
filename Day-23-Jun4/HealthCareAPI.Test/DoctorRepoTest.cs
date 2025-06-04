using System.Text;
using HealthCareAPI.Contexts;
using HealthCareAPI.Interfaces;
using HealthCareAPI.Models;
using HealthCareAPI.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HealthCareAPI.Test;

public class Tests
{
    private HealthCareDbContext _context;
    IRepository<int, Doctor> _doctorRepository;


    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<HealthCareDbContext>()
                            .UseInMemoryDatabase("TestDoctorDB")
                            .Options;
        _context = new HealthCareDbContext(options);

        _doctorRepository = new DoctorRepository(_context);
    }

    [Test]
    public async Task TestAddDoctor()
    {
        // 3A's - Arrange, Action, Assert
        var username = "TestUser@gmail.com";
        var password = Encoding.UTF8.GetBytes("testpassword");

        var user = new User
        {
            Username = username,
            Password = password,
            HashKey = Guid.NewGuid().ToByteArray(),
            Role = "Doctor"
        };

        await _context.AddAsync(user);
        await _context.SaveChangesAsync();

        var testDoctor = new Doctor
        {
            DoctorName = "Test",
            Email = username,
            Status = "Active"
        };

        testDoctor = await _doctorRepository.Add(testDoctor);

        Assert.That(testDoctor, Is.Not.Null, "Doctor is not added");
        Assert.That(testDoctor.Email, Is.EqualTo(username));
        Assert.That(testDoctor.Id, Is.EqualTo(1));

    }

    [TestCase(1)]
    public async Task TestGetById(int id)
    {
        var doctor = await _doctorRepository.GetById(id);

        Assert.That(doctor, Is.Not.Null, "No such doctor");
    }


    [TestCase(2)]
    public async Task TestDoctorRepoException(int id)
    {

        // Assert.That(() => _doctorRepository.GetById(id), Throws.TypeOf<KeyNotFoundException>());
        var ex = Assert.ThrowsAsync<KeyNotFoundException>(() => _doctorRepository.GetById(id));
        Assert.That(ex.Message, Is.EqualTo("Doctor not found"));

    }

    [Test]
    public async Task TestGetAllDoctors()
    {
        var result = await _doctorRepository.GetAll();
        Assert.That(result, Is.Not.Null);
    }


    // [Test]
    // public async Task TestDeleteDoctor()
    // {
    //     var doctor = await _doctorRepository.GetById(1);

    //     Assert.That(doctor.Id, Is.EqualTo(1));
    // }


    [Test]
    public async Task TestGetAllDoctorsException()
    {
        await _doctorRepository.Delete(1);
        await Assert.ThatAsync(() => _doctorRepository.GetAll(), Throws.TypeOf<Exception>());
    }


    [TearDown]
    public void DisposeContext()
    {
        _context.Dispose();
    }
}
