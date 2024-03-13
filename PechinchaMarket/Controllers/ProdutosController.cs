using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PechinchaMarket.Areas.Identity.Data;
using PechinchaMarket.Models;

namespace PechinchaMarket.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly DBPechinchaMarketContext _context;
        private readonly Microsoft.AspNetCore.Identity.UserManager<PechinchaMarketUser> _userManager;

        public ProdutosController(DBPechinchaMarketContext context, Microsoft.AspNetCore.Identity.UserManager<PechinchaMarketUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        // GET: Produtos
        public async Task<IActionResult> Index()
        {
            var produtos = _context.Produto.Join(_context.ProdutoLoja,
             produto => produto.Id,
             loja => loja.Produto.Id,
             (produto, loja) => new Tuple<Produto, ProdutoLoja>(produto, loja)).ToList();

            return View(produtos);
        }

        // GET: Produtos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
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

        // GET: Produtos/Create
        public IActionResult Create()
        {
            ViewData["Shops"] = _context.Loja.Where(l => l.UserId == _userManager.GetUserId(User));
            return View();
        }

        /// <summary>
        /// Função Create - Utilizada quando o comerciante pretende criar um novo produto
        /// </summary>
        /// <param name="produto">novo produto a adicionar à base de dados</param>
        /// <param name="file">imagem do produto</param>
        /// <param name="price">conjunto de preços para definir estes nos ProdutoLojas definidos</param>
        /// <returns>View da lista dos produtos do comerciante</returns>
        // POST: Produtos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Brand,Image,Weight,Unidade,ProdCategoria")] Produto produto, IFormFile file, float[] price)
        {
            ModelState.Remove("Image");
            if (ModelState.IsValid)
            {
                var memoryStreamImg = new MemoryStream();

                await file.CopyToAsync(memoryStreamImg);

                var userId = _userManager.GetUserId(User);
                List<Loja> lojas = (from l in _context.Loja where l.UserId == userId select l).ToList();
                if (!lojas.IsNullOrEmpty())
                {
                    List<ProdutoLoja> ProdL = new List<ProdutoLoja>();
                    
                    for(int i = 0; i< lojas.Count;i++)
                    {
                        int pi = i+1;
                        var p = new ProdutoLoja { Price = price[pi], Loja = lojas[i] };
                        ProdL.Add(p);
                    }
                    
                    

                    produto.ProdEstado = Estado.InAnalysis;
                    produto.Image = memoryStreamImg.ToArray();
                    produto.ProdutoLojas = ProdL;

                    _context.Add(produto);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["alertMessage"] = "Nao existe nenhuma loja";
                    return View(produto);
                }
                
            }
            return View(produto);
        }

        /// <summary>
        /// Função Edit - utilizada quando o comerciante pretende editar um produto
        /// </summary>
        /// <param name="id">id do produto</param>
        /// <returns>View com opções de edição do produto</returns>
        // GET: Produtos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = _context.Produto.Include(x => x.ProdutoLojas).ThenInclude(x => x.Loja).Where(x => x.Id == id).FirstOrDefault();

            return View(model);
        }


        /// <summary>
        /// Função Edit httpPost - utilizada quando o comerciante realiza submit do formulário de edição de um produto
        /// </summary>
        /// <param name="id">id do produto</param>
        /// <param name="produto">informação do produto a ser atualizada</param>
        /// <param name="price">novo preço do produto</param>
        /// <param name="discount">desconto do produto</param>
        /// <param name="file">imagem do produto</param>
        /// <param name="duration">duração do desconto</param>
        /// <returns>View da lista de produtos</returns>
        // POST: Produtos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Brand,Image,Weight,Unidade,ProdEstado,ProdCategoria")] Produto produto, float[] price, float[] discount, IFormFile file, string[] duration)
        {
            if (id != produto.Id)
            {
                return NotFound();
            }

            ModelState.Remove("file");
            ModelState.Remove("discount");
            ModelState.Remove("duration");
            ModelState.Remove("Image");

            if (ModelState.IsValid)
            {
                try
                {
                    var produtoToUpdate = await _context.Produto
                        .Include(p => p.ProdutoLojas)
                        .FirstOrDefaultAsync(p => p.Id == id);

                    if (produtoToUpdate == null)
                    {
                        return NotFound();
                    }

                    if (file != null)
                    {
                        // Atualizar a imagem do produto, se for o caso
                        using (var memoryStream = new MemoryStream())
                        {
                            await file.CopyToAsync(memoryStream);
                            produtoToUpdate.Image = memoryStream.ToArray();
                            _context.Update(produtoToUpdate);
                        }
                    }

                    // Atualizar o campo Price
                    if (price != null)
                    {
                        for (int i = 0; i < produtoToUpdate.ProdutoLojas.Count && i < price.Length; i++)
                        {
                            var currentProdutoLoja = produtoToUpdate.ProdutoLojas[i];
                            currentProdutoLoja.Price = price[i];
                            _context.Update(currentProdutoLoja);
                        }
                    }

                    // Atualizar o campo Discount e Duration
                    if (discount != null && duration != null)
                    {
                        for (int i = 0; i < produtoToUpdate.ProdutoLojas.Count && i < discount.Length && i < duration.Length; i++)
                        {
                            var currentProdutoLoja = produtoToUpdate.ProdutoLojas[i];
                            currentProdutoLoja.Discount = discount[i];

                            if (!string.IsNullOrEmpty(duration[i]) && duration[i].Contains("-"))
                            {
                                var durationParts = duration[i].Split('-');
                                if (durationParts.Length >= 2 && DateTime.TryParse(durationParts[0].Trim(), out DateTime inicioPromocao) && DateTime.TryParse(durationParts[1].Trim(), out DateTime fimPromocao))
                                {
                                    // Atualizar os campos StartDiscount e EndDiscount
                                    currentProdutoLoja.StartDiscount = inicioPromocao;
                                    currentProdutoLoja.EndDiscount = fimPromocao;
                                    _context.Update(currentProdutoLoja);
                                }
                                else
                                {
                                    TempData["alertMessage"] = "Formato inválido para a duração";
                                }
                            }
                            else if (string.IsNullOrEmpty(duration[i]) && discount[i] > 0)
                            {
                                TempData["alertMessage"] = "É necessário especificar a duração para aplicar um desconto.";
                            }

                            _context.Update(currentProdutoLoja);
                        }
                    }

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoExists(produto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Produtos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // POST: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produto = await _context.Produto.FindAsync(id);
            if (produto != null)
            {
                _context.Produto.Remove(produto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdutoExists(int id)
        {
            return _context.Produto.Any(e => e.Id == id);
        }
        
    }
}
