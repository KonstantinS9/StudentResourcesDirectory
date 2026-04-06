using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentResourcesDirectory.Data;
using StudentResourcesDirectory.Services.Core;
using StudentResourcesDirectory.Services.Tests.Helpers;

namespace StudentResourcesDirectory.Services.Tests;

[TestFixture]
public class StudentManagementServiceTests
{
    private ApplicationDbContext _context;
    private UserManager<IdentityUser> _userManager;
    private RoleManager<IdentityRole> _roleManager;
    private StudentManagementService _service;

    [SetUp]
    public async Task Setup()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("StudentMgmtDb_" + System.Guid.NewGuid())
            .Options;

        _context = new ApplicationDbContext(options);
        await DbContextHelper.SeedDataAsync(_context);

        var userStore = new UserStore<IdentityUser>(_context);
        _userManager = new UserManager<IdentityUser>(
            userStore, null, new PasswordHasher<IdentityUser>(),
            null, null, null, null, null, null);

        var roleStore = new RoleStore<IdentityRole>(_context);
        _roleManager = new RoleManager<IdentityRole>(
            roleStore, null, null, null, null);

        if (!await _roleManager.RoleExistsAsync("Admin"))
            await _roleManager.CreateAsync(new IdentityRole("Admin"));

        if (!await _roleManager.RoleExistsAsync("Student"))
            await _roleManager.CreateAsync(new IdentityRole("Student"));

        foreach (var student in _context.Students)
        {
            var userExists = await _userManager.FindByIdAsync(student.UserId);
            if (userExists == null)
            {
                await _userManager.CreateAsync(new IdentityUser
                {
                    Id = student.UserId,
                    UserName = student.Email,
                    Email = student.Email
                });
            }
        }

        _service = new StudentManagementService(_context, _userManager, _roleManager);
    }

    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
        _userManager.Dispose();
        _roleManager.Dispose();
    }

    [Test]
    public async Task AssignRoleAsync_ShouldAssignRoleSuccessfully()
    {
        var student = _context.Students.First();
        var result = await _service.AssignRoleAsync(student.UserId, "Student");

        Assert.True(result);

        var userRoles = await _userManager.GetRolesAsync(await _userManager.FindByIdAsync(student.UserId));
        Assert.Contains("Student", userRoles.ToList());
    }

    [Test]
    public async Task AssignRoleAsync_ShouldReturnFalse_WhenRoleDoesNotExist()
    {
        var student = _context.Students.First();
        var result = await _service.AssignRoleAsync(student.UserId, "NonExistentRole");

        Assert.False(result);
    }

    [Test]
    public async Task GetDeleteModelAsync_ShouldReturnStudentModel()
    {
        var student = _context.Students.First();
        var model = await _service.GetDeleteModelAsync(student.UserId);

        Assert.NotNull(model);
        Assert.AreEqual(student.UserId, model.Id);
        Assert.AreEqual(student.Email, model.Email);
    }

    [Test]
    public async Task GetDeleteModelAsync_ShouldReturnNull_WhenUserNotFound()
    {
        var model = await _service.GetDeleteModelAsync("non-existent-id");
        Assert.Null(model);
    }

    [Test]
    public async Task GetAllStudentsOrderedByEmailAsync_ShouldReturnAllExceptAdmin()
    {
        var admin = new IdentityUser { Id = "admin", Email = "admin@example.com" };
        await _userManager.CreateAsync(admin);

        var students = await _service.GetAllStudentsOrderedByEmailAsync("admin");

        Assert.IsTrue(students.All(s => s.Id != "admin"));
        Assert.IsTrue(students.Count() == _context.Students.Count());
    }

    [Test]
    public async Task DeleteStudentAsync_ShouldDeleteStudentAndUser()
    {
        var student = _context.Students.First();
        await _service.DeleteStudentAsync(student.UserId);

        var studentExists = await _context.Students.AnyAsync(s => s.UserId == student.UserId);
        var userExists = await _context.Users.AnyAsync(u => u.Id == student.UserId);

        Assert.False(studentExists);
        Assert.False(userExists);
    }
}
