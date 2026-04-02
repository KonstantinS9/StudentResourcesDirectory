
using Microsoft.EntityFrameworkCore;
using StudentResourcesDirectory.Data;
using StudentResourcesDirectory.Data.Models;
using StudentResourcesDirectory.Services.Core.Contracts;
using StudentResourcesDirectory.ViewModels.CommentViewModels;
using static StudentResourcesDirectory.GCommon.ExceptionMessages.Comment;

namespace StudentResourcesDirectory.Services.Core
{
    public class CommentService : ICommentService
    {
        private ApplicationDbContext _dbContext;

        public CommentService(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task AddCommentAsync(CommentAddViewModel viewModel, string userId)
        {
            if (string.IsNullOrEmpty(userId))
                throw new ArgumentNullException((InvalidUserId));

            Comment comment = new Comment
            {
                UserId = userId,
                Content = viewModel.Content,
                ResourceId = viewModel.ResourceId,
                CreatedOn = DateTime.Now
            };

            await _dbContext.Comments.AddAsync(comment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteCommentAsync(int commentId, string userId)
        {
            var comment = await _dbContext.Comments
                .FirstOrDefaultAsync(c => c.Id == commentId);

            if (comment == null)
                throw new ArgumentException(CommentNotFound);
            if (comment.UserId != userId)
                throw new ArgumentException(NotOwnerOfComment);

            int resourceId = comment.ResourceId;

            _dbContext.Comments.Remove(comment);
            await _dbContext.SaveChangesAsync();

            return resourceId;
        }

        public async Task<CommentAddViewModel> GetAddCommentModelAsync(int resourceId)
        {
            var viewModel = new CommentAddViewModel()
            {
                ResourceId = resourceId            
            };

            return viewModel;
        }

        public async Task<IEnumerable<CommentIndexViewModel>> GetCommentsForResourceOrderedByDate(int resourceId)
        {
            var comments = await _dbContext
                .Comments
                .Where(c => c.ResourceId == resourceId)
                .Include(c => c.User)
                .Select(c => new CommentIndexViewModel
                {
                    Id = c.Id,
                    Content = c.Content,
                    CreatedOn = c.CreatedOn,
                    UserName = c.User.Email!.Split('@')[0]
                })
                .OrderBy(c => c.CreatedOn)
                .ToArrayAsync();

            return comments;
        }

        public async Task<CommentDeleteViewModel> GetDeleteCommentModelAsync(int id)
        {
            var comment = await _dbContext.Comments.Include(c => c.User).FirstOrDefaultAsync(c => c.Id == id);

            if (comment == null)
            {
                return null!;
            }

            var viewModel = new CommentDeleteViewModel()
            {
                Id = comment.Id,
                Content = comment!.Content,
                UserName = comment.User.Email!.Split('@')[0],
                CreatedOn = comment.CreatedOn
            };

            return viewModel;
        }
    }
}