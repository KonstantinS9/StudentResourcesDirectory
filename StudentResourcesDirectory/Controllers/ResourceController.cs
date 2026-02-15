using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentResourcesDirectory.Data;
using StudentResourcesDirectory.Data.Models;
using StudentResourcesDirectory.Services.Core.Contracts;
using StudentResourcesDirectory.ViewModels.ResourceViewModels;
namespace StudentResourcesDirectory.Controllers
{
    public class ResourceController : Controller
    {
        private readonly IResourceService _resourceService;

        public ResourceController(IResourceService resourceService)
        {
            this._resourceService = resourceService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var resources = await 
                this._resourceService.GetAllResourcesOrderedByTitleThenByDateAscAsync();

            return this.View(resources);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var viewModel = await _resourceService.GetCreateResourceModelAsync();

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateResourceViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel = await this._resourceService.GetCreateResourceModelAsync();

                return this.View(viewModel);
            }

            await _resourceService.CreateResourceAsync(viewModel);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if (id <= 0)
            {
                return this.BadRequest();
            }

            var resource = await 
                this._resourceService.GetResourceDetailsAsync(id);

            if (resource == null)
            {
                return this.NotFound();
            }

            return this.View(resource);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id < 0)
            {
                return this.BadRequest();
            }

            var viewModel = await _resourceService.GetEditResourceModelAsync(id);

            if (viewModel == null)
            {
                return this.NotFound();
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CreateResourceViewModel viewModel)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            if (!ModelState.IsValid)
            {
                viewModel = await _resourceService.GetEditResourceModelAsync(id);

                return this.View(viewModel);
            }

            await _resourceService.EditResourceAsync(id, viewModel);

            return this.RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 0)
            {
                return this.BadRequest();
            }

            var model = await this._resourceService.GetDeleteResourceModelAsync(id);

            if (model == null)
            {
                return this.NotFound();
            }

            return this.View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, ResourceDeleteViewModel viewModel)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            await _resourceService.DeleteResourceAsync(id, viewModel);

            return RedirectToAction(nameof(Index));
        }

    }
}