using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentResourcesDirectory.Data;
using StudentResourcesDirectory.Data.Models;
using StudentResourcesDirectory.Services.Core.Contracts;
using StudentResourcesDirectory.ViewModels.ResourceViewModels;
using System.Security.Claims;
namespace StudentResourcesDirectory.Controllers
{
    public class ResourceController : Controller
    {
        private readonly IResourceService _resourceService;
        private readonly UserManager<IdentityUser> _userManager;

        public ResourceController(IResourceService resourceService, UserManager<IdentityUser> userManager)
        {
            this._resourceService = resourceService;
            this._userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var resources = await 
                this._resourceService.GetAllResourcesOrderedByTitleThenByDateAscAsync();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            ViewData["CurrentUserId"] = userId;

            return this.View(resources);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Create()
        {
            if (User.IsInRole("Admin"))
                return Forbid();
            var viewModel = await _resourceService.GetCreateResourceModelAsync();

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateResourceViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (User.IsInRole("Admin"))
                return Forbid();

            var userId = _userManager.GetUserId(User);

            await _resourceService.CreateResourceAsync(model, userId);

            return RedirectToAction(nameof(MyResources));
        }

        [HttpGet]
        [Authorize]
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
            var userId = _userManager.GetUserId(User);

            var model = await _resourceService.GetEditResourceModelAsync(id, userId);

            return View(model);
        }


        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, CreateResourceViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var userId = _userManager.GetUserId(User);

            await _resourceService.EditResourceAsync(id, model, userId);

            return RedirectToAction(nameof(MyResources));
        }



        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = _userManager.GetUserId(User);

            if (!await _resourceService.IsOwnerAsync(id, userId))
                return Forbid();

            var model = await _resourceService.GetDeleteResourceModelAsync(id);
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = _userManager.GetUserId(User);

            await _resourceService.DeleteResourceAsync(id, userId);

            return RedirectToAction(nameof(MyResources));
        }


        [Authorize]
        public async Task<IActionResult> MyResources()
        {
            var userId = _userManager.GetUserId(User);

            var model = await _resourceService.GetMyResourcesAsync(userId);

            return View(model);
        }
    }
}