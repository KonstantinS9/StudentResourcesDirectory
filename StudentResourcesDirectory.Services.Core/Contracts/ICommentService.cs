
using StudentResourcesDirectory.ViewModels.CommentViewModels;

namespace StudentResourcesDirectory.Services.Core.Contracts
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentIndexViewModel>> GetCommentsForResourceOrderedByDate(int resourceId);
    }
}