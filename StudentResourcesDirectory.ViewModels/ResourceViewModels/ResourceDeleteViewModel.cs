using System.ComponentModel.DataAnnotations;

namespace StudentResourcesDirectory.ViewModels.ResourceViewModels
{
    public class ResourceDeleteViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = null!;

        public int StudentId { get; set; }
    }
}
