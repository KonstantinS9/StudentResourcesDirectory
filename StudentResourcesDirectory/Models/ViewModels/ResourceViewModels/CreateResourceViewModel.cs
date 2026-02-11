using StudentResourcesDirectory.Models.Enums;
using System.ComponentModel.DataAnnotations;
using static StudentResourcesDirectory.Common.EntityValidation;
namespace StudentResourcesDirectory.Models.ViewModels.ResourceViewModels
{
    public class CreateResourceViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(ResourceTitleMaxLength)]
        [MinLength(ResourceTitleMinLength)]
        public string Title { get; set; } = null!;
        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int StudentId { get; set; }

        [Required]
        [MaxLength(ResourceDescriptionMaxLength)]
        [MinLength(ResourceDescriptionMinLength)]
        public string Description { get; set; } = null!;

        [Required]
        [Url]
        public string Url { get; set; } = null!;

        [Required]
        public ResourceType ResourceType { get; set; }

        public ICollection<CategoryViewModel> Categories { get; set; }
            = new List<CategoryViewModel>();

        public ICollection<Student> Students { get; set; }
            = new List<Student>();

        public DateTime CreatedOn { get; set; }
    }
}