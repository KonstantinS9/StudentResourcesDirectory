using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentResourcesDirectory.Services.Core.Contracts;
using StudentResourcesDirectory.ViewModels.CommentViewModels;

namespace StudentResourcesDirectory.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly UserManager<IdentityUser> _userManager;

        public CommentController(ICommentService commentService, UserManager<IdentityUser> userManager)
        {
            this._commentService = commentService;
            this._userManager = userManager;
        }

        public async Task<IActionResult> Index([FromRoute(Name = "id")] int resourceId)
        {
            var comments = await _commentService.GetCommentsForResourceOrderedByDate(resourceId);

            return View(comments);
        }

        [HttpGet]
        public async Task<IActionResult> Add(int resourceId)
        {
            var comment = await _commentService.GetAddCommentModelAsync(resourceId);
            return View(comment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(CommentAddViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            var userId = _userManager.GetUserId(User);

            await _commentService.AddCommentAsync(viewModel, userId!);

            return RedirectToAction("Index", "Resource");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var comment = await _commentService.GetDeleteCommentModelAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = _userManager.GetUserId(User);

            var resourceId = await _commentService.DeleteCommentAsync(id, userId!);

            return RedirectToAction(nameof(Index), new { id = resourceId });
        }
    }
}