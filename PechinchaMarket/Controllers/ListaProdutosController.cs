using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PechinchaMarket.Areas.Identity.Data;
using PechinchaMarket.Models;
using System.Data;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Aspose.Pdf.Text;
using Aspose.Pdf.Operators;

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

            // var lista = from l in _context.ListaProdutos where l.ClienteId == clienteId select l;
           

            var produtos = _context.Produto
              .Where(p => p.ProdEstado == Estado.Approved)
                  .Include(p => p.ProdutoLojas)
                      .ThenInclude(p => p.Loja).ToList();



            return View(produtos);
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

            ListaProdutos? listaProdutos = await _context.ListaProdutos
                .Include(l => l.detalheListaProds)
                    .ThenInclude(d => d.ProdutoLoja)
                        .ThenInclude(p => p.Produto)
                .Include(l => l.detalheListaProds)
                    .ThenInclude(d => d.ProdutoLoja)
                        .ThenInclude(l => l.Loja)
                .FirstOrDefaultAsync(d => d.Id == id);

            ViewData["Comerciante"] = _context.Comerciante;
            return View("Edit",listaProdutos);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteMany(Guid id, Guid[] deletes)
        {
            ListaProdutos? listaProdutos = await _context.ListaProdutos
                .Include(l => l.detalheListaProds)
                    .ThenInclude(d => d.ProdutoLoja)
                        .ThenInclude(p => p.Produto)
                .Include(l => l.detalheListaProds)
                    .ThenInclude(d => d.ProdutoLoja)
                        .ThenInclude(l => l.Loja)
                .FirstOrDefaultAsync(d => d.Id == id);

            List<DetalheListaProd> deletesObj= new List<DetalheListaProd>();
            foreach (var d in deletes)
            {
                deletesObj.Add(_context.DetalheListaProd.Single(l => l.Id == d));
            }

            foreach (var d in deletesObj)
            {
                listaProdutos.detalheListaProds.Remove(d);
            }

            await _context.SaveChangesAsync();

            ViewData["Comerciante"] = _context.Comerciante;
            return View("Edit", listaProdutos);
        }

        //Syncfusion.Pdf.AspNet.Mvc5
        public FileContentResult CreateDocument(Guid id, string conteudo, string ficheiro)
        {
            ListaProdutos? listaProdutos = _context.ListaProdutos
                .Include(l => l.detalheListaProds)
                    .ThenInclude(d => d.ProdutoLoja)
                        .ThenInclude(p => p.Produto)
                .Include(l => l.detalheListaProds)
                    .ThenInclude(d => d.ProdutoLoja)
                        .ThenInclude(l => l.Loja)
                .FirstOrDefault(d => d.Id == id);

            Document doc = new Document();

            Page page = doc.Pages.Add();


            Table table = new Table();
            table.ColumnWidths = "70 50 50 70 50 60";

            MarginInfo margin = new MarginInfo();
            margin.Top = 5f;
            margin.Left = 5f;
            margin.Right = 5f;
            margin.Bottom = 5f; 
            table.DefaultCellPadding = margin;

            Row row0 = table.Rows.Add();
            row0.Cells.Add("Quantidade");
            row0.Cells.Add("Produto");
            row0.Cells.Add("Marca");
            row0.Cells.Add("Comerciante");
            row0.Cells.Add("Preço");
            row0.Cells.Add("Morada");
            row0.Cells.Add("Comprado");
            if (conteudo == "ilustrativo")
            {
                row0.Cells.Add("Imagem");
            }

            row0.BackgroundColor = Aspose.Pdf.Color.FromArgb(255, 181, 70);

            for (int i = 0; i < listaProdutos.detalheListaProds.Count; i++)
            {
                var item = listaProdutos.detalheListaProds[i];

                Row row = table.Rows.Add();
                row.Cells.Add(item.quantity.ToString());
                row.Cells.Add(item.ProdutoLoja.Produto.Name);
                row.Cells.Add(item.ProdutoLoja.Produto.Brand);

                if(conteudo == "simples")
                {
                    var c = _context.Comerciante.Where(c => c.UserId == item.ProdutoLoja.Loja.UserId).First().Name;
                    row.Cells.Add(c);
                }
                if(conteudo == "ilustrativo")
                {
                    var c = _context.Comerciante.Where(c => c.UserId == item.ProdutoLoja.Loja.UserId).First().logo;
                    Aspose.Pdf.Image img = new Aspose.Pdf.Image();
                    var image = c;
                    MemoryStream memStream = new MemoryStream();
                    memStream.Write(image, 0, image.Length);
                    img.ImageStream = memStream;
                    img.FixWidth = 40;
                    img.FixHeight = 40;
                    Cell cellImage = row.Cells.Add();
                    cellImage.Paragraphs.Add(img);
                }
                
                row.Cells.Add(item.ProdutoLoja.Price.ToString() + "€");
                row.Cells.Add(item.ProdutoLoja.Loja.Address);

                Cell cell = new Cell();
                row.Cells.Add("\u25a1").Alignment = HorizontalAlignment.Center;

                if(conteudo == "ilustrativo")
                {

                    Aspose.Pdf.Image img = new Aspose.Pdf.Image();
                    // Path for source file
                    var image = item.ProdutoLoja.Produto.Image;
                    MemoryStream memStream = new MemoryStream();
                    memStream.Write(image, 0, image.Length);
                    img.ImageStream = memStream;
                    // Set width for image instance
                    img.FixWidth = 40;
                    // Set height for image instance
                    img.FixHeight = 40;
                    // Create cell object and add it to row instance
                    Cell cellImage = row.Cells.Add();
                    // Add image to paragraphs collection of recently added cell instance
                    cellImage.Paragraphs.Add(img);
                }

                if (i % 2 != 0)
                {
                    row.BackgroundColor = Aspose.Pdf.Color.FromArgb(252, 244, 204);
                }

            }

            float preco = 0;
            foreach (var item in listaProdutos.detalheListaProds)
            {
                preco = preco + (item.quantity * item.ProdutoLoja.Price);
            }
            Row foot = table.Rows.Add();
            foot.Cells.Add("Preço total: " + preco + "€");

            doc.Pages[1].Paragraphs.Add(table);
            var stream = new MemoryStream();
            doc.Save(stream);

           
            if (ficheiro == "pdf")
            {
                return new FileContentResult(stream.ToArray(), "application/pdf");

            }
            if (ficheiro == "png"){
                // Create file stream for output image
                using (var imageStream = new MemoryStream())
                {
                    Resolution resolution = new Resolution(300);

                    PngDevice PngDevice = new PngDevice(2480, 3508, resolution);

                    PngDevice.Process(doc.Pages[1], imageStream);

                    imageStream.Close();

                    return new FileContentResult(imageStream.ToArray() , "image/png");

                }
            }
            return new FileContentResult(stream.ToArray(), "application/pdf");


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

        public async Task<IActionResult> Show(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var c = await _context.Comerciante
                .FirstOrDefaultAsync(m => m.Id == id);

            return File(c.logo, "image/jpg");
        }
    }
}
