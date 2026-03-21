using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using StudentResourcesDirectory.ViewModels;
namespace StudentResourcesDirectory.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("/Home/Error/{statusCode}")]
        public IActionResult Error(int statusCode)
        {
            if (statusCode == StatusCodes.Status404NotFound)
            {
                return View("NotFound");
            }
            if (statusCode == StatusCodes.Status400BadRequest)
            {
                return View("BadRequest");
            }
            if (statusCode == StatusCodes.Status500InternalServerError)
            {
                return View("InternalServerError");
            }

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
