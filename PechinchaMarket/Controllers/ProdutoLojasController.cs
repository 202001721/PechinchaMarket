using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PechinchaMarket.Areas.Identity.Data;
using PechinchaMarket.Models;

namespace PechinchaMarket.Controllers
{
    public class ProdutoLojasController : Controller
    {
        private readonly DBPechinchaMarketContext _context;

        public ProdutoLojasController(DBPechinchaMarketContext context)
        {
            _context = context;
        }

        // GET: ProdutoLojas
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProdutoLoja.ToListAsync());
        }

        // GET: ProdutoLojas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produtoLoja = await _context.ProdutoLoja
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produtoLoja == null)
            {
                return NotFound();
            }

            return View(produtoLoja);
        }

        // GET: ProdutoLojas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProdutoLojas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Price,Discount,,StartDiscount,EndDiscount")] ProdutoLoja produtoLoja)
        {
            if (ModelState.IsValid)
            {
                _context.Add(produtoLoja);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(produtoLoja);
        }

        // GET: ProdutoLojas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produtoLoja = await _context.ProdutoLoja.FindAsync(id);
            if (produtoLoja == null)
            {
                return NotFound();
            }
            return View(produtoLoja);
        }

        // POST: ProdutoLojas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Price,Discount,StartDiscount,EndDiscount")] ProdutoLoja produtoLoja)
        {
            if (id != produtoLoja.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produtoLoja);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoLojaExists(produtoLoja.Id))
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
            return View(produtoLoja);
        }

        // GET: ProdutoLojas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produtoLoja = await _context.ProdutoLoja
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produtoLoja == null)
            {
                return NotFound();
            }

            return View(produtoLoja);
        }

        // POST: ProdutoLojas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produtoLoja = await _context.ProdutoLoja.FindAsync(id);
            if (produtoLoja != null)
            {
                _context.ProdutoLoja.Remove(produtoLoja);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdutoLojaExists(int id)
        {
            return _context.ProdutoLoja.Any(e => e.Id == id);
        }
    }
}
