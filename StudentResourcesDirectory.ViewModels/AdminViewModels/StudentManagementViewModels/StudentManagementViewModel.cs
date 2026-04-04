

namespace StudentResourcesDirectory.ViewModels.AdminViewModels.StudentManagementViewModels
{
    public class StudentManagementViewModel
    {
        public string Id { get; set; } = null!;
        public string Email { get; set; } = null!;

        public ICollection<string> Roles { get; set; } 
            = new List<string>();
    }
}