using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System.Text.RegularExpressions;
using System.Linq;
using PechinchaMarket.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;
using PechinchaMarket.Models;

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

        [HttpPost]
        public IActionResult Search(string searchText)
        {
            return RedirectToAction("SearchResults", new { search = searchText});
        }

        // Action method to display search results
        public IActionResult SearchResults(string search)
        {
            var produtos = _context.Produto.ToList();

            var result = searchAlgorithm(produtos, search);

            return View(result);
        }

        public List<Produto> searchAlgorithm(List<Produto> produtos, String input)
        {
            var result = new List<Produto>();

            foreach (Produto produto in produtos)
            {
                //Se o input for contido então foi encontrado um produto com
                //esse nome e o código deve acabar aqui, assim determinando
                //que o input está escrito corretamente.
                if (compareToSearch(produto.Name, input)){
                    result.Add(produto);
                }
            }

            return result;
        }

        public Boolean compareToSearch(string str1, string str2) {
            foreach (string word in str1.Split(' '))
            {
                if (word.Equals(str2, StringComparison.OrdinalIgnoreCase)) {
                    return true;
                }
                else
                {
                    for (int j=-1; j < (str2.Length * 2) + 1; j++){ 
                        if ((word.Length == str2.Length && j % 2 == 0) || (word.Length == str2.Length - 1 && j % 2 != 0))
                        {
                            int differences = 0;
                            for (int i = 0; i < word.Length; i++)
                            {
                                if (i != Math.Floor((double)j/2) && !string.Equals(word[i].ToString(), str2[i].ToString(), StringComparison.OrdinalIgnoreCase))
                                {
                                    differences++;
                                    if (differences > 1)
                                    {
                                        break;
                                    }
                                }
                            }
                            if (differences <= 1)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        public async Task<IActionResult> ShowImage(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var produto = await _context.Produto
                .FirstOrDefaultAsync(m => m.Id == id);

            return File(produto.Image, "image/jpg");
        }

        [HttpGet]
        public IActionResult GetSugestiveNames(string input) { 
            var nameList = _context.Produto
                                      .Select(m => m.Name)
                                      .Distinct()
                                      .ToList();

            var result = new List<string>();

           foreach(var name in nameList) {
                foreach (string word in name.Split(' ')) {
                   if (word.Length > input.Length) {
                        var needcorrectvalue = Math.Floor((double) word.Length * 0.4);
                        for (int i = 0; i < input.Length; i++) {
                            if (string.Equals(word[i].ToString(), input[i].ToString(), StringComparison.OrdinalIgnoreCase))
                            {
                                needcorrectvalue--;
                            }
                            else 
                            {
                                break;
                            }

                            if (needcorrectvalue <= 0 && i + 1 == input.Length) {
                                result.Add(word);
                            }
                        }
                    }
                }
           }

            return Json(result);
        }
          
        public async Task<ActionResult> AddToList(int id)
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
        .Join(_context.Produto.Where(produto => produto.Id == id),
            temp => temp.ProdutoLoja.Produto.Id,
            produto => produto.Id,
            (temp, produto) => Tuple.Create(temp.User, temp.Comerciante, temp.Loja, temp.ProdutoLoja,produto ))
        .ToList();

            var userId = _userManager.GetUserId(User);
            var cliente = _context.Cliente.FirstOrDefault(c => c.UserId == userId);
            var produto = model.Select(x => x.Item5.Id).FirstOrDefault();

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

            return RedirectToAction("Edit", "ListaProdutosController");

        }

        public async Task<IActionResult> AddProductToList()
        {
            return View();
        }
    }
}
