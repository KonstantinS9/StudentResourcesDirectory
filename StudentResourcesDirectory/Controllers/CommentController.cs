using Microsoft.AspNetCore.Mvc;
using StudentResourcesDirectory.Services.Core.Contracts;

namespace StudentResourcesDirectory.Controllers
{
    public class CommentController : Controller
    {
        private ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            this._commentService = commentService;
        }

        public async Task<IActionResult> Index(int resourceId)
        {
            var comments = await _commentService.GetCommentsForResourceOrderedByDate(resourceId);

            return View(comments);
        }
    }
}