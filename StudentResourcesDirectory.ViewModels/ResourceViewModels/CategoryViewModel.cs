using System.ComponentModel.DataAnnotations;

namespace StudentResourcesDirectory.ViewModels.ResourceViewModels
{
    public class CategoryViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;
    }
}
