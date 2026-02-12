using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentResourcesDirectory.Data;
using StudentResourcesDirectory.Models.ViewModels.ResourceViewModels.StudentViewModels;
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
    }
}
