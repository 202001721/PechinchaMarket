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
            return View(await _context.Produto.ToListAsync());
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

        public async Task<IActionResult> Show(int? id)
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
            ViewData["Shops"] = _context.Loja;
            return View();
        }

        // POST: Produtos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Brand,Weight,Unidade,ProdCategoria")] Produto produto, IFormFile file, float[] price)
        {
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


        // GET: Produtos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = _context.Produto
            .Where(produto => produto.Id == id)
            .Join(_context.ProdutoLoja,
                temp => temp.Id,
                produtoLoja => produtoLoja.Produto.Id,
                (temp, produtoLoja) => new { Produto = temp, ProdutoLoja = produtoLoja })
            .Select(x => Tuple.Create(x.ProdutoLoja, x.Produto))
            .ToList();

            var produto = model.Select(x => x.Item2.Id).FirstOrDefault();
            var userId = _userManager.GetUserId(User);

            ViewData["Lojas"] = _context.Loja
      .Join(_context.ProdutoLoja, loja => loja.Id, produtoLoja => produtoLoja.Loja.Id, (loja, produtoLoja) => new { Loja = loja, ProdutoLoja = produtoLoja })
      .Where(joined => joined.ProdutoLoja.Produto.Id == produto)
      .Select(joined => joined.Loja)
      .ToList();

            return View(model);
        }

        // POST: Produtos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Brand,Image,Weight,Unidade,ProdEstado,ProdCategoria")] Produto produto, float[] price, float[] discount, IFormFile file, string duration)
        {
            if (id != produto.Id)
            {
                return NotFound();
            }

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
                        }
                    }
                    
                    // Atualizar as propriedades editáveis do produto
                    produtoToUpdate.Image = file != null ? produtoToUpdate.Image : produto.Image;

                    if (!string.IsNullOrEmpty(duration) && duration.Contains("-"))
                    {
                        var durationParts = duration.Split('-');
                        if (durationParts.Length >= 2 && DateTime.TryParse(durationParts[0].Trim(), out DateTime inicioPromocao) && DateTime.TryParse(durationParts[1].Trim(), out DateTime fimPromocao))
                        {

                            var userId = _userManager.GetUserId(User);
                            List<Loja> lojas = (from l in _context.Loja where l.UserId == userId select l).ToList();
                            if (!lojas.IsNullOrEmpty())
                            {

                                for (int i = 0; i < lojas.Count && i < produtoToUpdate.ProdutoLojas.Count; i++)
                                {
                                    var currentProdutoLoja = produtoToUpdate.ProdutoLojas[i];
                                    currentProdutoLoja.Price = price[i];
                                    currentProdutoLoja.Discount = discount[i];
                                    currentProdutoLoja.StartDiscount = inicioPromocao;
                                    currentProdutoLoja.EndDiscount = fimPromocao;
                                    _context.Update(currentProdutoLoja);
                                }
                            }
                            else
                            {
                                TempData["alertMessage"] = "Formato inválido para a duração";
                            }
                        }
                        else
                        {
                            TempData["alertMessage"] = "Formato inválido para a duração";
                        }

                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
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
