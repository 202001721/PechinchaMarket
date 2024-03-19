using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using PechinchaMarket.Areas.Identity.Data;
using PechinchaMarket.Controllers;
using PechinchaMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PechinchaMarketTest
{
    public class AgrupamentosControllerTest : IClassFixture<ApplicationDbContextFixture>
    {
        private DBPechinchaMarketContext _context;
        private UserManager<PechinchaMarketUser> _userManager;
        private IWebHostEnvironment _hostingEnvironment;
        private Cliente cliente;
        private Agrupamento agrupamento;


        public AgrupamentosControllerTest(ApplicationDbContextFixture context)
        {
            _context = context.DbContext;

            Restart_Context();

        }

        private void Restart_Context()
        {
            _context.Cliente.RemoveRange(_context.Cliente);
            _context.Agrupamentos.RemoveRange(_context.Agrupamentos);
            _context.SaveChanges();
            
            //definir _userManager com Mock
            var user = new PechinchaMarketUser() { UserName = "JohnDoe", Id = "1" };

            Mock<UserManager<PechinchaMarketUser>> userMgr = GetMockUserManager();
            userMgr.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(user);
            userMgr.Setup(s => s.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns("1");
            _userManager = userMgr.Object;

            //criar mock de IWebHostEnvironment
            var mockEnvironment = new Mock<IWebHostEnvironment>();
            mockEnvironment
                .Setup(m => m.WebRootPath)
                .Returns("wwwroot");
            _hostingEnvironment = mockEnvironment.Object;

            //Criar um novo cliente e um novo agrupamento
            var guid_cliente = Guid.NewGuid();
            cliente = new Cliente { Id = guid_cliente, Name = "JohnDoe", UserId = "1", Localizacao = "Lisboa" };
            agrupamento = new Agrupamento
            {
                Id = Guid.NewGuid(),
                Nome = "Agrupamento",
                Codigo = 12345,
            };

            // Adicionar o cliente e o agrupamento ao contexto e salvar as alterações
            _context.Cliente.Add(cliente);
            _context.Agrupamentos.Add(agrupamento);
            _context.SaveChanges();
        }

        public Mock<UserManager<PechinchaMarketUser>> GetMockUserManager()
        {
            var userStoreMock = new Mock<IUserStore<PechinchaMarketUser>>();
            return new Mock<UserManager<PechinchaMarketUser>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null);
        }

        [Fact]
        public async void Index_ReturnsView()
        {
            Restart_Context();
            var controller = new AgrupamentosController(_context, _userManager, _hostingEnvironment);
            var result = await controller.Index();

            var viewResult =  Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<AgrupamentoMembro>>(viewResult.Model);
            Assert.NotNull(model);

        }

        [Fact]
        public async void Create_ReturnsView_newAgrupamento()
        {
            Restart_Context();
            var controler = new AgrupamentosController(_context, _userManager, _hostingEnvironment);

            var newAgrupamento = new Agrupamento
            {
                Id = Guid.NewGuid(),
                Nome = "Agrupamento da familia",
                Codigo = GenerateRandomNumber(),
            };

            var result = await controler.Create(newAgrupamento);
            var viewResult = Assert.IsType<RedirectToActionResult>(result);

        }

        [Fact]
        public async void EditName_ReturnsView()
        {
            Restart_Context();
            var controller = new AgrupamentosController(_context, _userManager, _hostingEnvironment);

            var newAgrupamento = new Agrupamento
            {
                Id = agrupamento.Id,
                Nome = "Agrupamento da familia",
                Codigo = GenerateRandomNumber(),
            };

            var result = await controller.EditName(agrupamento.Id, newAgrupamento);
            var viewResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal(nameof(Index), viewResult.ActionName);
        }

        /*[Fact]
        public async void AddList_ReturnsView()
        {
            Restart_Context();
            var controller = new AgrupamentosController(_context, _userManager);

            

            var result = await controller.AddList(agrupamento.Id, "lista");
            var viewResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal(nameof(Index), viewResult.ActionName);
        }*/

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
    }
}
