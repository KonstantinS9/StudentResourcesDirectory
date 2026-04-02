
using Microsoft.EntityFrameworkCore;
using StudentResourcesDirectory.Data;
using StudentResourcesDirectory.Data.Models;
using StudentResourcesDirectory.Services.Core.Contracts;
using StudentResourcesDirectory.ViewModels.RatingViewModels;
using static StudentResourcesDirectory.GCommon.ExceptionMessages.Rating;

namespace StudentResourcesDirectory.Services.Core
{
    public class RatingService : IRatingService
    {
        private ApplicationDbContext _dbContext;

        public RatingService(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<int> AddRatingAsync(RatingAddViewModel viewModel, int id, string userId)
        {
            if (string.IsNullOrEmpty(userId))
                throw new ArgumentNullException(InvalidUserId);

            if (await _dbContext.Ratings.AnyAsync(r => r.UserId == userId && r.ResourceId == id))
                throw new ArgumentException(YouGaveRatingAlready);

            Rating rating = new Rating()
            {
                CreatedOn = DateTime.Now,
                UserId = userId,
                ResourceId = viewModel.ResourceId,
                RatingResource = viewModel.RatingResource
            };

            var resourceId = viewModel.ResourceId;
            await _dbContext.Ratings.AddAsync(rating);
            await _dbContext.SaveChangesAsync();

            return resourceId;
        }

        public async Task<int> DeleteRatingAsync(int ratingId, string userId)
        {
            var rating = await _dbContext.Ratings
                .FirstOrDefaultAsync(r => r.Id == ratingId && r.UserId == userId);

            if (rating == null)
                throw new ArgumentException(RatingNotFound);

            int resourceId = rating.ResourceId;

            _dbContext.Ratings.Remove(rating);

            await _dbContext.SaveChangesAsync();

            return resourceId;
        }

        public async Task<RatingAddViewModel> GetAddRatingModelAsync(int resourceId)
        {
            var viewModel = new RatingAddViewModel()
            {
                ResourceId = resourceId,
            };

            return viewModel;
        }

        public async Task<IEnumerable<RatingIndexViewModel>> GetAllRatingsForResourceOrderedByDateAsync(int resourceId)
        {
            var ratings = await _dbContext
                .Ratings
                .Where(r => r.ResourceId == resourceId)
                .Include(r => r.User)
                .Select(r => new RatingIndexViewModel
                {
                    Id = r.Id,
                    UserName = r.User!.Email!.Split('@')[0],
                    RatingResource = r.RatingResource,
                    CreatedOn = r.CreatedOn
                })
                .OrderBy(r => r.CreatedOn)
                .ToListAsync();

            return ratings;
        }

        public async Task<RatingDeleteViewModel> GetDeleteRatingModelAsync(int id)
        {
            var rating = await _dbContext.Ratings
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (rating == null)
            {
                return null!;
            }

            var viewModel = new RatingDeleteViewModel()
            {
                Id = rating.Id,
                CreatedOn = rating.CreatedOn,
                RatingResource = rating.RatingResource,
                UserName = rating.User.Email!.Split('@')[0]
            };

            return viewModel;
        }
    }
}