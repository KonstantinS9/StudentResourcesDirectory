using System.ComponentModel.DataAnnotations;

namespace StudentResourcesDirectory.Models.ViewModels.ResourceViewModels
{
    public class CategoryViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;
    }
}
