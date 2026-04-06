using Microsoft.EntityFrameworkCore;
using StudentResourcesDirectory.Data;
using StudentResourcesDirectory.Services.Core;
using StudentResourcesDirectory.Services.Tests.Helpers;

namespace StudentResourcesDirectory.Services.Tests;

[TestFixture]
public class FavoriteServiceTests
{
    private ApplicationDbContext _context;
    private FavoriteService _service;

    [SetUp]
    public async Task Setup()
    {
        _context = DbContextHelper.GetInMemoryDbContext();
        await DbContextHelper.SeedDataAsync(_context); 
        _service = new FavoriteService(_context);
    }

    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }

    [Test]
    public async Task AddResourceToFavoritesAsync_ShouldAddFavorite()
    {
        await _service.AddResourceToFavoritesAsync(1, "user-1");

        var favorite = await _context.Favorites.FirstOrDefaultAsync(f => f.ResourceId == 1 && f.UserId == "user-1");
        Assert.NotNull(favorite);
        Assert.AreEqual(1, favorite.ResourceId);
        Assert.AreEqual("user-1", favorite.UserId);
    }

    [Test]
    public async Task AddResourceToFavoritesAsync_ShouldNotDuplicateFavorite()
    {
        await _service.AddResourceToFavoritesAsync(1, "user-1");
        await _service.AddResourceToFavoritesAsync(1, "user-1");

        var count = await _context.Favorites.CountAsync(f => f.ResourceId == 1 && f.UserId == "user-1");
        Assert.AreEqual(1, count);
    }

    [Test]
    public async Task GetFavoriteResourcesAsync_ShouldReturnFavorites()
    {
        await _service.AddResourceToFavoritesAsync(1, "user-1");

        var favorites = (await _service.GetFavoriteResourcesAsync("user-1")).ToList();

        Assert.AreEqual(1, favorites.Count);
        Assert.AreEqual(1, favorites[0].Id);
        Assert.AreEqual("C# Basics", favorites[0].Title);
    }

    [Test]
    public async Task GetFavoriteResourcesAsync_ShouldFilterBySearchQuery()
    {
        await _service.AddResourceToFavoritesAsync(1, "user-1");

        var favorites = (await _service.GetFavoriteResourcesAsync("user-1", searchQuery: "c#")).ToList();

        Assert.AreEqual(1, favorites.Count);

        favorites = (await _service.GetFavoriteResourcesAsync("user-1", searchQuery: "java")).ToList();
        Assert.AreEqual(0, favorites.Count);
    }

    [Test]
    public async Task GetFavoriteResourcesAsync_ShouldFilterByResourceType()
    {
        await _service.AddResourceToFavoritesAsync(1, "user-1");

        var favorites = (await _service.GetFavoriteResourcesAsync("user-1", resourceType: "video")).ToList();
        Assert.AreEqual(1, favorites.Count);

        favorites = (await _service.GetFavoriteResourcesAsync("user-1", resourceType: "book")).ToList();
        Assert.AreEqual(0, favorites.Count);
    }

    [Test]
    public async Task GetFavoriteResourcesAsync_ShouldFilterByCategory()
    {
        await _service.AddResourceToFavoritesAsync(1, "user-1");

        var favorites = (await _service.GetFavoriteResourcesAsync("user-1", category: "Programming")).ToList();
        Assert.AreEqual(1, favorites.Count);

        favorites = (await _service.GetFavoriteResourcesAsync("user-1", category: "Math")).ToList();
        Assert.AreEqual(0, favorites.Count);
    }

    [Test]
    public async Task RemoveResourceFromFavorites_ShouldRemoveFavorite()
    {
        await _service.AddResourceToFavoritesAsync(1, "user-1");

        await _service.RemoveResourceFromFavorites(1, "user-1");

        var favorite = await _context.Favorites.FirstOrDefaultAsync(f => f.ResourceId == 1 && f.UserId == "user-1");
        Assert.Null(favorite);
    }

    [Test]
    public async Task RemoveResourceFromFavorites_ShouldDoNothingIfNotExists()
    {
        await _service.RemoveResourceFromFavorites(999, "user-1");

        var count = await _context.Favorites.CountAsync();
        Assert.AreEqual(0, count);
    }
}
