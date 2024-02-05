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
    public class ComerciantesController : Controller
    {
        private readonly DBPechinchaMarketContext _context;

        public ComerciantesController(DBPechinchaMarketContext context)
        {
            _context = context;
        }

        // GET: Comerciantes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Comerciante.ToListAsync());
        }
    

        // GET: Comerciantes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comerciante = await _context.Comerciante
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comerciante == null)
            {
                return NotFound();
            }

            return View(comerciante);
        }

        // GET: Comerciantes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Comerciantes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,contact,logo,document")] Comerciante comerciante)
        {
            if (ModelState.IsValid)
            {
                comerciante.Id = Guid.NewGuid();
                _context.Add(comerciante);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(comerciante);
        }

        // GET: Comerciantes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comerciante = await _context.Comerciante.FindAsync(id);
            if (comerciante == null)
            {
                return NotFound();
            }
            return View(comerciante);
        }

        // POST: Comerciantes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,UserId,contact,logo,document")] Comerciante comerciante)
        {
            if (id != comerciante.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comerciante);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComercianteExists(comerciante.Id))
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
            return View(comerciante);
        }

        // GET: Comerciantes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comerciante = await _context.Comerciante
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comerciante == null)
            {
                return NotFound();
            }

            return View(comerciante);
        }

        // POST: Comerciantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var comerciante = await _context.Comerciante.FindAsync(id);
            if (comerciante != null)
            {
                _context.Comerciante.Remove(comerciante);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        private bool ComercianteExists(Guid id)
        {
            return _context.Comerciante.Any(e => e.Id == id);
        }
    }
}
