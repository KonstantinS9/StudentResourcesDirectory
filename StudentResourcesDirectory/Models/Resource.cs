using StudentResourcesDirectory.Models.Enums;
using System.ComponentModel.DataAnnotations;
using static StudentResourcesDirectory.Common.EntityValidation;
namespace StudentResourcesDirectory.Models
{
    public class Resource
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ResourceTitleMaxLength)]
        [MinLength(ResourceTitleMinLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(ResourceDescriptionMaxLength)]
        [MinLength(ResourceDescriptionMinLength)]
        public string Description { get; set; } = null!;

        [Required]
        [Url]
        public string Url { get; set; } = null!;    

        [Required]
        public ResourceType Type { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}