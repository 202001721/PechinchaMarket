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

        public SearchController(DBPechinchaMarketContext context)
        {
            _context = context;
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
    }
}
