
using StudentResourcesDirectory.ViewModels.CommentViewModels;

namespace StudentResourcesDirectory.Services.Core.Contracts
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentIndexViewModel>> GetCommentsForResourceOrderedByDate(int resourceId);
        Task<CommentAddViewModel> GetAddCommentModelAsync(int resourceId);
        Task AddCommentAsync(CommentAddViewModel viewModel, string userId);
        Task<CommentDeleteViewModel> GetDeleteCommentModelAsync(int id);
        Task<int> DeleteCommentAsync(int id, string userId);
    }
}