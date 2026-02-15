using StudentResourcesDirectory.ViewModels.ResourceViewModels;
using StudentResourcesDirectory.ViewModels.StudentViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentResourcesDirectory.Services.Core.Contracts
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentViewModel>> GetAllStudentsOrderedByFirstNameAscAsync();
        Task<StudentDetailsViewModel> GetStudentDetailsAsync(int id);
        Task<IEnumerable<ResourceViewModel>> GetStudentResourcesAsync(int studentId);
    }
}