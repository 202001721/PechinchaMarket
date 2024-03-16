using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PechinchaMarket.Areas.Identity.Data;
using PechinchaMarket.Models;
using PechinchaMarket.Services;

namespace PechinchaMarket.Controllers
{
    public class ManagerController : Controller
    {
        private readonly DBPechinchaMarketContext _context;

        public ManagerController(DBPechinchaMarketContext context)
        {
            _context = context;
        }

        // GET: Comerciantes não aprovados
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

        // GET: Produtos não aprovados
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

            var model = _context.Comerciante.Where(comerciante => comerciante.Id == id)
   .Join(_context.Users,
       comerciante => comerciante.UserId,
       user => user.Id,
       (comerciante, user) => new Tuple<Comerciante, PechinchaMarketUser>(comerciante, user));

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // GET: Produtos/Details/5
        public async Task<IActionResult> DetailsProduct(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = _context.Produto.Where(produto => produto.Id == id)
       .Join(_context.ProdutoLoja,
           produto => produto.Id,
           prodLoja=> prodLoja.Produto.Id,
           (produto, prodLoja) => new Tuple<Produto, ProdutoLoja>(produto, prodLoja))
       .ToList();

            /*var produto = await _context.Produto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produto == null)
            {
                return NotFound();
            }*/

            return View(model);
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
        //Aprovar Porduto
        public async Task<IActionResult> ApproveProduct(int? id)
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

        [HttpPost, ActionName("Aprove")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AproveConfirmed(Guid? id)
        {
            var comerciante = await _context.Comerciante.FindAsync(id);
            if (comerciante != null)
            {
                var utilizador = await _context.Users.FirstOrDefaultAsync(m => m.Id == comerciante.UserId);
           
                comerciante.isApproved = true;
                if(utilizador != null) {
                    utilizador.EmailConfirmed = true;
                    EmailSender emailSender = new EmailSender();
                    await emailSender.SendEmail("Seu cadastro foi aceito",utilizador.Email,comerciante.Name, "Estamos felizes em informar que seu registo como comerciante na plataforma PechinchaMarket foi aceito");
                   
            
                }
            }
            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(NonConfirmedList));

        }

        //Mudar o estado do produto para aprovado
        [HttpPost, ActionName("ApproveProduct")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AproveConfirmedProduct(int? id)
        {
            var produto = await _context.Produto.FindAsync(id);
            if (produto != null)
            {
                produto.ProdEstado = 0;
            }
            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(NonAprovedProducts));

        }

        public async Task<IActionResult> Reprove(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var comerciante = await _context.Comerciante
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comerciante == null) { return NotFound(); }

            return View(comerciante);

        }

        [HttpPost, ActionName("Reprove")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReproveConfirmed(Guid? id)
        {
            var comerciante = await _context.Comerciante.FindAsync(id);
          

            if (comerciante != null)
            {
                var utilizador = await _context.Users.FirstOrDefaultAsync(m => m.Id == comerciante.UserId);
        
             
                if (utilizador != null)
                {
                    EmailSender emailSender = new EmailSender();
                    await emailSender.SendEmail("Seu cadastro foi recusado", utilizador.Email, comerciante.Name, "Lamentamos informar que seu registo como comerciante na plataforma PechinchaMarket não foi aceito");
                    _context.Comerciante.Remove(comerciante);
                    _context.Users.Remove(utilizador);
                }
            
                

            }
            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(NonConfirmedList));

        }

        //Reprovar Produto
        public async Task<IActionResult> ReproveProduct(int? id)
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

        //Mudar o estado do produto para reporvado
        [HttpPost, ActionName("ReproveProduct")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReproveConfirmedProduct(int? id)
        {
            var produto = await _context.Produto.FindAsync(id);
            if (produto != null)
            {
                produto.ProdEstado = (Estado?)1;
            }
            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(NonAprovedProducts));

        }

      public async Task<IActionResult> ShowLogo(Guid? id)
      {
            if (id == null)
            {
                return NotFound();
            }
            var comerciante = await _context.Comerciante
                .FirstOrDefaultAsync(m => m.Id == id);

            return File(comerciante.logo, "image/jpg");
      }
        //Mostrar imagem do produto
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
        public async Task<IActionResult> ShowDocument(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comerciante = await _context.Comerciante
                .FirstOrDefaultAsync(m => m.Id == id);

            return File(comerciante.document, "application/pdf");
        }
       
    }
}
