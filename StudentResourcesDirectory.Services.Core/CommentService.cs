
using Microsoft.EntityFrameworkCore;
using StudentResourcesDirectory.Data;
using StudentResourcesDirectory.Services.Core.Contracts;
using StudentResourcesDirectory.ViewModels.CommentViewModels;

namespace StudentResourcesDirectory.Services.Core
{
    public class CommentService : ICommentService
    {
        private ApplicationDbContext _dbContext;

        public CommentService(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<IEnumerable<CommentIndexViewModel>> GetCommentsForResourceOrderedByDate(int resourceId)
        {
            var comments = await _dbContext
                .Comments
                .Where(c => c.ResourceId == resourceId)
                .Select(c => new CommentIndexViewModel
                {
                    Content = c.Content,
                    CreatedOn = c.CreatedOn,
                    User = c.User
                })
                .OrderBy(c => c.CreatedOn)
                .ToArrayAsync();

            return comments;
        }
    }
}