using StudentResourcesDirectory.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace StudentResourcesDirectory.Models
{
    public class Resource
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        [Required]
        public string Url { get; set; } = null!;    

        [Required]
        public ResourceType Type { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}