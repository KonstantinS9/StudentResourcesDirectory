
using StudentResourcesDirectory.ViewModels.AdminViewModels.StudentManagementViewModels;

namespace StudentResourcesDirectory.Services.Core.Contracts
{
    public interface IStudentManagementService
    {
        Task<IEnumerable<StudentManagementViewModel>> GetAllStudentsOrderedByEmailAsync(string adminUserId);
        Task<bool> AssignRoleAsync(string id, string role);
        Task<StudentManagementViewModel> GetDeleteModelAsync(string id);
        Task DeleteStudentAsync(string id);
    }
}