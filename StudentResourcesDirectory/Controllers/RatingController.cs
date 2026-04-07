using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentResourcesDirectory.Services.Core.Contracts;
using StudentResourcesDirectory.ViewModels.RatingViewModels;

namespace StudentResourcesDirectory.Controllers
{
    [Authorize]
    public class RatingController : Controller
    {
        private readonly IRatingService _ratingService;
        private readonly UserManager<IdentityUser> _userManager;

        public RatingController(IRatingService ratingService, UserManager<IdentityUser> userManager)
        {
            this._ratingService = ratingService;
            this._userManager = userManager;
        }
        public async Task<IActionResult> Index(int id)
        {
            var ratings = await _ratingService.GetAllRatingsForResourceOrderedByDateAsync(id);

            if (ratings == null)
            {
                return NotFound();
            }

            return View(ratings);
        }

        [HttpGet]
        public async Task<IActionResult> Add(int id)
        {
            var rating = await _ratingService.GetAddRatingModelAsync(id);
             
            if (rating == null)
            {
                return NotFound();
            }

            return View(rating);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(RatingAddViewModel viewModel, int id)
        {
            var userId = _userManager.GetUserId(User)!;

            if(!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var resourceId = await _ratingService.AddRatingAsync(viewModel, id, userId!);

            return RedirectToAction("Index", new { id = resourceId });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var rating = await _ratingService.GetDeleteRatingModelAsync(id);
            if (rating == null)
            {
                return NotFound();
            }

            return View(rating);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = _userManager.GetUserId(User)!;

            var resourceId = await _ratingService.DeleteRatingAsync(id, userId);

            return RedirectToAction("Index", new { id = resourceId});
        }
    }
}