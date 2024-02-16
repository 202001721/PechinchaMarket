using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using PechinchaMarket.Areas.Identity.Data;
using PechinchaMarket.Controllers;
using PechinchaMarket.Models;
using System.Web.WebPages.Html;

namespace PechinchaMarketTest
{
    public class ClientesControllerTest : IClassFixture<ApplicationDbContextFixture>
    {

        private DBPechinchaMarketContext _context;
        private Cliente cliente;

        public ClientesControllerTest(ApplicationDbContextFixture context)
        {
            _context = context.DbContext;

            Restart_Context();
        }

        private void Restart_Context()
        {
            _context.Cliente.RemoveRange(_context.Cliente);

            var guid_Cliente = Guid.NewGuid();
            cliente = new Cliente { Id = guid_Cliente, Name = "Luis", UserId = guid_Cliente.ToString(), Localizacao = "Lisboa" };

            _context.Cliente.Add(cliente);

            _context.SaveChanges();
        }

        [Fact]
        public async void Index_returnsView() {
            Restart_Context(); //Como os testes não são executados sequencialmente

            var controller = new ClientesController(_context);

            var result = await controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result); //Verifica se uma view é retornada
            var model = Assert.IsAssignableFrom<IEnumerable<Cliente>>(viewResult.ViewData.Model); //Verifica se o Model é do tipo Lista Cliente
            Assert.Single(model); //Verifica se o model passado só tem 1 unico Cliente
        }

        [Fact]
        public async void Details_returnsNotFound_WhenIdNull() {
            Restart_Context(); //Como os testes não são executados sequencialmente

            var controller = new ClientesController(_context);

            var result = await controller.Details(null);

            Assert.IsType<NotFoundResult>(result); //Verifica se uma view é retornada
        }

        [Fact]
        public async void Details_returnsNotFound_WhenUserDontExist()
        {
            Restart_Context(); //Como os testes não são executados sequencialmente

            var controller = new ClientesController(_context);

            var result = await controller.Details(new Guid());

            Assert.IsType<NotFoundResult>(result); //Verifica se uma view é retornada

        }

        [Fact]
        public async void Details_returnsView()
        {
            Restart_Context(); //Como os testes não são executados sequencialmente

            var controller = new ClientesController(_context);

            var result = await controller.Details(cliente.Id);

            var viewResult = Assert.IsType<ViewResult>(result); //Verifica se uma view é retornada
            Assert.IsAssignableFrom<Cliente>(viewResult.ViewData.Model); //Verifica se o Model é do tipo Cliente
        }

        [Fact]
        public void Create_HttpGet_returnsView() {
            Restart_Context(); //Como os testes não são executados sequencialmente

            var controller = new ClientesController(_context);

            var result = controller.Create();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void Create_HttpPost_returnsRedirectToAction()
        {
            Restart_Context(); //Como os testes não são executados sequencialmente

            var controller = new ClientesController(_context);

            var guid_Cliente = Guid.NewGuid();
            var newCliente = new Cliente { Id = guid_Cliente, Name = "Bruno", UserId = guid_Cliente.ToString(), Localizacao = "Setubal" };
            var result = await controller.Create(newCliente);

            var viewResult = Assert.IsType<RedirectToActionResult>(result); //Verifica se uma view é retornada
            Assert.Equal(nameof(Index), viewResult.ActionName); //Verifica se o redirecionamento é para a action Index

            var createdCliente = await _context.Cliente.FirstOrDefaultAsync(c => c.Id == newCliente.Id);
            Assert.NotNull(createdCliente); //Verifica se o novo cliente foi adicionado ao context
        }

        [Fact]
        public async void Create_HttpPost_returnsView() {
            Restart_Context(); //Como os testes não são executados sequencialmente

            var controller = new ClientesController(_context);
            controller.ModelState.AddModelError("Key", "ErrorMessage"); //Mete o ModelState Invalido para um retorno da View

            var guid_Cliente = Guid.NewGuid();
            var newCliente = new Cliente { Id = guid_Cliente, Name = "Bruno", UserId = guid_Cliente.ToString(), Localizacao = "Setubal" };
            var result = await controller.Create(newCliente);

            var viewResult = Assert.IsType<ViewResult>(result); //Verifica se uma view é retornada
            var model = Assert.IsAssignableFrom<Cliente>(viewResult.ViewData.Model); //Verifica se o Model é do tipo Cliente
            Assert.Equal(newCliente.Id,model.Id); //Verifica se o Cliente passado é igual passado na view
        }

        /* Isto faz parte de uma fase de testes numa sprint mais avançada
        [Fact]
        public async void Edit_HttpGet_returnsNotFound_WhenIdNull() {
            Restart_Context(); //Como os testes não são executados sequencialmente

            var controller = new ClientesController(_context);

            var result = await controller.Edit(null);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void Edit_HttpGet_returnsNotFound_WhenUserDontExist()
        {
            Restart_Context(); //Como os testes não são executados sequencialmente

            var controller = new ClientesController(_context);

            var result = await controller.Edit(120);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void Edit_HttpGet_returnsView()
        {
            Restart_Context(); //Como os testes não são executados sequencialmente

            var controller = new ClientesController(_context);

            var result = await controller.Edit(cliente.Id); //O Edit recebe um int ID ?

            Assert.IsType<NotFoundResult>(result);
        }
        */
    }
}
