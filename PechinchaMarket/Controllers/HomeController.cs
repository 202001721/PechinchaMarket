using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PechinchaMarket.Areas.Identity.Data;
using PechinchaMarket.Models;
using System.Diagnostics;

namespace PechinchaMarket.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Microsoft.AspNetCore.Identity.UserManager<PechinchaMarketUser> _userManager;
        private readonly DBPechinchaMarketContext _context;

        public HomeController(DBPechinchaMarketContext context,
            Microsoft.AspNetCore.Identity.UserManager<PechinchaMarketUser> userManager,
                             ILogger<HomeController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (User.IsInRole("Cliente"))
            {
                var produtos = _context.Produto
                .Where(p => p.ProdEstado == Estado.Approved)
                    .Include(p => p.ProdutoLojas)
                        .ThenInclude(p => p.Loja).ToList();

                ViewData["Comerciante"] = _context.Comerciante;

                var userId = _userManager.GetUserId(User);
                if (userId == null)
                {
                    ViewData["Categorias"] = new List<Categoria>();
                }
                else
                {
                    ViewData["Categorias"] = _context.Cliente.Where(c => c.UserId == userId).Select(c => c.Preferencias).FirstOrDefault();
                }

                    var categorias = _context.Cliente
                .Where(c => c.Id.Equals(_userManager.GetUserId)).Select(c => c.Preferencias).FirstOrDefault();

                return View(produtos);
            }
            else {
                return View();
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
