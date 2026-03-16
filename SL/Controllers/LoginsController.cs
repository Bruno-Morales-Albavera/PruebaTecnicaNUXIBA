using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    public class LoginsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
