using Microsoft.EntityFrameworkCore;
using StudentResourcesDirectory.Data;
using StudentResourcesDirectory.Data.Models.Enums;
using StudentResourcesDirectory.Services.Core;
using StudentResourcesDirectory.Services.Tests.Helpers;
using StudentResourcesDirectory.ViewModels.RatingViewModels;

namespace StudentResourcesDirectory.Services.Tests;

[TestFixture]
public class RatingServiceTests
{
    private ApplicationDbContext _context;
    private RatingService _service;

    [SetUp]
    public async Task Setup()
    {
        _context = DbContextHelper.GetInMemoryDbContext();
        await DbContextHelper.SeedDataAsync(_context);
        _service = new RatingService(_context);
    }

    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }

    [Test]
    public async Task AddRatingAsync_ShouldAddRating()
    {
        var model = new RatingAddViewModel
        {
            RatingResource = RatingResource.Excellent,
            ResourceId = 1
        };

        var resourceId = await _service.AddRatingAsync(model, 1, "user-1");

        Assert.AreEqual(1, resourceId);
        Assert.AreEqual(1, await _context.Ratings.CountAsync(r => r.ResourceId == 1 && r.UserId == "user-1"));
    }

    [Test]
    public void AddRatingAsync_ShouldThrow_WhenUserIdNull()
    {
        var model = new RatingAddViewModel { RatingResource = RatingResource.Excellent, ResourceId = 1 };

        Assert.ThrowsAsync<ArgumentNullException>(async () =>
            await _service.AddRatingAsync(model, 1, null));
    }

    [Test]
    public async Task AddRatingAsync_ShouldThrow_WhenRatingAlreadyExists()
    {
        var model = new RatingAddViewModel { RatingResource = RatingResource.Good, ResourceId = 1 };

        await _service.AddRatingAsync(model, 1, "user-1");

        Assert.ThrowsAsync<ArgumentException>(async () =>
            await _service.AddRatingAsync(model, 1, "user-1"));
    }

    [Test]
    public async Task DeleteRatingAsync_ShouldDeleteRating()
    {
        var model = new RatingAddViewModel { RatingResource = RatingResource.Excellent, ResourceId = 1 };
        await _service.AddRatingAsync(model, 1, "user-1");
        var rating = await _context.Ratings.FirstAsync();

        var resourceId = await _service.DeleteRatingAsync(rating.Id, "user-1");

        Assert.AreEqual(1, resourceId);
        Assert.AreEqual(0, await _context.Ratings.CountAsync());
    }

    [Test]
    public void DeleteRatingAsync_ShouldThrow_WhenRatingNotFound()
    {
        Assert.ThrowsAsync<ArgumentException>(async () =>
            await _service.DeleteRatingAsync(999, "user-1"));
    }

    [Test]
    public async Task GetAddRatingModelAsync_ShouldReturnCorrectModel()
    {
        var model = await _service.GetAddRatingModelAsync(1);
        Assert.NotNull(model);
        Assert.AreEqual(1, model.ResourceId);
    }

    [Test]
    public async Task GetAllRatingsForResourceOrderedByDateAsync_ShouldReturnRatingsInOrder()
    {
        await _service.AddRatingAsync(new RatingAddViewModel { RatingResource = RatingResource.Good, ResourceId = 1 }, 1, "user-1");
        await Task.Delay(50);
        await _service.AddRatingAsync(new RatingAddViewModel { RatingResource = RatingResource.Excellent, ResourceId = 1 }, 1, "user-2");

        var ratings = (await _service.GetAllRatingsForResourceOrderedByDateAsync(1)).ToList();

        Assert.AreEqual(2, ratings.Count);
        Assert.LessOrEqual(ratings[0].CreatedOn, ratings[1].CreatedOn);
        Assert.AreEqual(RatingResource.Good, ratings[0].RatingResource);
        Assert.AreEqual(RatingResource.Excellent, ratings[1].RatingResource);
    }

    [Test]
    public async Task GetDeleteRatingModelAsync_ShouldReturnCorrectModel()
    {
        await _service.AddRatingAsync(new RatingAddViewModel { RatingResource = RatingResource.Good, ResourceId = 1 }, 1, "user-1");
        var rating = await _context.Ratings.FirstAsync();

        var model = await _service.GetDeleteRatingModelAsync(rating.Id);

        Assert.NotNull(model);
        Assert.AreEqual(rating.Id, model.Id);
        Assert.AreEqual(rating.RatingResource, model.RatingResource);
        Assert.AreEqual(rating.CreatedOn, model.CreatedOn);
    }
}