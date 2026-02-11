using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentResourcesDirectory.Data;
using StudentResourcesDirectory.Models;
using StudentResourcesDirectory.Models.ViewModels.ResourceViewModels;

namespace StudentResourcesDirectory.Controllers
{
    public class ResourceController : Controller
    {
        private ApplicationDbContext _dbContext;

        public ResourceController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var resources = _dbContext.Resources
                .Include(r => r.Category)
                .Include(r => r.Student)
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
                .ToList();

            return View(resources);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new CreateResourceViewModel
            {
                Categories = _dbContext.Categories
                    .OrderBy(c => c.Name)
                    .Select(c => new CategoryViewModel
                    {
                        Id = c.Id,
                        Name = c.Name
                    })
                    .ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(CreateResourceViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Categories = _dbContext.Categories
                    .OrderBy(c => c.Name)
                    .Select(c => new CategoryViewModel
                    {
                        Id = c.Id,
                        Name = c.Name
                    })
                    .ToList();

                return View(viewModel);
            }

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

            _dbContext.Add(resource);
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            if (id <= 0)
            {
                return this.BadRequest();
            }

            var resource = _dbContext
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
                .FirstOrDefault();

            if (resource == null)
            {
                return this.NotFound();
            }

            return this.View(resource);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var resource = _dbContext
                .Resources
                .FirstOrDefault(r => r.Id == id);

            if (resource == null)
            {
                return this.NotFound();
            }

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

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(int id, CreateResourceViewModel viewModel)
        {
            var resource = this._dbContext
                .Resources
                .FirstOrDefault(r => r.Id == id);

            if (resource == null)
            {
                return this.NotFound();
            }

            if (!ModelState.IsValid)
            {
                viewModel.Categories = _dbContext.Categories
                    .OrderBy(c => c.Name)
                    .Select(c => new CategoryViewModel
                    {
                        Id = c.Id,
                        Name = c.Name
                    })
                    .ToList();

                return View(viewModel);
            }

            resource.Title = viewModel.Title;
            resource.Description = viewModel.Description;
            resource.Url = viewModel.Url;
            resource.ResourceType = viewModel.ResourceType;
            resource.CategoryId = viewModel.CategoryId;

            this._dbContext.SaveChanges();

            return this.RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var resource = this._dbContext.Resources.Find(id);

            if (resource == null)
            {
                return this.BadRequest();
            }

            var model = new ResourceDeleteViewModel
            {
                Id = resource.Id,
                Title = resource.Title
            };

            return this.View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(ResourceDeleteViewModel model)
        {
            var resource = this._dbContext.Resources.Find(model.Id);

            if (resource == null)
            {
                return this.BadRequest();
            }

            this._dbContext.Resources.Remove(resource);
            this._dbContext.SaveChanges();

            return this.RedirectToAction(nameof(Index));
        }
    }
}