using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudentResourcesDirectory.Data;
using StudentResourcesDirectory.Data.Models;
using StudentResourcesDirectory.Services.Core.Contracts;
using StudentResourcesDirectory.ViewModels.ResourceViewModels;
using System;
using System.Collections.Generic;
using System.Security.Claims;
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

        public async Task CreateResourceAsync(CreateResourceViewModel viewModel, string userId)
        {
            var student = await _dbContext.Students.FirstOrDefaultAsync(s => s.UserId == userId);
            if (student == null)
                throw new InvalidOperationException("No student profile found for this user.");

            var resource = new Resource()
            {
                Title = viewModel.Title,
                Description = viewModel.Description,
                Url = viewModel.Url,
                CategoryId = viewModel.CategoryId,
                ResourceType = viewModel.ResourceType,
                CreatedOn = DateTime.Now,
                StudentId = student.Id  
            };

            await _dbContext.Resources.AddAsync(resource);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<CreateResourceViewModel> GetEditResourceModelAsync(int id, string userId)
        {
            var resource = await _dbContext.Resources
                .Include(r => r.Student)
                .Include(r => r.Category)
                .FirstOrDefaultAsync(r => r.Id == id);

            var categories = await _dbContext.Categories
                .Select(c => new CategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();

            if (resource == null)
                throw new InvalidOperationException("Resource not found.");

            if (resource.Student.UserId != userId)
                throw new UnauthorizedAccessException("Not owner.");

            return new CreateResourceViewModel
            {
                Title = resource.Title,
                Description = resource.Description,
                Url = resource.Url,
                CategoryId = resource.CategoryId,
                ResourceType = resource.ResourceType,
                Categories = categories
            };
        }

        public async Task EditResourceAsync(int id, CreateResourceViewModel model, string userId)
        {
            var resource = await _dbContext.Resources
                .Include(r => r.Student)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (resource == null)
                throw new InvalidOperationException("Resource not found.");

            if (resource.Student.UserId != userId)
                throw new UnauthorizedAccessException("Not owner.");

            resource.Title = model.Title;
            resource.Description = model.Description;
            resource.Url = model.Url;
            resource.CategoryId = model.CategoryId;
            resource.ResourceType = model.ResourceType;

            await _dbContext.SaveChangesAsync();
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

        public async Task DeleteResourceAsync(int id, string userId)
        {
            var resource = await _dbContext.Resources
            .Include(r => r.Student)
            .FirstOrDefaultAsync(r => r.Id == id);

            if (resource == null)
                throw new InvalidOperationException("Resource not found.");

            if (resource.Student.UserId != userId)
                throw new UnauthorizedAccessException("You are not the owner of this resource.");

            _dbContext.Resources.Remove(resource);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> IsOwnerAsync(int resourceId, string userId)
        {
            return await _dbContext.Resources
                .AnyAsync(r => r.Id == resourceId && r.Student.UserId == userId);
        }


        public async Task<IEnumerable<ResourceViewModel>> GetMyResourcesAsync(string userId)
        {
            return await _dbContext.Resources
                .Where(r => r.Student.UserId == userId)
                .Select(r => new ResourceViewModel
                {
                    Id = r.Id,
                    Title = r.Title,
                    Description = r.Description,
                    Url = r.Url,
                    Category = r.Category.Name,
                    Student = r.Student.FirstName + " " + r.Student.LastName,
                    ResourceType = r.ResourceType
                })
                .ToListAsync();
        }
    }
}