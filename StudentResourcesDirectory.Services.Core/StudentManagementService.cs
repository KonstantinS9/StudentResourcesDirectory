using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudentResourcesDirectory.Data;
using StudentResourcesDirectory.Services.Core.Contracts;
using StudentResourcesDirectory.ViewModels.AdminViewModels.StudentManagementViewModels;
using static StudentResourcesDirectory.GCommon.ExceptionMessages.Student;
namespace StudentResourcesDirectory.Services.Core
{
    public class StudentManagementService : IStudentManagementService
    {
        private ApplicationDbContext _dbContext;
        private UserManager<IdentityUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public StudentManagementService(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this._dbContext = dbContext;
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        public async Task<bool> AssignRoleAsync(string studentId, string role)
        {
            var user = await _userManager.FindByIdAsync(studentId);

            if (user == null)
            {
                return false;
            }

            var roleExists = await _roleManager.RoleExistsAsync(role);
            if (!roleExists)
            {
                return false;
            }

            if (await _userManager.IsInRoleAsync(user, role))
            {
                return true;
            }

            var result = await _userManager.AddToRoleAsync(user, role);

            return result.Succeeded;
        }

        public async Task<StudentManagementViewModel> GetDeleteModelAsync(string studentId)
        {
            var user = await _userManager.FindByIdAsync(studentId);

            if (user == null)
            {
                return null!;
            }

            var student = new StudentManagementViewModel
            {
                Id = user.Id,
                Email = user.Email!
            };

            return student;
        }

        public async Task<IEnumerable<StudentManagementViewModel>> GetAllStudentsOrderedByEmailAsync(string adminUserId)
        {
            var users = await _dbContext.Users
                .Where(u => u.Id != adminUserId)
                .OrderBy(u => u.Email)
                .ToListAsync();

            var students = new List<StudentManagementViewModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                students.Add(new StudentManagementViewModel
                {
                    Id = user.Id,
                    Email = user.Email!,
                    Roles = roles.ToList()
                });
            }

            return students;
        }

        public async Task DeleteStudentAsync(string userId)
        {
            var student = await _dbContext.Students
                .Include(s => s.Resources)
                .FirstOrDefaultAsync(s => s.UserId == userId);

            if (student != null)
            {
                if (student.Resources.Any())
                {
                    _dbContext.Resources.RemoveRange(student.Resources);
                }

                _dbContext.Students.Remove(student);
            }

            var user = await _dbContext.Users.FindAsync(userId);
            if (user != null)
            {
                _dbContext.Users.Remove(user);
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}