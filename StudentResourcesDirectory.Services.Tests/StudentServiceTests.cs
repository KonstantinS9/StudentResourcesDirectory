using Microsoft.EntityFrameworkCore;
using StudentResourcesDirectory.Data;
using StudentResourcesDirectory.Services.Core;
using StudentResourcesDirectory.Services.Tests.Helpers;

namespace StudentResourcesDirectory.Services.Tests;

[TestFixture]
public class StudentServiceTests
{
    private ApplicationDbContext _context;
    private StudentService _service;

    [SetUp]
    public async Task Setup()
    {
        _context = DbContextHelper.GetInMemoryDbContext();
        await DbContextHelper.SeedDataAsync(_context);
        _service = new StudentService(_context);
    }

    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }

    [Test]
    public async Task GetAllStudentsOrderedByFirstNameAscAsync_ShouldReturnAllStudents()
    {
        var result = await _service.GetAllStudentsOrderedByFirstNameAscAsync();

        Assert.NotNull(result);
        Assert.AreEqual(2, result.Count());
        Assert.AreEqual("Ivan", result.First().FirstName);
    }

    [Test]
    public async Task GetAllStudentsOrderedByFirstNameAscAsync_ShouldFilterBySearchQuery()
    {
        var result = await _service.GetAllStudentsOrderedByFirstNameAscAsync("Maria");

        Assert.AreEqual(1, result.Count());
        Assert.AreEqual("Maria", result.First().FirstName);
    }

    [Test]
    public async Task GetStudentDetailsAsync_ShouldReturnCorrectStudent()
    {
        var student = await _context.Students.FirstAsync();
        var result = await _service.GetStudentDetailsAsync(student.Id);

        Assert.NotNull(result);
        Assert.AreEqual(student.FirstName, result.FirstName);
        Assert.AreEqual(student.LastName, result.LastName);
        Assert.AreEqual(student.Email, result.Email);
        Assert.AreEqual(student.FacultyNumber, result.FacultyNumber);
        Assert.AreEqual(1, result.Resources.Count());
    }

    [Test]
    public async Task GetStudentResourcesAsync_ShouldReturnResourcesOfStudent()
    {
        var student = await _context.Students.FirstAsync();
        var result = await _service.GetStudentResourcesAsync(student.Id);

        Assert.AreEqual(1, result.Count());
        Assert.AreEqual("C# Basics", result.First().Title);
    }

    [Test]
    public async Task GetStudentResourcesAsync_ShouldReturnEmptyListIfNoResources()
    {
        var student = await _context.Students.FirstOrDefaultAsync(s => s.FirstName == "Maria");
        var result = await _service.GetStudentResourcesAsync(student.Id);

        Assert.IsEmpty(result);
    }
}