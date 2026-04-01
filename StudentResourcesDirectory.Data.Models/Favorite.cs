
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentResourcesDirectory.Data.Models
{
    public class Favorite
    {
        public int Id { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = null!;
        public IdentityUser User { get; set; } = null!;

        [ForeignKey(nameof(Resource))]
        public int ResourceId { get; set; }
        public Resource Resource { get; set; } = null!;
    }
}