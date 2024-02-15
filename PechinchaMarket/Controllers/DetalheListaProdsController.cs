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
    public class DetalheListaProdsController : Controller
    {
        private readonly DBPechinchaMarketContext _context;

        public DetalheListaProdsController(DBPechinchaMarketContext context)
        {
            _context = context;
        }

        // GET: DetalheListaProds
        public async Task<IActionResult> Index()
        {
            return View(await _context.DetalheListaProd.ToListAsync());
        }

        // GET: DetalheListaProds/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalheListaProd = await _context.DetalheListaProd
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detalheListaProd == null)
            {
                return NotFound();
            }

            return View(detalheListaProd);
        }

        // GET: DetalheListaProds/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DetalheListaProds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,quantity,ListaProdutosId,ProdutoId")] DetalheListaProd detalheListaProd)
        {
            if (ModelState.IsValid)
            {
                detalheListaProd.Id = Guid.NewGuid();
                _context.Add(detalheListaProd);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(detalheListaProd);
        }

        // GET: DetalheListaProds/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalheListaProd = await _context.DetalheListaProd.FindAsync(id);
            if (detalheListaProd == null)
            {
                return NotFound();
            }
            return View(detalheListaProd);
        }

        // POST: DetalheListaProds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,quantity,ListaProdutosId,ProdutoId")] DetalheListaProd detalheListaProd)
        {
            if (id != detalheListaProd.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detalheListaProd);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetalheListaProdExists(detalheListaProd.Id))
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
            return View(detalheListaProd);
        }

        // GET: DetalheListaProds/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalheListaProd = await _context.DetalheListaProd
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detalheListaProd == null)
            {
                return NotFound();
            }

            return View(detalheListaProd);
        }

        // POST: DetalheListaProds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var detalheListaProd = await _context.DetalheListaProd.FindAsync(id);
            if (detalheListaProd != null)
            {
                _context.DetalheListaProd.Remove(detalheListaProd);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetalheListaProdExists(Guid id)
        {
            return _context.DetalheListaProd.Any(e => e.Id == id);
        }
    }
}
