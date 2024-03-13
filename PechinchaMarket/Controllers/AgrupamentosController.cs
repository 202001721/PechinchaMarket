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
    public class AgrupamentosController : Controller
    {
        private readonly DBPechinchaMarketContext _context;
        private readonly Microsoft.AspNetCore.Identity.UserManager<PechinchaMarketUser> _userManager;

        public AgrupamentosController(DBPechinchaMarketContext context,
                                      Microsoft.AspNetCore.Identity.UserManager<PechinchaMarketUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Agrupamentos
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            Cliente cliente = _context.Cliente.Where(x => x.UserId.Equals(userId)).FirstOrDefault();

            var agrupamentos = await _context.AgrupamentosMembro.Where(x => x.Cliente.Equals(cliente))
                                        .Include(x => x.Agrupamento)
                                             .ThenInclude(x => x.ListaProdutos).ToListAsync();
            
            var lists = _context.ListaProdutos.Where(x => x.ClienteId == cliente.Id.ToString()).ToList();
            ViewData["Lists"] = lists;

            return View(agrupamentos);
        }

        // GET: Agrupamentos/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agrupamento = await _context.Agrupamentos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (agrupamento == null)
            {
                return NotFound();
            }

            return View(agrupamento);
        }

        // GET: Agrupamentos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Agrupamentos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id")] Agrupamento agrupamento)
        {

            ModelState.Remove("Nome");
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                Cliente cliente = _context.Cliente.Where(x => x.UserId.Equals(userId)).FirstOrDefault();
                if (cliente != null) {
                    agrupamento.Id = Guid.NewGuid();
                    agrupamento.Nome = "Agrupamento " + (_context.AgrupamentosMembro.Where(x => x.Cliente.UserId.Equals(userId)).Select(x => x.Agrupamento).ToList().Count() + 1);
                    
                    var Codigos = _context.AgrupamentosMembro.Where(x => x.Cliente.UserId.Equals(userId)).Select(x => x.Agrupamento.Codigo).ToList();
                    long codigo = 0;
                    do
                    {
                        codigo = GenerateRandomNumber();
                    } while (Codigos.Contains(codigo)); 
                    
                    
                    agrupamento.Codigo = codigo;
                    AgrupamentoMembro agrupamentoMembro = new AgrupamentoMembro();
                    agrupamentoMembro.Agrupamento = agrupamento;
                    agrupamentoMembro.Cliente = cliente;

                    _context.Add(agrupamento);
                    _context.Add(agrupamentoMembro);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(agrupamento);
        }

        // GET: Agrupamentos/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agrupamento = await _context.Agrupamentos.FindAsync(id);
            if (agrupamento == null)
            {
                return NotFound();
            }
            return View(agrupamento);
        }

        // POST: Agrupamentos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Nome,Codigo")] Agrupamento agrupamento)
        {
            if (id != agrupamento.Id)
            {
                return NotFound();
            }

            ModelState.Remove("Codigo");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(agrupamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgrupamentoExists(agrupamento.Id))
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
            return View(agrupamento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditName(Guid id, [Bind("Id","Nome")] Agrupamento agrupamento) {
            var agrupamentos = await _context.AgrupamentosMembro
                                        .Include(x => x.Agrupamento)
                                             .ThenInclude(x => x.ListaProdutos).ToListAsync();

            if (id != agrupamento.Id)
            {
                return NotFound();
            }

            var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            ViewBag.ErrorMessages = errorMessages;

            if (ModelState.IsValid)
            {
                try
                {
                    var nome = agrupamento.Nome;
                    var agrupamento_context = _context.Agrupamentos.Single(a => a.Id == id);
                    agrupamento_context.Nome = nome;
                    //_context.Update(agrupamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgrupamentoExists(agrupamento.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                

                return View("Index", agrupamentos);
            }
            return View("Index", agrupamentos);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddList(Guid id, String listaId)
        {
            var userId = _userManager.GetUserId(User);
            Cliente cliente = _context.Cliente.Where(x => x.UserId.Equals(userId)).FirstOrDefault();

            var agrupamentos = await _context.AgrupamentosMembro.Where(x => x.Cliente.Equals(cliente))
                                        .Include(x => x.Agrupamento)
                                             .ThenInclude(x => x.ListaProdutos).ToListAsync();

            var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            ViewBag.ErrorMessages = errorMessages;

            var errors = ModelState.Values
         .SelectMany(v => v.Errors)
         .Select(e => e.ErrorMessage)
         .ToList();
            ModelState.Remove("ClienteId");
            ModelState.Remove("Name");
            if (ModelState.IsValid)
            { 
                try
                {
                    var agrupamento_context = _context.Agrupamentos.Single(a => a.Id == id);
                    var main = _context.ListaProdutos.ToList();
                    var listaprodutos_context = _context.ListaProdutos.Single(x => x.Id.ToString() == listaId);
                    agrupamento_context.ListaProdutos.Add(listaprodutos_context);
                    //_context.Update(agrupamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ListaProdutosExists(listaId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }


                return View("Index", agrupamentos);
            }
            return View("Index", agrupamentos);
        }

        // GET: Agrupamentos/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agrupamento = await _context.Agrupamentos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (agrupamento == null)
            {
                return NotFound();
            }

            return View(agrupamento);
        }

        // POST: Agrupamentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var agrupamento = await _context.Agrupamentos.FindAsync(id);
            if (agrupamento != null)
            {
                _context.Agrupamentos.Remove(agrupamento);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgrupamentoExists(Guid id)
        {
            return _context.Agrupamentos.Any(e => e.Id == id);
        }

        private bool ListaProdutosExists(String id)
        {
            return _context.ListaProdutos.Any(e => e.Id.ToString() == id);
        }

        private long GenerateRandomNumber() {
            Random random = new Random();

            // List to store the generated random numbers
            long result = 0;

            // Generate 16 random numbers
            for (int i = 0; i < 16; i++)
            {
                int randomNumber = random.Next(0, 10);

                result += randomNumber;
                result *= 10; 
            }

            return result;
        }
    }
}
