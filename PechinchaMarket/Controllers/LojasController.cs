using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PechinchaMarket.Areas.Identity.Data;
using PechinchaMarket.Models;

namespace PechinchaMarket.Controllers
{
    public class LojasController : Controller
    {
        private readonly DBPechinchaMarketContext _context;
        private readonly UserManager<PechinchaMarketUser> _userManager;

        public LojasController(DBPechinchaMarketContext context, UserManager<PechinchaMarketUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<ActionResult> LojasUserList()
        {

                var model = _context.Loja
          .Join(_context.Users,
              loja => loja.UserId,
              user => user.Id,
              (loja, user) => new Tuple<Loja, PechinchaMarketUser>(loja, user))
          .ToList()
          .Select(t => new { Loja = t.Item1, User = t.Item2 })
          .ToList();

            return View(model.Select(t => new { Loja = t.Loja, User = t.User }));
        }

        // GET: Lojas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Loja.ToListAsync());
        }

        // GET: Lojas/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loja = await _context.Loja
                .FirstOrDefaultAsync(m => m.Id == id);
            if (loja == null)
            {
                return NotFound();
            }

            return View(loja);
        }

        // GET: Lojas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lojas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,adress,OpeningTime,ClosingTime,UserId")] Loja loja)
        {
            if (ModelState.IsValid)
            {
                loja.Id = Guid.NewGuid();
                string userId = _userManager.GetUserId(User);
                if(userId != loja.UserId.ToString())
                {
                    return RedirectToAction("Error");
                }
                _context.Add(loja);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(loja);
        }

        // GET: Lojas/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loja = await _context.Loja.FindAsync(id);
            if (loja == null)
            {
                return NotFound();
            }
            return View(loja);
        }

        // POST: Lojas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,adress,OpeningTime,ClosingTime,UserId")] Loja loja)
        {
            if (id != loja.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loja);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LojaExists(loja.Id))
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
            return View(loja);
        }

        // GET: Lojas/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loja = await _context.Loja
                .FirstOrDefaultAsync(m => m.Id == id);
            if (loja == null)
            {
                return NotFound();
            }

            return View(loja);
        }

        // POST: Lojas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var loja = await _context.Loja.FindAsync(id);
            if (loja != null)
            {
                _context.Loja.Remove(loja);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LojaExists(Guid id)
        {
            return _context.Loja.Any(e => e.Id == id);
        }
    }
}
