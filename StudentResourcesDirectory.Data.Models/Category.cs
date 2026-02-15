using System.ComponentModel.DataAnnotations;
using static StudentResourcesDirectory.GCommon.EntityValidation;
namespace StudentResourcesDirectory.Data.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(CategoryNameMaxLength)]
        [MinLength(CategoryNameMinLength)]
        public string Name { get; set; } = null!;

        public ICollection<Resource> Resources { get; set; } 
            = new List<Resource>();
    }
}