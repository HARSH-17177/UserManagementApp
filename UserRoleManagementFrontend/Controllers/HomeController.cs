using Microsoft.AspNetCore.Mvc;

namespace UserRoleManagementFrontend.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
