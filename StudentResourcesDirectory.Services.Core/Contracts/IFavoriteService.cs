
using StudentResourcesDirectory.ViewModels.ResourceViewModels;

namespace StudentResourcesDirectory.Services.Core.Contracts
{
    public interface IFavoriteService
    {
        Task<IEnumerable<ResourceViewModel>> GetFavoriteResourcesAsync(string userId);
        Task AddResourceToFavoritesAsync(int resourceId, string userId);
        Task RemoveResourceFromFavorites(int resourceId, string userId);
    }
}