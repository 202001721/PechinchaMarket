using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aspose.Pdf;
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

        /// <summary>
        /// Função Index - é utilizada quando o cliente pretende visualizar os seus agrupamentos no seu perfil
        /// </summary>
        /// <returns>View com as opções e informações dos agrupamentos do utilizador</returns>
        // GET: Agrupamentos
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Redirect("/Identity/Account/Login");
            }

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

        /// <summary>
        /// Função Create - utilizada quando o cliente prentende criar um novo agrupamento
        /// </summary>
        /// <returns>View com as opções e informações dos agrupamentos do utilizador atualizado</returns>
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

        /// <summary>
        /// Função EditName - utilizada quando o utilizador pretende mudar o nome de um agrupamento
        /// </summary>
        /// <param name="id">id do agrupamento respetivo</param>
        /// <param name="agrupamento">novo agrupamento que vai atualizar a base de dados</param>
        /// <returns>View com as opções e informações dos agrupamentos do utilizador atualizada</returns>
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

        /// <summary>
        /// Função AddList - utilizada quando o utilizador pretende adicionar uma lista ao agrupamento
        /// </summary>
        /// <param name="id">id do agrupamento em questão</param>
        /// <param name="listaId">id da lista a ser adicionada</param>
        /// <returns>View com as opções e informações dos agrupamentos do utilizador atualizada</returns>
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
                    var agrupamento_context = _context.Agrupamentos.Where(a => a.Id == id).FirstOrDefault();
                    var main = _context.ListaProdutos.ToList();
                    var listaprodutos_context = main.Where(x => x.Id.ToString().Equals(listaId)).FirstOrDefault();
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

        /// <summary>
        /// Função AddMemberLeitor - usada quando o cliente administrador do agrupamento quer adicionar um membro novo ao agrupamento
        /// </summary>
        /// <param name="id">id do agrupamento em questão</param>
        /// <param name="clienteId">id do cliente a ser adicionado ao agrupamento</param>
        /// <returns>View com as opções e informações dos agrupamentos do utilizador atualizado</returns>
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
                    var agrupamento_context = _context.Agrupamentos.Where(a => a.Id == id).FirstOrDefault();
                    var main = _context.ListaProdutos.ToList();
                    var cliente_context = _context.Cliente.Where(x => x.Id == clienteId).FirstOrDefault();

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
        /// <summary>
        /// Função RemoveMember - Método que recebe o id do agrupamento e do membro a ser eliminado deste mesmo agrupamento
        /// </summary>
        /// <param name="id">Id do agrupamento a ser gerido</param>
        /// <param name="clienteId">membro do agrupamento a ser removidos</param>
        /// <returns>View com as opções e informações dos agrupamentos do utilizador atualizada</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveMember(Guid id, Guid clienteId)
        {
            var userId = _userManager.GetUserId(User);
            Cliente cliente = _context.Cliente.FirstOrDefault(x => x.UserId == userId);

            if (cliente == null)
            {
                return RedirectToAction("Index");
            }

            var agrupamentoMembro = await _context.AgrupamentosMembro
                .Include(am => am.Agrupamento)
                .FirstOrDefaultAsync(am => am.Agrupamento.Id == id && am.Cliente.Id == clienteId);

            if (agrupamentoMembro == null)
            {
                return RedirectToAction("Index");
            }

            try
            {
                _context.AgrupamentosMembro.Remove(agrupamentoMembro);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AgrupamentoMembroExists(clienteId, id))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction("Index"); 
        }
        /// <summary>
        /// Função RemoveMembers - Método que utiliza do método de remover membros para remover uma lista de membros do agrupamento
        /// </summary>
        /// <param name="id"> Id do agrupamento a ser gerido</param>
        /// <param name="members"> lista de membros do agrupamento a serem removidos</param>
        /// <returns>View com as opções e informações dos agrupamentos do utilizador atualizada</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveMembers(Guid id, List<Guid> members)
        {
            foreach (var member in members)
            {
               await RemoveMember(id, member);
            }
            return   RedirectToAction("Index");
        }

        /// <summary>
        /// Função RemoveList - utilizada para remover uma lista do agrupamento
        /// </summary>
        /// <param name="id">id do agrupamento em questão</param>
        /// <param name="listaId">id da lista a ser removida</param>
        /// <returns>View com as opções e informações dos agrupamentos do utilizador atualizada</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveList(Guid id, Guid listaId)
        {
            var userId = _userManager.GetUserId(User);
            Cliente cliente = _context.Cliente.FirstOrDefault(x => x.UserId.Equals(userId));

            if (cliente == null)
            {
                return RedirectToAction("Index");
            }

            var agrupamentos = await _context.AgrupamentosMembro
                .Where(x => x.Cliente.Equals(cliente))
                .Include(x => x.Agrupamento)
                .ThenInclude(x => x.ListaProdutos)
                .ToListAsync();

            var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            ViewBag.ErrorMessages = errorMessages;

            ModelState.Remove("ClienteId");
            ModelState.Remove("Name");

            if (ModelState.IsValid)
            {
                try
                {
                    var agrupamentoContext = _context.Agrupamentos.Single(a => a.Id == id);
                    var listaprodutosContext = _context.ListaProdutos.Single(x => x.Id == listaId);

                    agrupamentoContext.ListaProdutos.Remove(listaprodutosContext);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ListaProdutosExists(listaId.ToString()))
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

        /// <summary>
        /// Função RemoveLists - utilizada para todas as listas do agrupamento
        /// </summary>
        /// <param name="id">id do agrupamento em questão</param>
        /// <param name="listasId">lista de ids das listas a serem eleminadas</param>
        /// <returns>View com as opções e informações dos agrupamentos do utilizador atualizada</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveLists(Guid id, List<Guid> listasId)
        {
            foreach (var lista in listasId)
            {
                await RemoveList(id, lista);
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Função ChangePermissions - utilizada quando o utilizador pretende mudar os privilégios de um membro do agrupamento
        /// </summary>
        /// <param name="id">id do agrupamento em questão</param>
        /// <param name="editPermissions">lista de permissões repetivas a cada membro</param>
        /// <returns>View com as opções e informações dos agrupamentos do utilizador atualizado</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePermissions(Guid id, string[] editPermissions)
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
                    var membros = _context.AgrupamentosMembro.Where(x => x.Agrupamento.Id == id).ToArray();

                    for(int i = 1 ; i < membros.Length; i++)
                    {

                        int iForPermission = i -1 ;

                        string[] privAndId = editPermissions[iForPermission].Split("_");

                        if (privAndId[1] == id.ToString())
                        {
                            NivelPrivilegio nivelPrivilegio;
                            Enum.TryParse(privAndId[0], out nivelPrivilegio);
                            membros[i].Privilegio = nivelPrivilegio;
                        }
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
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

        /// <summary>
        /// Função GenerateRandomNumber - cria um numero aleatório a ser usado para o código 
        /// </summary>
        /// <returns>numero aleatório de 16 digitos</returns>
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

        /// <summary>
        /// Função GetPerfilImage - usada para mostrar a imagem do perfil
        /// </summary>
        /// <param name="id">id do cliente que está logged in</param>
        /// <returns>ficheiro com a imagem pretendida</returns>
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

        /// <summary>
        /// Função EnterWithCode - utilizada quando o cliente insere um codigo de um agrupamento para se juntar
        /// </summary>
        /// <param name="codigo">codigo do agrupamento inserido</param>
        /// <returns>View com as opções e informações dos agrupamentos do utilizador atualizada</returns>
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
        }
    }
}
