using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
            var produtos = _context.Produto
             .Where(p => p.ProdEstado == Estado.Approved).Join(_context.ProdutoLoja,
             produto => produto.Id,
             loja => loja.Id,
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

        public async Task<ActionResult> ProductsInAnalysis()
        {
            var produtos = _context.Produto
           .Where(p => p.ProdEstado == Estado.InAnalysis).Join(_context.ProdutoLoja,
           produto => produto.Id,
           loja => loja.Id,
           (produto, loja) => new Tuple<Produto, ProdutoLoja>(produto, loja)).ToList();

            return View(produtos);
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
            ViewData["Shops"] = _context.Loja.Where(l => l.UserId == _userManager.GetUserId(User));
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

            var produto = await _context.Produto.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }
            return View(produto);
        }

        // POST: Produtos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Brand,Image,Weight,Unidade,ProdEstado,ProdCategoria")] Produto produto)
        {
            if (id != produto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produto);
                    await _context.SaveChangesAsync();
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
                return RedirectToAction(nameof(Index));
            }
            return View(produto);
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
