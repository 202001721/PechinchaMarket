using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PechinchaMarket.Areas.Identity.Data;
using PechinchaMarket.Models;
using System.Linq;

namespace PechinchaMarket.Controllers
{
    public class SearchController : Controller
    {
        private readonly DBPechinchaMarketContext _context;
        private readonly Microsoft.AspNetCore.Identity.UserManager<PechinchaMarketUser> _userManager;
        public SearchController(DBPechinchaMarketContext context, Microsoft.AspNetCore.Identity.UserManager<PechinchaMarketUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> AddToList()
        {
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
            (temp, produto) => Tuple.Create(temp.User, temp.Comerciante, temp.Loja, temp.ProdutoLoja,produto ))
        .ToList();

            var userId = _userManager.GetUserId(User);
            var cliente = _context.Cliente.FirstOrDefault(c => c.UserId == userId);
            var produto = model.FirstOrDefault().Item4.Id;

            ViewData["Listas"] = _context.ListaProdutos
                .Where(l => l.ClienteId == cliente.Id.ToString());

            ViewData["Lojas"] = _context.Loja
        .Join(_context.ProdutoLoja, loja => loja.Id, produtoLoja => produtoLoja.Loja.Id, (loja, produtoLoja) => new { Loja = loja, ProdutoLoja = produtoLoja })
        .Where(joined => joined.ProdutoLoja.Produto.Id == produto)
        .Select(joined => joined.Loja)
        .ToList();

            return View(model);
        }

        [HttpPost, ActionName("AddProduct")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProduct(int? id, int quantityValue, string nome)
        {

            if (id == null)
            {
                return NotFound();
            }

            int contadorDeListas = 1;

            //User logado
            var userId = _userManager.GetUserId(User);
            var cliente = _context.Cliente.FirstOrDefault(c => c.UserId == userId);

            //Produto selecionado
            var produto = await _context.ProdutoLoja.FindAsync(id);
            var name = nome;

            // Verificar se o cliente já possui uma lista de produtos
            var listas = (await _context.ListaProdutos
                .Where(l => l.ClienteId == cliente.Id.ToString())
                .ToListAsync());

            //Se ja tiver uma lista de produtos vai adicionar um detalhe a essa lista
            if (listas.Count > 0 && listas.Any(l => l.name == nome))
            {
                var novoDetalhe = new DetalheListaProd
                {
                    quantity = quantityValue,
                    ListaProdutos = listas.FirstOrDefault(l => l.name == nome),
                    ProdutoLoja = produto,
                };
                _context.Add(novoDetalhe);
            }

            //Não tem nenhuma lista
            else if (listas.Count == 0 || listas.Any(l => l.name != nome))
            {
                var novaListaProdutos = new ListaProdutos
                {
                    name = nome ?? "Lista de compras " + contadorDeListas,
                    ClienteId = cliente.Id.ToString(),
                    state = EstadoProdutoCompra.PorComprar,
                };
                _context.Add(novaListaProdutos);
                contadorDeListas++;

                var novoDetalhe = new DetalheListaProd
                {
                    quantity = quantityValue,
                    ListaProdutos = novaListaProdutos,
                    ProdutoLoja = produto,

                };
                _context.Add(novoDetalhe);
            }
            
            await _context.SaveChangesAsync();

            return View("AddToList");

        }

        public async Task<IActionResult> AddProductToList()
        {
            return View();
        }
    }
}
