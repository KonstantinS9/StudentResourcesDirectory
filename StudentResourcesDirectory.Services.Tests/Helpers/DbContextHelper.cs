using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudentResourcesDirectory.Data;
using StudentResourcesDirectory.Data.Models;
using StudentResourcesDirectory.Data.Models.Enums;
using System;
using System.Threading.Tasks;

namespace StudentResourcesDirectory.Services.Tests.Helpers
{
    public static class DbContextHelper
    {
        public static ApplicationDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new ApplicationDbContext(options);
            return context;
        }

        public static async Task SeedDataAsync(ApplicationDbContext context)
        {
            var category = new Category { Name = "Programming" };
            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();

            var student1 = new Student
            {
                FirstName = "Ivan",
                LastName = "Ivanov",
                Email = "ivan@example.com",
                FacultyNumber = "12345",
                UserId = "user-1",
                RegisteredOn = DateTime.UtcNow
            };

            var student2 = new Student
            {
                FirstName = "Maria",
                LastName = "Petrova",
                Email = "maria@example.com",
                FacultyNumber = "67890",
                UserId = "user-2",
                RegisteredOn = DateTime.UtcNow
            };

            await context.Students.AddRangeAsync(student1, student2);
            await context.SaveChangesAsync();

            var user1 = new IdentityUser
            {
                Id = student1.UserId,
                Email = student1.Email,
                UserName = student1.Email
            };

            var user2 = new IdentityUser
            {
                Id = student2.UserId,
                Email = student2.Email,
                UserName = student2.Email
            };

            await context.Users.AddRangeAsync(user1, user2);
            await context.SaveChangesAsync();

            var resource = new Resource
            {
                Title = "C# Basics",
                Description = "Intro to C#",
                Url = "http://test.com",
                CategoryId = category.Id,
                StudentId = student1.Id,
                ResourceType = ResourceType.Video,
                CreatedOn = DateTime.UtcNow
            };
            await context.Resources.AddAsync(resource);
            await context.SaveChangesAsync();

            var comment1 = new Comment
            {
                Content = "Great resource!",
                UserId = user1.Id,
                ResourceId = resource.Id,
                CreatedOn = DateTime.UtcNow,
                User = user1
            };

            var comment2 = new Comment
            {
                Content = "Very helpful",
                UserId = user2.Id,
                ResourceId = resource.Id,
                CreatedOn = DateTime.UtcNow.AddMinutes(1),
                User = user2
            };

            await context.Comments.AddRangeAsync(comment1, comment2);
            await context.SaveChangesAsync();
        }
    }
}