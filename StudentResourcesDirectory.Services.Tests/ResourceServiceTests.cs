using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using StudentResourcesDirectory.Data;
using StudentResourcesDirectory.Data.Models;
using StudentResourcesDirectory.Data.Models.Enums;
using StudentResourcesDirectory.Services.Core;
using StudentResourcesDirectory.Services.Tests.Helpers;
using StudentResourcesDirectory.ViewModels.ResourceViewModels;

namespace StudentResourcesDirectory.Services.Tests
{
    [TestFixture]
    public class ResourceServiceTests
    {
        private ApplicationDbContext _context;
        private ResourceService _service;

        [SetUp]
        public async Task Setup()
        {
            _context = DbContextHelper.GetInMemoryDbContext();
            await DbContextHelper.SeedDataAsync(_context);
            _service = new ResourceService(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test]
        public async Task GetCreateResourceModelAsync_ShouldReturnCategories()
        {
            var result = await _service.GetCreateResourceModelAsync();

            Assert.NotNull(result);
            Assert.AreEqual(1, result.Categories.Count);
        }

        [Test]
        public async Task GetAllResources_ShouldReturnPagedData()
        {
            var (resources, totalPages) = await _service.GetAllResourcesOrderedByTitleThenByDateAscAsync();

            Assert.AreEqual(1, resources.Count());
            Assert.AreEqual(1, totalPages);
        }

        [Test]
        public async Task GetResourceDetailsAsync_ShouldReturnCorrectResource()
        {
            var result = await _service.GetResourceDetailsAsync(1);

            Assert.NotNull(result);
            Assert.AreEqual("C# Basics", result.Title);
        }

        [Test]
        public async Task CreateResourceAsync_ShouldAddResource()
        {
            var model = new CreateResourceViewModel
            {
                Title = "New Resource",
                Description = "Desc",
                Url = "http://new.com",
                CategoryId = 1,
                ResourceType = ResourceType.Book
            };

            await _service.CreateResourceAsync(model, "user-1");

            Assert.AreEqual(2, await _context.Resources.CountAsync());
        }

        [Test]
        public void CreateResourceAsync_ShouldThrow_WhenStudentNotFound()
        {
            var model = new CreateResourceViewModel();

            Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await _service.CreateResourceAsync(model, "invalid-user"));
        }

        [Test]
        public async Task EditResourceAsync_ShouldEditSuccessfully()
        {
            var model = new CreateResourceViewModel
            {
                Title = "Updated",
                Description = "Updated Desc",
                Url = "http://updated.com",
                CategoryId = 1,
                ResourceType = ResourceType.Book
            };

            await _service.EditResourceAsync(1, model, "user-1");

            var resource = await _context.Resources.FirstAsync();
            Assert.AreEqual("Updated", resource.Title);
        }

        [Test]
        public void EditResourceAsync_ShouldThrow_WhenNotOwner()
        {
            var model = new CreateResourceViewModel();

            Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
                await _service.EditResourceAsync(1, model, "wrong-user"));
        }

        [Test]
        public async Task DeleteResourceAsync_ShouldDeleteSuccessfully()
        {
            await _service.DeleteResourceAsync(1, "user-1");

            Assert.AreEqual(0, await _context.Resources.CountAsync());
        }

        [Test]
        public void DeleteResourceAsync_ShouldThrow_WhenNotOwner()
        {
            Assert.ThrowsAsync<UnauthorizedAccessException>(async () =>
                await _service.DeleteResourceAsync(1, "wrong-user"));
        }

        [Test]
        public async Task IsOwnerAsync_ShouldReturnTrue_WhenOwner()
        {
            var result = await _service.IsOwnerAsync(1, "user-1");
            Assert.IsTrue(result);
        }

        [Test]
        public async Task GetMyResourcesAsync_ShouldReturnOnlyUserResources()
        {
            var result = await _service.GetMyResourcesAsync("user-1");
            Assert.AreEqual(1, result.Count());
        }
    }
}