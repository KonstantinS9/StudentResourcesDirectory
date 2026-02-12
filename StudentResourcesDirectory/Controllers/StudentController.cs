using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentResourcesDirectory.Data;
using StudentResourcesDirectory.Models;
using StudentResourcesDirectory.Models.ViewModels.ResourceViewModels;
using StudentResourcesDirectory.Models.ViewModels.StudentViewModels;
using System.Collections;

namespace StudentResourcesDirectory.Controllers
{
    public class StudentController : Controller
    {
        private ApplicationDbContext _dbContext;

        public StudentController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var students = await this._dbContext
                .Students
                .AsNoTracking()
                .Include(s => s.Resources)
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


            return this.View(students);
        }

        public async Task<IActionResult> Details(int id)
        {
            var student = await this._dbContext
                .Students
                .Include(s => s.Resources)
                .ThenInclude(s => s.Category)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (student == null)
            {
                return this.NotFound();
            }

            StudentDetailsViewModel viewModel = new StudentDetailsViewModel()
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                RegisteredOn = student.RegisteredOn,
                Email = student.Email,
                FacultyNumber = student.FacultyNumber,
                Resources = student.Resources
            };

            return this.View(viewModel);
        }
        public async Task<IActionResult> ViewResources(int studentId)
        {
            var student = await this._dbContext
                .Students
                .Include(s => s.Resources)
                .ThenInclude(s => s.Category)
                .FirstOrDefaultAsync(s => s.Id == studentId);

            if (student == null)
            {
                return this.NotFound(); 
            }

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
            return this.View(resources);
        }
    }
}