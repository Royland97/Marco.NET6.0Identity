using Microsoft.AspNetCore.Mvc;

namespace UserInterface.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
