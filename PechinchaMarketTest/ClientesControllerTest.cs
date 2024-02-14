using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using PechinchaMarket.Areas.Identity.Data;
using PechinchaMarket.Controllers;
using PechinchaMarket.Models;

namespace PechinchaMarketTest
{
    public class ClientesControllerTest : IClassFixture<ApplicationDbContextFixture>
    {

        private DBPechinchaMarketContext _context;
        private Cliente cliente;

        public ClientesControllerTest(ApplicationDbContextFixture context)
        {
            _context = context.DbContext;

            var guid_Cliente = Guid.NewGuid();
            cliente = new Cliente { Id = guid_Cliente, Name = "Luis", UserId = guid_Cliente.ToString(), Localizacao = "Lisboa" };

            _context.Cliente.Add(cliente);;
            
            _context.SaveChanges();
        }

        [Fact]
        public async void Index_returnsView() {
            //var mock = new Mock<DBPechinchaMarketContext>();
            //mock.Setup(_context => _context.Cliente.ToListAsync()).ReturnsAsync(new List<Cliente>());

            var controller = new ClientesController(_context);

            var result = await controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result); //Verifica 
            var model = Assert.IsAssignableFrom<IEnumerable<Cliente>>(viewResult.ViewData.Model); //Verifica se o Model é do tipo Cliente
            Assert.Single(model); //Verifica se o model passado só tem 1 unico Cliente
        }

        [Fact]
        public async void Details_ReturnsNotFound_WhenIdNull() {
            var controller = new ClientesController(_context);

            var result = await controller.Details(null);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void Details_ReturnsNotFound_WhenUserDontExist()
        {
            var controller = new ClientesController(_context);

            var result = await controller.Details(new Guid());

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void Details_ReturnsView()
        {
            var controller = new ClientesController(_context);

            var result = await controller.Details(cliente.Id);

            Assert.IsType<ViewResult>(result);
        }

        
    }
}
