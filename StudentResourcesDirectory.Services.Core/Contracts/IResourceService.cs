using StudentResourcesDirectory.ViewModels.ResourceViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentResourcesDirectory.Services.Core.Contracts
{
    public interface IResourceService
    {
        Task<IEnumerable<ResourceViewModel>> GetAllResourcesOrderedByTitleThenByDateAscAsync();
        Task<ResourceDetailsViewModel> GetResourceDetailsAsync(int id);
        Task<CreateResourceViewModel> GetCreateResourceModelAsync();
        Task CreateResourceAsync(CreateResourceViewModel viewModel, string userId);
        Task<CreateResourceViewModel> GetEditResourceModelAsync(int id, string userId);
        Task EditResourceAsync(int id, CreateResourceViewModel viewModel, string userId);
        Task<ResourceDeleteViewModel> GetDeleteResourceModelAsync(int id);
        Task DeleteResourceAsync(int id, string userId);
        Task<bool> IsOwnerAsync(int rsourceId, string userId);
        Task<IEnumerable<ResourceViewModel>> GetMyResourcesAsync(string userId);
    }
}