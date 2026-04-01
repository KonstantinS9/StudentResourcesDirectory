using Microsoft.EntityFrameworkCore;
using StudentResourcesDirectory.Data;
using StudentResourcesDirectory.Data.Models;
using StudentResourcesDirectory.Services.Core.Contracts;
using StudentResourcesDirectory.ViewModels.ResourceViewModels;

namespace StudentResourcesDirectory.Services.Core
{
    public class FavoriteService : IFavoriteService
    {
        private ApplicationDbContext _dbContext;

        public FavoriteService(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task AddResourceToFavoritesAsync(int resourceId, string userId)
        {
            var exists = await _dbContext.Favorites
                .AnyAsync(f => f.ResourceId == resourceId && f.UserId == userId);

            if (exists)
            {
                return;
            }

            var favorite = new Favorite
            {
                ResourceId = resourceId,
                UserId = userId
            };

            await _dbContext.Favorites.AddAsync(favorite);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<ResourceViewModel>> GetFavoriteResourcesAsync(string userId)
        {
            return await _dbContext.Favorites
                .Where(f => f.UserId == userId)
                .Include(f => f.Resource)
                .ThenInclude(r => r.Category) 
                .Include(f => f.Resource)
                .ThenInclude(r => r.Student)
                .ThenInclude(s => s.User) 
                .Select(f => new ResourceViewModel
                {
                    Id = f.Resource.Id,
                    Title = f.Resource.Title,
                    Description = f.Resource.Description,
                    Category = f.Resource.Category.Name,
                    Url = f.Resource.Url,
                    ResourceType = f.Resource.ResourceType,
                    Student = f.Resource!.Student!.User!.UserName!
                })
                .ToArrayAsync();
        }

        public async Task RemoveResourceFromFavorites(int resourceId, string userId)
        {
            var favorite = await _dbContext.Favorites
                .SingleOrDefaultAsync(f => f.ResourceId == resourceId && f.UserId == userId);

            if (favorite != null)
            {
                _dbContext.Favorites.Remove(favorite);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}