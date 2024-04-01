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

namespace PechinchaMarket.Controllers
{
    public class ManagerController : Controller
    {
        private readonly DBPechinchaMarketContext _context;

        public ManagerController(DBPechinchaMarketContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Metódo para mostrar a lista de comerciantes não aprovados
        /// </summary>
        /// <returns>View da lista dos comerciantes que ainda não foram aprovados</returns>
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

        /// <summary>
        /// Metódo para mostrar a lista de produtos não aprovados
        /// </summary>
        /// <returns>View da lista de produtos que ainda não foram aprovados</returns>
        public async Task<ActionResult> NonAprovedProducts()
        {
            return View(await _context.Produto.ToListAsync());
        }

        /// <summary>
        /// Metódo para mostrar os detalhes de um comerciante
        /// </summary>
        /// <param name="id">id do comerciante</param>
        /// <returns>View com as informações do comerciante</returns>
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

        /// <summary>
        /// Metódo para mostrar os detalhes de um produto
        /// </summary>
        /// <param name="id">id do produto</param>
        /// <returns>View com as informações do produto</returns>
        public async Task<IActionResult> DetailsProduct(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = _context.Produto.Where(produto => produto.Id == id)
       .Join(_context.ProdutoLoja,
           produto => produto.Id,
           prodLoja => prodLoja.Produto.Id,
           (produto, prodLoja) => new Tuple<Produto, ProdutoLoja>(produto, prodLoja))
       .ToList();

            return View(model);
        }

        /// <summary>
        /// Metódo para aprovar um comerciante       
        /// </summary>
        /// <param name="id">id do comerciante</param>
        /// <returns>Redireciona para a view de comerciantes por aprovar</returns>
        [HttpPost, ActionName("Aprove")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AproveConfirmed(Guid? id)
        {
            var comerciante = await _context.Comerciante.FindAsync(id);
            var utilizador = await _context.Users.FirstOrDefaultAsync(m => m.Id == comerciante.UserId);
            if (comerciante != null)
            {
                comerciante.isApproved = true;
                if (utilizador != null)
                {
                    await SendEmailAsync(utilizador.Email, "Seu registo foi aceite",
              "Estamos felizes em informar que seu registo como comerciante na plataforma PechinchaMarket foi aceite");
                }
            }
            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(NonConfirmedList));

        }

        /// <summary>
        /// Metódo para aprovar um produto
        /// </summary>
        /// <param name="id">id do produto</param>
        /// <returns>Redireciona para a view de produtos por aprovar</returns>
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

        /// <summary>
        /// Metódo para reprovar um comerciante
        /// </summary>
        /// <param name="id">id do comerciante</param>
        /// <returns>Redireciona para a view de comerciantes por aprovar</returns>
        [HttpPost, ActionName("Reprove")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReproveConfirmed(Guid? id)
        {
            var comerciante = await _context.Comerciante.FindAsync(id);
            var utilizador = await _context.Users.FirstOrDefaultAsync(m => m.Id == comerciante.UserId);
            var utilizadorId = await _context.Users.FindAsync(utilizador.Id);

            if (comerciante != null)
            {
                await SendEmailAsync(utilizador.Email, "Seu registo foi negado",
              "Lamentamos informar que seu registo como comerciante na plataforma PechinchaMarket não foi aceite");
                _context.Comerciante.Remove(comerciante);
                _context.Users.Remove(utilizadorId);

            }
            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(NonConfirmedList));

        }

        //Mudar o estado do produto para reporvado
        /// <summary>
        /// Metódo para reprovar um produto
        /// </summary>
        /// <param name="id">id do produto</param>
        /// <returns>Redireciona para a view de produtos por aprovar</returns>
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

        /// <summary>
        /// Metódo para mostrar o logo do comerciante
        /// </summary>
        /// <param name="id">id do comerciante</param>
        /// <returns>Imagem do logo</returns>
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

        /// <summary>
        /// Metódo para mostrar a imagem do produto
        /// </summary>
        /// <param name="id">id do produto</param>
        /// <returns>Imagem do produto</returns>
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

        /// <summary>
        /// Metódo para mostrar o documento
        /// </summary>
        /// <param name="id">id do comerciante</param>
        /// <returns>Documento de vericidade do comerciante</returns>
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

        /// <summary>
        /// Metódo que envia um email para o comerciante 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        private async Task<bool> SendEmailAsync(string email, string subject, string body)
        {


            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtpClient = new SmtpClient();
                message.From = new MailAddress("pechinchamarket@outlook.com");
                message.To.Add(email);
                message.Subject = subject;
                message.IsBodyHtml = true;
                message.Body = body;

                smtpClient.Port = 587;
                smtpClient.Host = "smtp-mail.outlook.com";


                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("pechinchamarket@outlook.com", "Pechinchamos");
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Send(message);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}