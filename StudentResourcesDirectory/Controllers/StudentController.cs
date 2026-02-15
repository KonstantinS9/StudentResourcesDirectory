using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentResourcesDirectory.Data;
using StudentResourcesDirectory.Services.Core.Contracts;
using StudentResourcesDirectory.ViewModels.ResourceViewModels;
using StudentResourcesDirectory.ViewModels.StudentViewModels;
using System.Collections;

namespace StudentResourcesDirectory.Controllers
{
    public class StudentController : Controller
    {
        private ApplicationDbContext _dbContext;
        private IStudentService _studentService;

        public StudentController(ApplicationDbContext dbContext, IStudentService studentService)
        {
            this._dbContext = dbContext;
            this._studentService = studentService;
        }

        public async Task<IActionResult> Index()
        {
            var students = await _studentService
                .GetAllStudentsOrderedByFirstNameAscAsync();


            return this.View(students);
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id <= 0)
            {
                return this.BadRequest();
            }

            var viewModel = await _studentService
                .GetStudentDetailsAsync(id);

            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }
        public async Task<IActionResult> ViewResources(int studentId)
        {
            var resources = await _studentService.GetStudentResourcesAsync(studentId);
            return this.View(resources);
        }
    }
}