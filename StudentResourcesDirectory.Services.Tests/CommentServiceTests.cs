using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using StudentResourcesDirectory.Data;
using StudentResourcesDirectory.Services.Core;
using StudentResourcesDirectory.Services.Tests.Helpers;
using StudentResourcesDirectory.ViewModels.CommentViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace StudentResourcesDirectory.Services.Tests;

[TestFixture]
public class CommentServiceTests
{
    private ApplicationDbContext _context;
    private CommentService _service;

    [SetUp]
    public async Task Setup()
    {
        _context = DbContextHelper.GetInMemoryDbContext();
        await DbContextHelper.SeedDataAsync(_context);
        _service = new CommentService(_context);
    }

    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }

    [Test]
    public async Task AddCommentAsync_ShouldAddComment()
    {
        var model = new CommentAddViewModel
        {
            Content = "New Comment",
            ResourceId = 1
        };

        await _service.AddCommentAsync(model, "user-1");

        var count = await _context.Comments.CountAsync();
        Assert.AreEqual(3, count);
    }

    [Test]
    public void AddCommentAsync_ShouldThrow_WhenUserIdNull()
    {
        var model = new CommentAddViewModel { Content = "x", ResourceId = 1 };
        Assert.ThrowsAsync<ArgumentNullException>(async () =>
            await _service.AddCommentAsync(model, null));
    }

    [Test]
    public async Task GetAddCommentModelAsync_ShouldReturnModel()
    {
        var model = await _service.GetAddCommentModelAsync(1);
        Assert.NotNull(model);
        Assert.AreEqual(1, model.ResourceId);
    }

    [Test]
    public async Task GetCommentsForResourceOrderedByDate_ShouldReturnCommentsInOrder()
    {
        var comments = (await _service.GetCommentsForResourceOrderedByDate(1)).ToList();

        Assert.AreEqual(2, comments.Count);
        Assert.LessOrEqual(comments[0].CreatedOn, comments[1].CreatedOn);
        Assert.AreEqual("Great resource!", comments[0].Content);
        Assert.AreEqual("Very helpful", comments[1].Content);
    }

    [Test]
    public async Task GetDeleteCommentModelAsync_ShouldReturnCorrectModel()
    {
        var comment = _context.Comments.First();

        var model = await _service.GetDeleteCommentModelAsync(comment.Id);

        Assert.NotNull(model);
        Assert.AreEqual(comment.Content, model.Content);
        Assert.AreEqual(comment.CreatedOn, model.CreatedOn);
    }

    [Test]
    public async Task DeleteCommentAsync_ShouldDeleteComment_WhenOwner()
    {
        int resourceId = await _service.DeleteCommentAsync(1, "user-1");
        Assert.AreEqual(1, resourceId);
        Assert.AreEqual(1, await _context.Comments.CountAsync());
    }

    [Test]
    public void DeleteCommentAsync_ShouldThrow_WhenNotOwner()
    {
        Assert.ThrowsAsync<ArgumentException>(async () =>
            await _service.DeleteCommentAsync(1, "user-2"));
    }

    [Test]
    public void DeleteCommentAsync_ShouldThrow_WhenCommentNotFound()
    {
        Assert.ThrowsAsync<ArgumentException>(async () =>
            await _service.DeleteCommentAsync(999, "user-1"));
    }
}