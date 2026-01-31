using System.ComponentModel.DataAnnotations;
using static StudentResourcesDirectory.Common.EntityValidation;
namespace StudentResourcesDirectory.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(StudentFirstNameMaxLength)]
        [MinLength(StudentFirstNameMinLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(StudentLastNameMaxLength)]
        [MinLength(StudentLastNameMinLength)]
        public string LastName { get; set; } = null!;

        [Required]
        public string FacultyNumber { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        public DateTime RegisteredOn { get; set; }

        public ICollection<Resource> Resources { get; set; }
            = new List<Resource>();
    }
}