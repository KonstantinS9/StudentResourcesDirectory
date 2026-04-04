using Microsoft.AspNetCore.Mvc;

namespace StudentResourcesDirectory.Areas.Admin.Controllers
{
    using global::StudentResourcesDirectory.Services.Core.Contracts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;


    namespace StudentResourcesDirectory.Areas.Admin.Controllers
    {
        public class StudentManagementController : BaseAdminController
        {
            private readonly IStudentManagementService _studentService;
            private readonly UserManager<IdentityUser> _userManager;

            public StudentManagementController(IStudentManagementService studentService, UserManager<IdentityUser> userManager)
            {
                this._studentService = studentService;
                this._userManager = userManager;
            }

            [HttpGet]
            public async Task<IActionResult> Index()
            {
                var adminUserId = _userManager.GetUserId(User);

                var students = await _studentService
                    .GetAllStudentsOrderedByEmailAsync(adminUserId!);

                return View(students);
            }

            [HttpGet]
            public async Task<IActionResult> Delete(string id)
            {
                var student = await _studentService.GetDeleteModelAsync(id);

                if (student == null)
                {
                    return NotFound();
                }

                return View(student);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(string id)
            {
                await _studentService.DeleteStudentAsync(id);
                return RedirectToAction(nameof(Index));
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> AssignRole(string studentId, string role)
            {
                var success = await _studentService.AssignRoleAsync(studentId, role);

                if (!success)
                {
                    TempData["Error"] = "Failed to assign role.";
                }

                return RedirectToAction(nameof(Index));
            }
        }
    }
}