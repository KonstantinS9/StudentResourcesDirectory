
namespace StudentResourcesDirectory.ViewModels.CommentViewModels
{
    public class CommentDeleteViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
    }
}