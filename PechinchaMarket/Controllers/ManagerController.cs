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
    public class ManagerController : Controller
    {
        private readonly DBPechinchaMarketContext _context;

        public ManagerController(DBPechinchaMarketContext context)
        {
            _context = context;
        }

        
        public async Task<ActionResult> NonConfirmedList()
        {
            
            var model = _context.Comerciante
       .Join(_context.Users,
           comerciante => comerciante.UserId,
           user => user.Id,
           (comerciante, user) => new Tuple<Comerciante, PechinchaMarketUser>(comerciante, user))
       .ToList();

            return View(model);
        }

        // GET: Comerciantes/Details/5
        public async Task<IActionResult> DetailsComerciante(Guid? id)
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
        
        public async Task<IActionResult> Aprove(Guid? id)
        {
           
            if (id == null)
            {
                return NotFound();
            }
            var comerciante = await _context.Comerciante
                .FirstOrDefaultAsync(m => m.Id == id);
            if(comerciante == null) { return NotFound(); }
            

            return View(comerciante);

        }

        [HttpPost, ActionName("Aprove")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AproveConfirmed(Guid? id)
        {
            var comerciante = await _context.Comerciante.FindAsync(id);
            if (comerciante != null)
            {
                comerciante.isApproved = true;
                // 
            }
            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(NonConfirmedList));

        }
        [HttpPost, ActionName("Reprove")]
        public async Task<IActionResult> ReproveComerciante(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var comerciante = await _context.Comerciante
                .FirstOrDefaultAsync(m => m.Id == id);
            
            comerciante.isApproved = true; // not working
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(NonConfirmedList));
        }


      
    }
}
