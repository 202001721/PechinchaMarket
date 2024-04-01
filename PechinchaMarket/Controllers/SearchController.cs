using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System.Text.RegularExpressions;
using System.Linq;
using PechinchaMarket.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;
using PechinchaMarket.Models;
using Microsoft.AspNetCore.Components.Forms;
using System;
using Microsoft.AspNetCore.Hosting;

namespace PechinchaMarket.Controllers
{
    public class SearchController : Controller
    {
        private readonly DBPechinchaMarketContext _context;
        private readonly Microsoft.AspNetCore.Identity.UserManager<PechinchaMarketUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public SearchController(DBPechinchaMarketContext context,
            Microsoft.AspNetCore.Identity.UserManager<PechinchaMarketUser> userManager,
            IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Função Search - Quando o cliente pesquisa algo, a função chama a outra função SearchResults
        /// </summary>
        /// <param name="searchText">texto a ser pesquisado</param>
        /// <returns>redireciona para a ação SearchResults</returns>
        [HttpPost]
        public IActionResult Search(string searchText)
        {
            return RedirectToAction("SearchResults", new { search = searchText });
        }

        /// <summary>
        /// Função SearchResults - É realizada quando o cliente pesquisa na barra de pesquisa
        /// </summary>
        /// <param name="search">texto inserido na barra de pesquisa</param>
        /// <returns>View com os produtos do resultado da pesquisa</returns>
        // Action method to display search results
        public IActionResult SearchResults(string search)
        {
            /*
            var produtos = _context.Produto.Where(p => p.ProdEstado == Estado.Approved)
                                           .Select(p => new { 
                                                   p.Name,
                                                   p.Brand,
                                                   p.ProdutoLojas.OrderBy(x => x.Price).FirstOrDefault().Price
                                           })
                                           .ToList();
            */

            var produtos = _context.Produto
                .Where(p => p.ProdEstado == Estado.Approved)
                    .Include(p => p.ProdutoLojas)
                        .ThenInclude(p => p.Loja).ToList();

            ViewData["Comerciante"] = _context.Comerciante;
            ViewData["Categoria"] = Enum.GetValues(typeof(Categoria));


            var result = new List<Produto>();
            if (string.IsNullOrWhiteSpace(search))
            {
                result = produtos;
            }
            else
            {
                result = searchAlgorithm(produtos, search);
            }

            return View(result);
        }

        /// <summary>
        /// Função searchAlgorithm - Executa o algoritmo de pesquisa na lista de produtos com base na string de entrada
        /// </summary>
        /// <param name="produtos">Lista de produtos</param>
        /// <param name="input">String de entrada da pesquisa</param>
        /// <returns>Lista de Produtos correspondentes à pesquisa</returns>
        public List<Produto> searchAlgorithm(List<Produto> produtos, String input)
        {
            var result = new List<Produto>();

            foreach (Produto produto in produtos)
            {
                foreach (string word in splitNameBasedOnInput(produto.Name, input))
                {
                    if (compareToSearch(word, input))
                    {
                        result.Add(produto);
                        break;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Função splitNameBasedOnInput - Divide o nome do produto em uma matriz de palavras com base na string de entrada
        /// </summary>
        /// <param name="name">nome a ser dividido</param>
        /// <param name="input">String de entrada na qual o nome será dividido</param>
        /// <returns>Uma matriz de palavras resultante da divisão do nome</returns>
        public string[] splitNameBasedOnInput(string name, string input)
        {
            int numOfWords = Regex.Matches(input, "[a-zA-Z] [a-zA-Z]", RegexOptions.IgnoreCase).Count() + 1;

            var based = "[a-zA-Z]*";
            if (numOfWords >= 1)
            {
                based = string.Concat(string.Concat(Enumerable.Repeat("[a-zA-Z]* ", numOfWords - 1)), "[a-zA-Z]*");
            }

            return Regex.Matches(name, based, RegexOptions.IgnoreCase).Cast<Match>().Select(m => m.Value).ToArray();
        }

        /// <summary>
        /// Função compareToSearch - Compara duas strings para determinar se são semelhantes
        /// </summary>
        /// <param name="str1">A primeira string a ser comparada</param>
        /// <param name="input">A segunda string a ser comparada</param>
        /// <returns>True se as strings forem semelhantes com no máximo 1 caractere de diferença, False caso contrário</returns>
        public Boolean compareToSearch(string str1, string input)
        {
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

        /// <summary>
        /// Função ShowImage - Mostra a imagem do produto pretendido
        /// </summary>
        /// <param name="id">id do produto</param>
        /// <returns>ficheiro da imagem do produto</returns>
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

        /// <summary>
        /// Função GetImage - Obtém a imagem de perfil do utilizador atual com base no papel de utilizador.
        /// </summary>
        /// <returns>Um objeto IActionResult contendo a imagem de perfil do utilizador</returns>
        [HttpGet]
        public async Task<IActionResult> GetPerfilImage()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var userId = _userManager.GetUserId(User);

            if (User.IsInRole("Comerciante"))
            {
                var image = ShowImage(_context.Comerciante.Where(x => x.UserId == _userManager.GetUserId(User)).Select(x => x.logo).FirstOrDefault());
                return Json(image);
            }
            else if (User.IsInRole("Cliente"))
            {
                var image = ShowImage(_context.Cliente.Where(x => x.UserId == _userManager.GetUserId(User)).Select(x => x.Image).FirstOrDefault());
                return Json(image);
            }
            else
            {
                var image = ShowImage(_context.Cliente.Where(x => x.UserId == _userManager.GetUserId(User)).Select(x => x.Image).FirstOrDefault());
                return Json(image);
            }
        }

        /// <summary>
        /// Função GetSugestiveNames - Retorna uma lista de sugestões de nomes de produtos com base na entrada fornecida.
        /// </summary>
        /// <param name="input">A entrada para a qual as sugestões de nomes serão baseadas</param>
        /// <returns>Um objeto IActionResult contendo a lista de sugestões de nomes de produtos</returns>
        [HttpGet]
        public IActionResult GetSugestiveNames(string input)
        {
            var nameList = _context.Produto
                                      .Where(p => p.ProdEstado == Estado.Approved)
                                      .Select(m => m.Name)
                                      .Distinct()
                                      .ToList();

            var result = new List<string>();
            var blacklisted = new List<string> { "a", "o", "de", "com" };

            foreach (var name in nameList)
            {
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
                }
                else
                {
                    var suggestion = GetWordAfterInput(name, input, blacklisted);
                    if (suggestion != null)
                    {
                        result.Add(suggestion);
                    }
                }
            }
            return Json(result);
        }

        /// <summary>
        /// Função GetWordAfterInput - Retorna a palavra depois da entrada
        /// </summary>
        /// <param name="name">A string na qual procurar a palavra seguinte</param>
        /// <param name="input">A entrada para a qual encontrar a palavra seguinte</param>
        /// <param name="blockedWords">Uma lista de palavras bloqueadas a serem evitadas</param>
        /// <returns>A palavra seguinte após a entrada, evitando palavras bloqueadas, ou null se não for encontrada</returns>
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

        /// <summary>
        /// Função AddToList - é realizada quando o cliente pretende visualizar um produto 
        /// </summary>
        /// <param name="id">id do produto pretendido</param>
        /// <returns>View com os detalhes do produto em questão</returns>
        public async Task<ActionResult> AddToList(int id)
        {

                var model2 = await _context.Produto
          .Where(produto => produto.Id == id && produto.ProdEstado == Estado.Approved && produto.ProdutoLojas
              .Any(produtoLoja => _context.Loja
                  .Any(loja => loja.UserId == _context.Comerciante
                      .Where(comerciante => comerciante.UserId == produtoLoja.Loja.UserId)
                      .Select(comerciante => comerciante.UserId)
                      .FirstOrDefault())))
          .Include(produto => produto.ProdutoLojas)
              .ThenInclude(produtoLoja => produtoLoja.Loja)
          .ToListAsync();

            var userId = _userManager.GetUserId(User);
            var cliente = _context.Cliente.FirstOrDefault(c => c.UserId == userId);

            var produto = model2.Select(x => x.Id).FirstOrDefault();

            ViewData["Listas"] = _context.ListaProdutos
                .Where(l => l.ClienteId == cliente.Id.ToString());

            ViewData["Lojas"] = _context.Loja
        .Join(_context.ProdutoLoja, loja => loja.Id, produtoLoja => produtoLoja.Loja.Id, (loja, produtoLoja) => new { Loja = loja, ProdutoLoja = produtoLoja })
        .Where(joined => joined.ProdutoLoja.Produto.Id == produto)
        .Select(joined => joined.Loja)
        .ToList();

            ViewData["ProdutosSemelhantes"] = SimilarProducts(produto);

            ViewData["Comerciante"] = _context.Comerciante;

            var pl = model2.SelectMany(x => x.ProdutoLojas).FirstOrDefault();
            if (pl != null)
            {
                ShowDiscount(pl.Id);
            }
            else
            {
                ViewBag.ErrorMessage = "Nenhum produto encontrado";
            }
            return View(model2);

        }

        /// <summary>
        /// Função AddProduct - realizada quando o cliente pretende adicionar um produto à sua lista
        /// </summary>
        /// <param name="id">id do produto</param>
        /// <param name="quantityValue">quantidade de produto</param>
        /// <param name="nome">nome da lista</param>
        /// <returns>View das listas do cliente</returns>
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


            return RedirectToAction("Index", "ListaProdutos");

        }

        /// <summary>
        /// Função SimilarProducts - seleciona um conjunto de produtos similares ao produto a ser visualizado 
        /// </summary>
        /// <param name="id">id do produto</param>
        /// <returns>Lista de produtos semelhantes</returns>
        //[HttpGet]
        public List<Produto> SimilarProducts(int? id)
        {
            var product = _context.Produto.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return null;
            }

            var searchWords = product.Name.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            var similarProducts = new List<Produto>();

            foreach (var word in searchWords)
            {
                var productsWithWord = _context.Produto
                    .Where(p => p.Name.Contains(word) && p.Id != id && p.ProdEstado == Estado.Approved)
                    .ToList();
                similarProducts.AddRange(productsWithWord);
            }

            similarProducts = similarProducts.Distinct().ToList();

            return similarProducts;

            //return Json(similarProducts);
        }

        /// <summary>
        /// Função ShowImage - Mostra a imagem
        /// </summary>
        /// <param name="image">Imagem</param>
        /// <returns>Uma imagem</returns>
        public string ShowImage(byte[]? image)
        {
            if (image == null)
            {
                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "userphoto_0.png");
                if (!System.IO.File.Exists(imagePath))
                    return null;

                return Convert.ToBase64String(System.IO.File.ReadAllBytes(imagePath));
            }

            return Convert.ToBase64String(image);
        }

        public IActionResult ShowDiscount(int id)
        {
            var produtoLoja = _context.ProdutoLoja.Select(p => p).FirstOrDefault(p => p.Id == id);
            if (produtoLoja != null && ProdutoHasDiscount(id))
            {
                var precoOriginal = produtoLoja.Price;
                var desconto = produtoLoja.Discount / 100;
                decimal precoExibicao;

                if (desconto > 0)
                {
                    var precoComDesconto = precoOriginal * (1 - desconto);
                    precoExibicao = (decimal)precoComDesconto;
                }
                else
                {
                    precoExibicao = (decimal)precoOriginal;
                }

                ViewBag.PrecoExibicao = precoExibicao;
            }
            return View();
        }

        private bool ProdutoHasDiscount(int id)
        {
            return _context.ProdutoLoja.Any(pl => pl.Id == id && pl.Discount > 0);
        }
    }
}