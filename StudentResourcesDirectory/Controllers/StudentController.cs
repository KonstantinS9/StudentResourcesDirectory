using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentResourcesDirectory.Data;

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
                .ToListAsync();
            
            
            return this.View(students);
        }
    }
}
