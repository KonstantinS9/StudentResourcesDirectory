using Microsoft.EntityFrameworkCore;
using NUnit.Framework.Legacy;
using StudentResourcesDirectory.Data;
using StudentResourcesDirectory.Data.Models.Enums;
using StudentResourcesDirectory.Services.Core;
using StudentResourcesDirectory.Services.Tests.Helpers;
using StudentResourcesDirectory.ViewModels.AdminViewModels.ResourceManagementViewModels;

namespace StudentResourcesDirectory.Services.Tests;

[TestFixture]
public class ResourceManagementServiceTests
{
    private ApplicationDbContext _context;
    private ResourceManagementService _service;

    [SetUp]
    public async Task Setup()
    {
        _context = DbContextHelper.GetInMemoryDbContext();
        await DbContextHelper.SeedDataAsync(_context);
        _service = new ResourceManagementService(_context);
    }

    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }

    [Test]
    public async Task CreateResourceAsync_ShouldCreateResource()
    {
        var category = _context.Categories.First();

        var model = new ResourceManagementViewModel
        {
            Title = "New Resource",
            Description = "Description",
            Url = "http://example.com",
            ResourceType = ResourceType.Video,
            CategoryId = category.Id
        };

        await _service.CreateResourceAsync(model);

        var resource = await _context.Resources.FirstOrDefaultAsync(r => r.Title == "New Resource");
        Assert.NotNull(resource);
        Assert.AreEqual("Description", resource.Description);
        Assert.AreEqual(ResourceType.Video, resource.ResourceType);
        Assert.AreEqual(category.Id, resource.CategoryId);
    }

    [Test]
    public void CreateResourceAsync_ShouldThrow_WhenInvalidCategory()
    {
        var model = new ResourceManagementViewModel
        {
            Title = "Invalid Resource",
            Description = "Desc",
            Url = "http://invalid.com",
            ResourceType = ResourceType.Video,
            CategoryId = 999
        };

        Assert.ThrowsAsync<Exception>(async () => await _service.CreateResourceAsync(model));
    }

    [Test]
    public async Task DeleteResourceAsync_ShouldDeleteResource()
    {
        var resource = _context.Resources.First();

        await _service.DeleteResourceAsync(resource.Id);

        var exists = await _context.Resources.AnyAsync(r => r.Id == resource.Id);
        Assert.False(exists);
    }

    [Test]
    public void DeleteResourceAsync_ShouldThrow_WhenResourceNotFound()
    {
        Assert.ThrowsAsync<ArgumentException>(async () => await _service.DeleteResourceAsync(999));
    }

    [Test]
    public async Task EditResourceAsync_ShouldEditResource()
    {
        var resource = _context.Resources.First();
        var category = _context.Categories.First();

        var model = new ResourceManagementViewModel
        {
            Id = resource.Id,
            Title = "Edited Title",
            Description = "Edited Desc",
            Url = "http://edited.com",
            ResourceType = ResourceType.Book,
            CategoryId = category.Id
        };

        await _service.EditResourceAsync(model);

        var updated = await _context.Resources.FirstOrDefaultAsync(r => r.Id == resource.Id);
        Assert.NotNull(updated);
        Assert.AreEqual("Edited Title", updated.Title);
        Assert.AreEqual(ResourceType.Book, updated.ResourceType);
    }

    [Test]
    public void EditResourceAsync_ShouldThrow_WhenResourceNotFound()
    {
        var model = new ResourceManagementViewModel
        {
            Id = 999,
            Title = "X",
            Description = "Y",
            Url = "Z",
            ResourceType = ResourceType.Video,
            CategoryId = 1
        };

        Assert.ThrowsAsync<ArgumentException>(async () => await _service.EditResourceAsync(model));
    }

    [Test]
    public async Task GetAllResourcesOrderedByTitleThenByDateAsync_ShouldReturnResourcesOrdered()
    {
        var resources = (await _service.GetAllResourcesOrderedByTitleThenByDateAsync()).ToList();

        Assert.IsTrue(resources.Count > 0);
        var sorted = resources.OrderBy(r => r.Title).ThenBy(r => r.CreatedOn).ToList();
        CollectionAssert.AreEqual(sorted.Select(r => r.Id), resources.Select(r => r.Id));
    }

    [Test]
    public async Task GetCreateResourceModelAsync_ShouldReturnCategories()
    {
        var model = await _service.GetCreateResourceModelAsync();

        Assert.NotNull(model);
        Assert.IsTrue(model.Categories.Count() > 0);
        Assert.IsTrue(model.Categories.All(c => !string.IsNullOrEmpty(c.Text)));
    }

    [Test]
    public async Task GetDeleteModelAsync_ShouldReturnModelWithId()
    {
        var model = await _service.GetDeleteModelAsync(1);
        Assert.NotNull(model);
        Assert.AreEqual(1, model.Id);
    }

    [Test]
    public async Task GetEditModelAsync_ShouldReturnResourceModel()
    {
        var resource = _context.Resources.First();
        var model = await _service.GetEditModelAsync(resource.Id);

        Assert.NotNull(model);
        Assert.AreEqual(resource.Id, model.Id);
        Assert.AreEqual(resource.Title, model.Title);
        Assert.IsTrue(model.Categories.Count() > 0);
    }

    [Test]
    public async Task GetEditModelAsync_ShouldReturnNull_WhenResourceNotFound()
    {
        var model = await _service.GetEditModelAsync(999);
        Assert.Null(model);
    }
}
