using Microsoft.AspNetCore.Mvc;

namespace PechinchaMarket.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public AuthenticationController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult AuthenticationSupport(int? section)
        {
            ViewData["Section"] = section ?? 0;
            return View();
        }
    }
}
