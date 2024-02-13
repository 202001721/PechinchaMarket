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
    public class ListaProdutosController : Controller
    {
        private readonly DBPechinchaMarketContext _context;

        public ListaProdutosController(DBPechinchaMarketContext context)
        {
            _context = context;
        }

        // GET: ListaProdutos
        public async Task<IActionResult> Index()
        {
            return View(await _context.ListaProdutos.ToListAsync());
        }

        // GET: ListaProdutos/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listaProdutos = await _context.ListaProdutos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (listaProdutos == null)
            {
                return NotFound();
            }

            return View(listaProdutos);
        }

        // GET: ListaProdutos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ListaProdutos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,name,ClienteId,state")] ListaProdutos listaProdutos)
        {
            if (ModelState.IsValid)
            {
                listaProdutos.Id = Guid.NewGuid();
                _context.Add(listaProdutos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(listaProdutos);
        }

        // GET: ListaProdutos/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listaProdutos = await _context.ListaProdutos.FindAsync(id);
            if (listaProdutos == null)
            {
                return NotFound();
            }
            return View(listaProdutos);
        }

        // POST: ListaProdutos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,name,ClienteId,state")] ListaProdutos listaProdutos)
        {
            if (id != listaProdutos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(listaProdutos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ListaProdutosExists(listaProdutos.Id))
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
            return View(listaProdutos);
        }

        // GET: ListaProdutos/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listaProdutos = await _context.ListaProdutos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (listaProdutos == null)
            {
                return NotFound();
            }

            return View(listaProdutos);
        }

        // POST: ListaProdutos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var listaProdutos = await _context.ListaProdutos.FindAsync(id);
            if (listaProdutos != null)
            {
                _context.ListaProdutos.Remove(listaProdutos);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ListaProdutosExists(Guid id)
        {
            return _context.ListaProdutos.Any(e => e.Id == id);
        }
    }
}
