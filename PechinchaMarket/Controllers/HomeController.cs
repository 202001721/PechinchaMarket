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
        private readonly DBPechinchaMarketContext _context;

        public HomeController(DBPechinchaMarketContext context,
                             ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (User.IsInRole("Cliente"))
            {
                var produtos = _context.Produto
                .Where(p => p.ProdEstado == Estado.Approved)
                    .Include(p => p.ProdutoLojas)
                        .ThenInclude(p => p.Loja)
                .Where(p => p.ProdutoLojas.Any(p1 => p1.Discount > 0)).ToList();

                ViewData["Comerciante"] = _context.Comerciante;

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
