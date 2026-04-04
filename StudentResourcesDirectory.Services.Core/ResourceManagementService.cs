
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentResourcesDirectory.Data;
using StudentResourcesDirectory.Data.Models;
using StudentResourcesDirectory.Services.Core.Contracts;
using StudentResourcesDirectory.ViewModels.AdminViewModels.ResourceManagementViewModels;
using static StudentResourcesDirectory.GCommon.ExceptionMessages.Resource;
namespace StudentResourcesDirectory.Services.Core
{
    public class ResourceManagementService : IResourceManagementService
    {
        private readonly ApplicationDbContext _dbContext;

        public ResourceManagementService(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task CreateResourceAsync(ResourceManagementViewModel viewModel)
        {
            var categoryExists = await _dbContext.Categories
                .AnyAsync(c => c.Id == viewModel.CategoryId);

            if (!categoryExists)
            {
                throw new Exception("Invalid CategoryId");
            }

            var resource = new Resource
            {
                Title = viewModel.Title,
                Description = viewModel.Description,
                Url = viewModel.Url,
                ResourceType = viewModel.ResourceType,
                CategoryId = viewModel.CategoryId,
                CreatedOn = DateTime.UtcNow
            };

            await _dbContext.Resources.AddAsync(resource);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteResourceAsync(int id)
        {
            var resource = await _dbContext.Resources.FirstOrDefaultAsync(r => r.Id == id);
            if (resource == null)
                throw new ArgumentException(ResourceNotFound);

            _dbContext.Resources.Remove(resource);
            await _dbContext.SaveChangesAsync();
        }

        public async Task EditResourceAsync(ResourceManagementViewModel viewModel)
        {
            var resource = await _dbContext.Resources.FirstOrDefaultAsync(r => r.Id == viewModel.Id);
            if (resource == null)
                throw new ArgumentException(ResourceNotFound);

            resource.Title = viewModel.Title;
            resource.Description = viewModel.Description;
            resource.Url = viewModel.Url;
            resource.CategoryId = viewModel.CategoryId;
            resource.ResourceType = viewModel.ResourceType;

            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ResourceManagementViewModel>> GetAllResourcesOrderedByTitleThenByDateAsync()
        {
            var resources = await _dbContext
                .Resources
                .AsNoTracking()
                .Select(r => new ResourceManagementViewModel
                {
                    Id = r.Id,
                    Title = r.Title,
                    Description = r.Description,
                    Url = r.Url,
                    CreatedOn = r.CreatedOn,
                    ResourceType = r.ResourceType,
                })
                .OrderBy(r => r.Title)
                .ThenBy(r => r.CreatedOn)
                .ToArrayAsync();

            return resources;
        }

        public async Task<ResourceManagementViewModel> GetCreateResourceModelAsync()
        {
            var viewModel = new ResourceManagementViewModel()
            {
                Categories = await _dbContext.Categories
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    }).ToListAsync(),
                CreatedOn = DateTime.UtcNow
            };

            return viewModel;
        }

        public async Task<ResourceManagementViewModel> GetDeleteModelAsync(int id)
        {
            var viewModel = new ResourceManagementViewModel()
            {
                Id = id
            };

            return viewModel;
        }

        public async Task<ResourceManagementViewModel> GetEditModelAsync(int id)
        {
            var resource = await _dbContext.Resources.FirstOrDefaultAsync(r => r.Id == id);

            if (resource == null)
            {
                return null!;
            }

            var viewModel = new ResourceManagementViewModel()
            {
                Id = resource.Id,
                Categories = await _dbContext.Categories
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    }).ToListAsync(),
                CategoryId = resource.CategoryId,
                Title = resource.Title,
                Description = resource.Description,
                ResourceType = resource.ResourceType,
                Url = resource.Url,
            };

            return viewModel;
        }
    }
}