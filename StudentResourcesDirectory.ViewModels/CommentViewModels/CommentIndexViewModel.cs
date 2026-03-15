
using Microsoft.AspNetCore.Identity;

namespace StudentResourcesDirectory.ViewModels.CommentViewModels
{
    public class CommentIndexViewModel
    {
        public string Content { get; set; } = null!;
        public DateTime CreatedOn { get; set; }

        public IdentityUser User { get; set; } = null!;
    }
}