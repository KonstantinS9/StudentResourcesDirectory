using StudentResourcesDirectory.Data.Models;
using StudentResourcesDirectory.ViewModels.ResourceViewModels;

namespace StudentResourcesDirectory.ViewModels.StudentViewModels
{
    public class StudentDetailsViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string FacultyNumber { get; set; } = null!;
        public DateTime RegisteredOn { get; set; }
        public Category Category { get; set; } = null!;

        public IEnumerable<Resource> Resources { get; set; }
            = new List<Resource>();
    }
}