using StudentResourcesDirectory.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static StudentResourcesDirectory.GCommon.EntityValidation;
namespace StudentResourcesDirectory.Data.Models
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
        public ResourceType ResourceType { get; set; }

        public DateTime CreatedOn { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        [ForeignKey(nameof(Student))]
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;
    }
}