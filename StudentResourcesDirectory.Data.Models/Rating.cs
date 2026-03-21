
using Microsoft.AspNetCore.Identity;
using StudentResourcesDirectory.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentResourcesDirectory.Data.Models
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public RatingResource RatingResource { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = null!;
        public IdentityUser User { get; set; } = null!;

        [ForeignKey(nameof(Resource))]
        public int ResourceId { get; set; }
        public Resource Resource { get; set; } = null!;
    }
}