using Microsoft.AspNetCore.Mvc;
using StudentResourcesDirectory.Services.Core.Contracts;
using StudentResourcesDirectory.ViewModels.AdminViewModels.ResourceManagementViewModels;

namespace StudentResourcesDirectory.Areas.Admin.Controllers
{
    public class ResourceManagementController : BaseAdminController
    {
        private readonly IResourceManagementService _resourceManagementService;

        public ResourceManagementController(IResourceManagementService resourceManagementService)
        {
            this._resourceManagementService = resourceManagementService;
        }

        public async Task<IActionResult> Index()
        {
            var resources = await _resourceManagementService
                .GetAllResourcesOrderedByTitleThenByDateAsync();
            return View(resources);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = await _resourceManagementService.GetCreateResourceModelAsync();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ResourceManagementViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Categories = (await _resourceManagementService.GetCreateResourceModelAsync()).Categories;
                return View(viewModel);
            }

            await _resourceManagementService.CreateResourceAsync(viewModel);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var viewModel = await _resourceManagementService.GetDeleteModelAsync(id);
            if (viewModel == null)
            {
                return NotFound();
            }
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _resourceManagementService.DeleteResourceAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var viewModel = await _resourceManagementService.GetEditModelAsync(id);
            if (viewModel == null)
            {
                return NotFound();
            }
            return View(viewModel);
        }

        public async Task<IActionResult> Edit(ResourceManagementViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Categories = (await _resourceManagementService.GetEditModelAsync(viewModel.Id)).Categories;
                return View(viewModel);
            }
            await _resourceManagementService.EditResourceAsync(viewModel);
            return RedirectToAction(nameof(Index));
        }
    }
}