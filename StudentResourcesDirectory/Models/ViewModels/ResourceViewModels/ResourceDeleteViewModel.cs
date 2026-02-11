using System.ComponentModel.DataAnnotations;

namespace StudentResourcesDirectory.Models.ViewModels.ResourceViewModels
{
    public class ResourceDeleteViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = null!;
    }
}
