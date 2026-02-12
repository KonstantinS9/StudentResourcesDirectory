using System.ComponentModel.DataAnnotations;

namespace StudentResourcesDirectory.Models.ViewModels.StudentViewModels
{
    public class StudentViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string FacultyNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime RegisteredOn { get; set; }
    }
}