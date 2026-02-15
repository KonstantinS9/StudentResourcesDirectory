using Microsoft.EntityFrameworkCore;
using StudentResourcesDirectory.Data;
using StudentResourcesDirectory.Services.Core.Contracts;
using StudentResourcesDirectory.ViewModels.ResourceViewModels;
using StudentResourcesDirectory.ViewModels.StudentViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentResourcesDirectory.Services.Core
{
    public class StudentService : IStudentService
    {
        private ApplicationDbContext _dbContext;

        public StudentService(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<IEnumerable<StudentViewModel>> GetAllStudentsOrderedByFirstNameAscAsync()
        {
            var students = await this._dbContext
                .Students
                .AsNoTracking()
                .Include(s => s.Resources)
                .OrderBy(s => s.FirstName)
                .Select(s => new StudentViewModel
                {
                    Id = s.Id,
                    Email = s.Email,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    RegisteredOn = s.RegisteredOn,
                    FacultyNumber = s.FacultyNumber
                })
                .ToListAsync();

            return students;
        }

        public async Task<StudentDetailsViewModel> GetStudentDetailsAsync(int id)
        {
            var student = await _dbContext
                .Students
                .Include(s => s.Resources)
                .ThenInclude(s => s.Category)
                .FirstOrDefaultAsync(s => s.Id == id);

            var viewModel = new StudentDetailsViewModel()
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                RegisteredOn = student.RegisteredOn,
                Email = student.Email,
                FacultyNumber = student.FacultyNumber,
                Resources = student.Resources
            };

            return viewModel;
        }

        public async Task<IEnumerable<ResourceViewModel>> GetStudentResourcesAsync(int studentId)
        {
            var student = await _dbContext
                .Students
                .Include(s => s.Resources)
                .ThenInclude(s => s.Category)
                .FirstOrDefaultAsync(s => s.Id == studentId);

            var resources = student
                .Resources
                .Where(r => r.Student.FacultyNumber == student.FacultyNumber)
                .Select(r => new ResourceViewModel
                {
                    Id = r.Id,
                    Category = r.Category.Name,
                    Description = r.Description,
                    ResourceType = r.ResourceType,
                    Title = r.Title,
                    Url = r.Url,
                    Student = r.Student.FirstName + " " + r.Student.LastName,
                })
                .ToList();

            return resources;
        }
    }
}