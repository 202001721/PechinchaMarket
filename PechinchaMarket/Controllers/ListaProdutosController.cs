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
        private readonly Microsoft.AspNetCore.Identity.UserManager<PechinchaMarketUser> _userManager;

        public ListaProdutosController(DBPechinchaMarketContext context, Microsoft.AspNetCore.Identity.UserManager<PechinchaMarketUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: ListaProdutos
        public async Task<IActionResult> Index()
        {
            var clienteId = (from q in _context.Cliente where q.UserId == _userManager.GetUserId(User) select q).FirstOrDefault().Id.ToString();

            var lista = from l in _context.ListaProdutos where l.ClienteId == clienteId select l;
            return View(lista);
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
            ModelState.Remove("ClienteId");
            ModelState.Remove("state");
            if (ModelState.IsValid)
            {
                listaProdutos.Id = Guid.NewGuid();
                var clienteId = (from q in _context.Cliente where q.UserId == _userManager.GetUserId(User) select q).FirstOrDefault()?.Id.ToString();

                listaProdutos.ClienteId = clienteId;
                listaProdutos.state = EstadoProdutoCompra.PorComprar;
                listaProdutos.detalheListaProds = new List<DetalheListaProd>();
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

            ListaProdutos? listaProdutos = await _context.ListaProdutos
                .Include(l=>l.detalheListaProds)
                    .ThenInclude(d => d.ProdutoLoja)
                        .ThenInclude(p => p.Produto)
                .Include(l=>l.detalheListaProds)
                    .ThenInclude(d=>d.ProdutoLoja)
                        .ThenInclude(l => l.Loja)
                .FirstOrDefaultAsync(d => d.Id == id);
      
            if (listaProdutos == null)
            {
                return NotFound();
            }

            ViewData["Comerciante"] = _context.Comerciante;
            return View(listaProdutos);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeName(Guid id, string name)
        {
            var l = _context.ListaProdutos.Single(b => b.Id == id);
            l.name = name;
            await _context.SaveChangesAsync();
            return View("Edit",l);
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
