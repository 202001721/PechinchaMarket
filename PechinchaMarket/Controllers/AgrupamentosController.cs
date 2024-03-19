using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
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
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AgrupamentosController(DBPechinchaMarketContext context,
                                      Microsoft.AspNetCore.Identity.UserManager<PechinchaMarketUser> userManager,
                                       IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Agrupamentos
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            Cliente cliente = _context.Cliente.Where(x => x.UserId.Equals(userId)).FirstOrDefault();

            var model = await _context.AgrupamentosMembro.Where(x => x.Cliente.Equals(cliente))
                                        .Include(x => x.Agrupamento)
                                             .ThenInclude(x => x.ListaProdutos).ToListAsync();

            var lists = _context.ListaProdutos.Where(x => x.ClienteId == cliente.Id.ToString()).ToList();
            ViewData["Lists"] = lists;
            var agrupamentos = _context.AgrupamentosMembro.Where(x => x.Cliente.Equals(cliente)).Select(x => x.Agrupamento).ToList(); //Agrupamentos que o cliente pretence
            var members = _context.AgrupamentosMembro.Where(x => agrupamentos.Contains(x.Agrupamento)).ToList(); //Membros dos Agrupamentos que o cliente pretence
            ViewData["members"] = members;

            ViewData["Clients"] = _context.Cliente.Where(x => !x.UserId.Equals(cliente.UserId)).ToList(); //Todos os Clientes menos eu (O cliente logado)

            return View(model);
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
            var userId = _userManager.GetUserId(User);
            Cliente cliente = _context.Cliente.Where(x => x.UserId.Equals(userId)).FirstOrDefault();

            var agrupamentos = await _context.AgrupamentosMembro.Where(x => x.Cliente.Equals(cliente))
                                        .Include(x => x.Agrupamento)
                                             .ThenInclude(x => x.ListaProdutos).ToListAsync();

            var lists = _context.ListaProdutos.Where(x => x.ClienteId == cliente.Id.ToString()).ToList();
            ViewData["Lists"] = lists;

            ModelState.Remove("Nome");
            if (ModelState.IsValid)
            {
                if (cliente != null)
                {
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
                    agrupamentoMembro.Privilegio = NivelPrivilegio.Admin;

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
            var userId = _userManager.GetUserId(User);
            Cliente cliente = _context.Cliente.Where(x => x.UserId.Equals(userId)).FirstOrDefault();

            var agrupamentos = await _context.AgrupamentosMembro.Where(x => x.Cliente.Equals(cliente))
                                        .Include(x => x.Agrupamento)
                                             .ThenInclude(x => x.ListaProdutos).ToListAsync();

            var lists = _context.ListaProdutos.Where(x => x.ClienteId == cliente.Id.ToString()).ToList();
            ViewData["Lists"] = lists;

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
        public async Task<IActionResult> EditName(Guid id, [Bind("Id", "Nome")] Agrupamento agrupamento)
        {
            var userId = _userManager.GetUserId(User);
            Cliente cliente = _context.Cliente.Where(x => x.UserId.Equals(userId)).FirstOrDefault();
            var agrupamentos = await _context.AgrupamentosMembro.Where(x => x.Cliente.Equals(cliente))
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


                return RedirectToAction("Index", new { model = agrupamentos });
            }
            return RedirectToAction("Index", new { model = agrupamentos });
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


                return RedirectToAction("Index", new { model = agrupamentos });
            }
            return RedirectToAction("Index", new { model = agrupamentos });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMemberLeitor(Guid id, Guid clienteId)
        {
            var userId = _userManager.GetUserId(User);
            Cliente cliente = _context.Cliente.Where(x => x.UserId.Equals(userId)).FirstOrDefault();

            var agrupamentos = await _context.AgrupamentosMembro.Where(x => x.Cliente.Equals(cliente))
                                        .Include(x => x.Agrupamento)
                                             .ThenInclude(x => x.ListaProdutos).ToListAsync();

            var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            ViewBag.ErrorMessages = errorMessages;

            ModelState.Remove("ClienteId");
            ModelState.Remove("Name");
            if (ModelState.IsValid)
            {
                try
                {
                    var agrupamento_context = _context.Agrupamentos.Single(a => a.Id == id);
                    var main = _context.ListaProdutos.ToList();
                    var cliente_context = _context.Cliente.Single(x => x.Id == clienteId);

                    _context.AgrupamentosMembro.Add(
                        new AgrupamentoMembro
                        {
                            Agrupamento = agrupamento_context,
                            Cliente = cliente_context,
                            Privilegio = NivelPrivilegio.Leitor
                        });

                    //_context.Update(agrupamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgrupamentoMembroExists(clienteId, id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }


                return RedirectToAction("Index", new { model = agrupamentos });
            }
            return RedirectToAction("Index", new { model = agrupamentos });
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

        private bool AgrupamentoMembroExists(Guid clienteId, Guid agrupamentoId)
        {
            return _context.AgrupamentosMembro.Any(e => e.Cliente.Id == clienteId && e.Agrupamento.Id == agrupamentoId);
        }

        private long GenerateRandomNumber()
        {
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


        public async Task<IActionResult> GetPerfilImage(Guid id)
        {
            var image = _context.Cliente.Where(x => x.Id == id).Select(x => x.Image).FirstOrDefault();

            if (image == null)
            {
                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "userphoto_0.png");
                if (!System.IO.File.Exists(imagePath))
                    return NotFound();

                return File(System.IO.File.ReadAllBytes(imagePath), "image/jpg");
            }

            return File(image, "image/jpg");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EnterWithCode(long codigo)
        {
            var userId = _userManager.GetUserId(User);

            if (await _context.AgrupamentosMembro.AnyAsync(x => x.Agrupamento.Codigo == codigo && x.Cliente.UserId == userId))
            {
                return RedirectToAction("Index");
            }
            else
            {
                //procura o agrupamento e coloca o membro como leitor desse grupo
                var agrupamento = await _context.Agrupamentos.FirstOrDefaultAsync(x => x.Codigo == codigo);
                var cliente = _context.Cliente.Where(x => x.UserId == userId).FirstOrDefault();
                if (agrupamento != null)
                {
                    var novoMembro = new AgrupamentoMembro
                    {
                        Agrupamento = agrupamento,
                        Cliente = cliente,
                        Privilegio = NivelPrivilegio.Leitor
                    };
                    _context.AgrupamentosMembro.Add(novoMembro);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound();
                }
            }
            return View();
        }
    }
}
