using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System.Text.RegularExpressions;
using System.Linq;
using PechinchaMarket.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;
using PechinchaMarket.Models;
using Microsoft.AspNetCore.Components.Forms;

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


            var result = new List<Produto>();
            if (string.IsNullOrWhiteSpace(search)) {
                result = produtos;
            }
            else {
                result = searchAlgorithm(produtos, search);
            }

            return View(result);
        }

        public List<Produto> searchAlgorithm(List<Produto> produtos, String input)
        {
            var result = new List<Produto>();

            foreach (Produto produto in produtos)
            {
                foreach (string word in splitNameBasedOnInput(produto.Name, input)){
                    if (compareToSearch(word, input)){
                        result.Add(produto);
                        break;
                    }
                }       
            }

            return result;
        }

        public string[] splitNameBasedOnInput(string name, string input) {
            int numOfWords = Regex.Matches(input, "[a-zA-Z] [a-zA-Z]", RegexOptions.IgnoreCase).Count() + 1;

            var based = "[a-zA-Z]*";
            if (numOfWords >= 1){
                based = string.Concat(string.Concat(Enumerable.Repeat("[a-zA-Z]* ", numOfWords - 1)), "[a-zA-Z]*");
            }

            return Regex.Matches(name, based, RegexOptions.IgnoreCase).Cast<Match>().Select(m => m.Value).ToArray();
        }

        public Boolean compareToSearch(string str1, string input) {
            var str2 = input;
            if (str1.Equals(str2, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            else
            {
                for (int j = -1; j < (str2.Length * 2) - 1; j++)
                {
                    if ((str1.Length == str2.Length && j % 2 == 0) || (str1.Length == str2.Length - 1 && j % 2 != 0))
                    {
                        int differences = 0;
                        for (int i = 0; i < str1.Length; i++)
                        {
                            if (i == Math.Floor(Math.Abs((double)j / 2)))
                            {
                                str2 = str2.Remove(i, 1);
                            }

                            if (!string.Equals(str1[i].ToString(), str2[i].ToString(), StringComparison.OrdinalIgnoreCase))
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
            var blacklisted = new List<string> { "a", "o", "de", "com" };

            foreach (var name in nameList) {
                if (!Regex.IsMatch(input, "[a-zA-Z] "))
                {
                    foreach (string word in name.Split(' '))
                    {
                        if (word.Length > input.Length && !result.Contains(word))
                        {
                            var needcorrectvalue = Math.Floor((double)word.Length * 0.4);
                            for (int i = 0; i < input.Length; i++)
                            {
                                if (string.Equals(word[i].ToString(), input[i].ToString(), StringComparison.OrdinalIgnoreCase))
                                {
                                    needcorrectvalue--;
                                }
                                else
                                {
                                    break;
                                }

                                if (needcorrectvalue <= 0 && i + 1 == input.Length)
                                {
                                    result.Add(word);
                                }
                            }
                        }
                    }
                }else{ 
                    var suggestion = GetWordAfterInput(name, input, blacklisted);
                    if (suggestion != null) {
                        result.Add(suggestion);
                    }
                }
            }
            return Json(result);
        }

        private string GetWordAfterInput(string name, string input, List<string> blockedWords)
        {
            int index = name.IndexOf(input, StringComparison.OrdinalIgnoreCase);
            if (index == -1) // If input not found
                return null;

            index = +input.Length;
            string nextWord;
            do
            {
                nextWord = "";
                if (name.Length < index)
                    return null;

                //Para passar a frente todos os espaços no nome até a proxima palavra
                while (index < name.Length && name[index] == ' ')
                    index++;

                var nextWordIndex = index;
                while (index < name.Length && name[index] != ' ')
                {
                    nextWord = nextWord + name[index].ToString();
                    index++;
                }
            } while (blockedWords.Any(word => word.Equals(nextWord, StringComparison.OrdinalIgnoreCase)));

            // Retira a palavra até 1 a frente
            string result = name.Substring(0, index);
            return result;
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

            return RedirectToAction("Edit", "ListaProdutosController");

        }

        public async Task<IActionResult> AddProductToList()
        {
            return View();
        }
    }
}
