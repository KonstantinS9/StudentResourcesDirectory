using System.ComponentModel.DataAnnotations;

namespace StudentResourcesDirectory.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;
    }
}