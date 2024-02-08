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

        
        public async Task<ActionResult> NonAprovedProducts(int id)
        {
            return View(await _context.Produto.ToListAsync());
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

        public async Task<IActionResult> DetailsProduct(int? id)
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

        public async Task<IActionResult> AproveProduct(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            var produto = await _context.Produto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produto == null) { return NotFound(); }


            return View(produto);

        }

        [HttpPost, ActionName("Approve")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AproveConfirmed(Guid? id)
        {
            var comerciante = await _context.Comerciante.FindAsync(id);
            if (comerciante != null)
            {
                comerciante.isApproved = true;
            }
            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(NonConfirmedList));

        }

        [HttpPost, ActionName("ApproveProduct")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AproveConfirmedProduct(Guid? id)
        {
            var produto = await _context.Produto.FindAsync(id);
            if (produto != null)
            {
                produto.ProdEstado = Estado.Approved;
            }
            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(NonAprovedProducts));

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

        [HttpPost, ActionName("ReproveProduto")]
        public async Task<IActionResult> ReproveProduct(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var produto = await _context.Produto
                .FirstOrDefaultAsync(m => m.Id == id);

            produto.ProdEstado = Estado.Repproved; 
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(NonAprovedProducts));
        }

    }
}
