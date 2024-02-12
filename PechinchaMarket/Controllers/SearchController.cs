using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PechinchaMarket.Areas.Identity.Data;
using PechinchaMarket.Models;

namespace PechinchaMarket.Controllers
{
    public class SearchController : Controller
    {
        private readonly DBPechinchaMarketContext _context;
        public SearchController(DBPechinchaMarketContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> AddToList()
        {

            /* var model = _context.Comerciante
        .Join(_context.Users,
            comerciante => comerciante.UserId,
            user => user.Id
        .Join(_context.Loja,
            user => user.UserId,
            loja => loja.UserId,
            (comerciante, user, loja) => new Tuple<Comerciante, PechinchaMarketUser, Loja>(comerciante, user, loja)))
        .ToList();*/

            var model = _context.Users
    .Join(_context.Comerciante,
        user => user.Id,
        comerciante => comerciante.UserId,
        (user, comerciante) => new { User = user, Comerciante = comerciante })
    .Join(_context.Loja,
        temp => temp.User.Id,
        loja => loja.UserId,
        (temp, loja) => new { temp.User, temp.Comerciante, Loja = loja })
    .Join(_context.ProdutoLoja,
        temp => temp.Loja.Id,
        produtoLoja => produtoLoja.Loja.Id,
        (temp, produtoLoja) => new { temp.User, temp.Comerciante, temp.Loja, ProdutoLoja = produtoLoja })
    .Join(_context.Produto,
        temp => temp.ProdutoLoja.Produto.Id,
        produto => produto.Id,
        (temp, produto) => new { temp.User, temp.Comerciante, temp.Loja, temp.ProdutoLoja, Produto = produto })
    .ToList();

            return View(model);
        }
    }
}
