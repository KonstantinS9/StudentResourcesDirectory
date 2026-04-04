using Microsoft.AspNetCore.Mvc;

namespace StudentResourcesDirectory.Areas.Admin.Controllers
{
    public class HomeController : BaseAdminController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
