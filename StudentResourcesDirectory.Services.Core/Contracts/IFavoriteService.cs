
using StudentResourcesDirectory.ViewModels.ResourceViewModels;

namespace StudentResourcesDirectory.Services.Core.Contracts
{
    public interface IFavoriteService
    {
        Task<IEnumerable<ResourceViewModel>> GetFavoriteResourcesAsync(
            string userId, 
            string? searchQuery = null, 
            string? resourceType = null,
            string? category = null);
        Task AddResourceToFavoritesAsync(int resourceId, string userId);
        Task RemoveResourceFromFavorites(int resourceId, string userId);
    }
}