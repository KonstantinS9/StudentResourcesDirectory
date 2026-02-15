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
        Task CreateResourceAsync(CreateResourceViewModel viewModel);
        Task<CreateResourceViewModel> GetEditResourceModelAsync(int id);
        Task EditResourceAsync(int id, CreateResourceViewModel viewModel);
        Task<ResourceDeleteViewModel> GetDeleteResourceModelAsync(int id);
        Task DeleteResourceAsync(int id, ResourceDeleteViewModel viewModel);
    }
}