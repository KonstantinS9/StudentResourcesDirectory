
using StudentResourcesDirectory.ViewModels.RatingViewModels;

namespace StudentResourcesDirectory.Services.Core.Contracts
{
    public interface IRatingService
    {
        Task<IEnumerable<RatingIndexViewModel>> GetAllRatingsForResourceOrderedByDateAsync(int resourceId);
        Task<RatingAddViewModel> GetAddRatingModelAsync(int resourceId);
        Task<int> AddRatingAsync(RatingAddViewModel viewModel, int id, string userId);
        Task<RatingDeleteViewModel> GetDeleteRatingModelAsync(int id);
        Task<int> DeleteRatingAsync(int ratingId, string userId);
    }
}