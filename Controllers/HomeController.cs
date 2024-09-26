using Microsoft.AspNetCore.Mvc;

namespace NewRepository.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}