using Microsoft.EntityFrameworkCore;
using StudentResourcesDirectory.Data;
using StudentResourcesDirectory.Data.Models;
using StudentResourcesDirectory.Services.Core.Contracts;
using StudentResourcesDirectory.ViewModels.ResourceViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentResourcesDirectory.Services.Core
{
    public class ResourceService : IResourceService
    {
        private readonly ApplicationDbContext _dbContext;

        public ResourceService(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<CreateResourceViewModel> GetCreateResourceModelAsync()
        {
            var viewModel = new CreateResourceViewModel
            {
                Categories = await this._dbContext.Categories
                .OrderBy(c => c.Name)
                .Select(c => new CategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync()
            };

            return viewModel;
        }

        public async Task<IEnumerable<ResourceViewModel>> GetAllResourcesOrderedByTitleThenByDateAscAsync()
        {
            var resources = await this._dbContext.Resources
                .Include(r => r.Category)
                .Include(r => r.Student)
                .AsNoTracking()
                .OrderBy(r => r.Title)
                .ThenBy(r => r.CreatedOn)
                .Select(r => new ResourceViewModel
                {
                    Id = r.Id,
                    Title = r.Title,
                    Category = r.Category.Name,
                    Description = r.Description,
                    Student = r.Student.FirstName + " " + r.Student.LastName,
                    Url = r.Url,
                    ResourceType = r.ResourceType
                })
                .ToListAsync();

            return resources;
        }

        public async Task<ResourceDetailsViewModel> GetResourceDetailsAsync(int id)
        {
            var resource = await this._dbContext
               .Resources
               .AsNoTracking()
               .Include(r => r.Category)
               .Where(r => r.Id == id)
               .Select(r => new ResourceDetailsViewModel
               {
                   Id = r.Id,
                   Title = r.Title,
                   Category = r.Category,
                   Student = r.Student,
                   Description = r.Description,
                   Url = r.Url,
                   ResourceType = r.ResourceType,
                   CreatedOn = r.CreatedOn,
               })
               .FirstOrDefaultAsync();

            return resource;
        }

        public async Task CreateResourceAsync(CreateResourceViewModel viewModel)
        {
            Resource resource = new Resource()
            {
                Title = viewModel.Title,
                Description = viewModel.Description,
                Url = viewModel.Url,
                StudentId = 1,
                CategoryId = viewModel.CategoryId,
                ResourceType = viewModel.ResourceType,
                CreatedOn = DateTime.Now
            };

            await this._dbContext.Resources.AddAsync(resource);
            await this._dbContext.SaveChangesAsync();
        }

        public async Task<CreateResourceViewModel> GetEditResourceModelAsync(int id)
        {
            var resource = await this._dbContext
                .Resources
                .FirstOrDefaultAsync(r => r.Id == id);

            CreateResourceViewModel viewModel = new CreateResourceViewModel
            {
                Title = resource.Title,
                Description = resource.Description,
                Url = resource.Url,
                ResourceType = resource.ResourceType,
                CategoryId = resource.CategoryId,
                Categories = _dbContext.Categories.OrderBy(c => c.Name).Select(c => new CategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToList()
            };

            return viewModel;
        }

        public async Task EditResourceAsync(int id, CreateResourceViewModel viewModel)
        {
            var resource = await this._dbContext
                .Resources
                .FirstOrDefaultAsync(r => r.Id == id);

            resource.Title = viewModel.Title;
            resource.Description = viewModel.Description;
            resource.Url = viewModel.Url;
            resource.ResourceType = viewModel.ResourceType;
            resource.CategoryId = viewModel.CategoryId;

            this._dbContext.SaveChanges();
        }

        public async Task<ResourceDeleteViewModel> GetDeleteResourceModelAsync(int id)
        {
            var resource = await this._dbContext.Resources.FindAsync(id);

            var model = new ResourceDeleteViewModel
            {
                Id = resource.Id,
                Title = resource.Title
            };

            return model;
        }

        public async Task DeleteResourceAsync(int id, ResourceDeleteViewModel viewModel)
        {
            var resource = await this._dbContext.Resources.FindAsync(id);

            this._dbContext.Resources.Remove(resource);
            await  this._dbContext.SaveChangesAsync();
        }
    }
}