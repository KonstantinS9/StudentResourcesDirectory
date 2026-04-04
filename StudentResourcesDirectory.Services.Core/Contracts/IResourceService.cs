using StudentResourcesDirectory.ViewModels.ResourceViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentResourcesDirectory.Services.Core.Contracts
{
    public interface IResourceService
    {
        Task<(IEnumerable<ResourceViewModel>, int TotalPages)> GetAllResourcesOrderedByTitleThenByDateAscAsync(
            string? searchQuery = null, 
            string? resourceType = null,
            string? category = null,
            int pageNumber = 1, int pageSize = 3);
        Task<ResourceDetailsViewModel> GetResourceDetailsAsync(int id);
        Task<CreateResourceViewModel> GetCreateResourceModelAsync();
        Task CreateResourceAsync(CreateResourceViewModel viewModel, string userId);
        Task<CreateResourceViewModel> GetEditResourceModelAsync(int id, string userId);
        Task EditResourceAsync(int id, CreateResourceViewModel viewModel, string userId);
        Task<ResourceDeleteViewModel> GetDeleteResourceModelAsync(int id);
        Task DeleteResourceAsync(int id, string userId);
        Task<bool> IsOwnerAsync(int rеsourceId, string userId);
        Task<IEnumerable<ResourceViewModel>> GetMyResourcesAsync(
            string userId, 
            string? searchQuery = null, 
            string? resourceType = null,
            string? category = null);
    }
}