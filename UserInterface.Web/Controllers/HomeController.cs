using Microsoft.AspNetCore.Mvc;

namespace UserInterface.Web.Controllers
{
    public class HomeController : Controller
    {
        public string Index()
        {
            return "Hello World";
        }
    }
}
