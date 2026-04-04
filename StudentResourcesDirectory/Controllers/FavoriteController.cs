using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentResourcesDirectory.Services.Core.Contracts;

namespace StudentResourcesDirectory.Controllers
{
    public class FavoriteController : Controller
    {
        private IFavoriteService _favoriteService;
        private UserManager<IdentityUser> _userManager;

        public FavoriteController(IFavoriteService favoriteService, UserManager<IdentityUser> userManager)
        {
            this._favoriteService = favoriteService;
            this._userManager = userManager;
        }

        public async Task<IActionResult> Index(
            string? searchQuery = null, 
            string? resourceType = null,
            string? category = null)
        {
            var userId = _userManager.GetUserId(User)!;

            var resources = await _favoriteService.GetFavoriteResourcesAsync(userId, searchQuery, resourceType, category);

            ViewData["SearchQuery"] = searchQuery;
            ViewData["ResourceType"] = resourceType;
            ViewData["Category"] = category;

            return View(resources);
        }

        [HttpPost]
        public async Task<IActionResult> Add(int resourceId)
        {
            var userId = _userManager.GetUserId(User)!;
            await _favoriteService.AddResourceToFavoritesAsync(resourceId, userId);
            return RedirectToAction(nameof(Index), new { id = userId });
        }

        [HttpPost]
        public async Task<IActionResult> Remove([FromRoute(Name = "id")]int resourceId)
        {
            var userId = _userManager.GetUserId(User)!;
            await _favoriteService.RemoveResourceFromFavorites(resourceId, userId);
            return RedirectToAction(nameof(Index), new { id = userId });
        }
    }
}