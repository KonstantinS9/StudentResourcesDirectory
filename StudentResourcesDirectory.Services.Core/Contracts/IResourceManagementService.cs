
using StudentResourcesDirectory.ViewModels.AdminViewModels.ResourceManagementViewModels;

namespace StudentResourcesDirectory.Services.Core.Contracts
{
    public interface IResourceManagementService
    {
        Task<IEnumerable<ResourceManagementViewModel>> GetAllResourcesOrderedByTitleThenByDateAsync();
        Task<ResourceManagementViewModel> GetCreateResourceModelAsync();
        Task CreateResourceAsync(ResourceManagementViewModel viewModel);
        Task<ResourceManagementViewModel> GetDeleteModelAsync(int id);
        Task DeleteResourceAsync(int id);
        Task<ResourceManagementViewModel> GetEditModelAsync(int id);
        Task EditResourceAsync(ResourceManagementViewModel viewModel);
    }
}