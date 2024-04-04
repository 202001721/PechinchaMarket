using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Moq;
using PechinchaMarket.Areas.Identity.Data;
using PechinchaMarket.Controllers;
using PechinchaMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        private AgrupamentoMembro agrupamentoMembro;
        private ListaProdutos listaProdutos;
        private DetalheListaProd detalheListaProd;
        private ProdutoLoja produtoLoja;
        private Produto produto;
        private Loja loja;
        private Comerciante comerciante;


        public AgrupamentosControllerTest(ApplicationDbContextFixture context)
        {
            _context = context.DbContext;

            Restart_Context();

        }

        private void Restart_Context()
        {
            _context.Cliente.RemoveRange(_context.Cliente);
            _context.Agrupamentos.RemoveRange(_context.Agrupamentos);
            _context.Produto.RemoveRange(_context.Produto);
            _context.ProdutoLoja.RemoveRange(_context.ProdutoLoja);
            _context.DetalheListaProd.RemoveRange(_context.DetalheListaProd);
            _context.ListaProdutos.RemoveRange(_context.ListaProdutos);
            _context.Loja.RemoveRange(_context.Loja);
            _context.Comerciante.RemoveRange(_context.Comerciante);

            _context.SaveChanges();
            
            //definir _userManager com Mock
            var user = new PechinchaMarketUser() { UserName = "JohnDoe", Id = "1" };

            Mock<UserManager<PechinchaMarketUser>> userMgr = GetMockUserManager();
            userMgr.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user);
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
                Codigo = GenerateRandomNumber(),
                ListaProdutos = new List<ListaProdutos>()
            };

            agrupamentoMembro = new AgrupamentoMembro
            {
                Id = Guid.NewGuid(),
                Cliente = cliente,
                Agrupamento = agrupamento,
                Privilegio = NivelPrivilegio.Admin
            };

            comerciante = new Comerciante
            {
                Id = Guid.NewGuid(),
                UserId = "1",
                Name = "Comerciante",
                contact = 123456789,
                logo = GenerateRandomBytes(100),
                document = GenerateRandomBytes(100),
                isApproved = true,
                Lojas = new List<Loja>()

            };

            loja = new Loja
            {
                Id = Guid.NewGuid(),
                Address = "morada",
                OpeningTime = DateTime.Now,
                ClosingTime = DateTime.Now,
                UserId = "1",
            };
            comerciante.Lojas.Add(loja);


            produto = new Produto
            {
                Id = 1,
                Name = "product",
                Brand = "brand",
                Image = GenerateRandomBytes(100),
                Unidade = UnidadeMedida.Kg,
                ProdCategoria = Categoria.Enlatados,
                ProdEstado = Estado.Approved

            };

            produtoLoja = new ProdutoLoja
            {
                Id = 1,
                Price = 1,
                Produto = produto,
                Loja = loja

            };

            listaProdutos = new ListaProdutos
            {
                Id = Guid.NewGuid(),
                name = "Lista",
                ClienteId = guid_cliente.ToString(),
                state = EstadoProdutoCompra.PorComprar,
                detalheListaProds = new List<DetalheListaProd>()

            };

            detalheListaProd = new DetalheListaProd
            {
                Id = Guid.NewGuid(),
                quantity = 1,
                ListaProdutos = listaProdutos,
                ProdutoLoja = produtoLoja
            };
            listaProdutos.detalheListaProds.Add(detalheListaProd);

            // Adicionar o cliente e o agrupamento ao contexto e salvar as alterações
            _context.Cliente.Add(cliente);
            _context.Agrupamentos.Add(agrupamento);
            _context.Produto.Add(produto);
            _context.ProdutoLoja.Add(produtoLoja);
            _context.DetalheListaProd.Add(detalheListaProd);
            _context.ListaProdutos.Add(listaProdutos);
            _context.Loja.Add(loja);
            _context.Comerciante.Add(comerciante);
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

            controler.TempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
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

            controller.TempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
            var result = await controller.EditName(agrupamento.Id, newAgrupamento);
            var viewResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal(nameof(Index), viewResult.ActionName);
        }

        [Fact]
        public async void AddList_ReturnsView()
        {
            Restart_Context();
            var controller = new AgrupamentosController(_context, _userManager, _hostingEnvironment);

            var newAgrupamento = new Agrupamento
            {
                Id = agrupamento.Id,
                Nome = agrupamento.Nome,
                Codigo = agrupamento.Codigo,
                ListaProdutos = agrupamento.ListaProdutos
            };

            var newList = new ListaProdutos
            {
                Id = listaProdutos.Id,
                name = listaProdutos.name,
                ClienteId = listaProdutos.ClienteId,
                state = listaProdutos.state,
                detalheListaProds = listaProdutos.detalheListaProds
            };

            controller.TempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
            var result = await controller.AddList(agrupamento.Id, newList.Id.ToString());
            
            var viewResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal(nameof(Index), viewResult.ActionName);
        }

        [Fact]
        public async void AddMemberLeitor_ReturnsView() {  
            Restart_Context();
            var controller = new AgrupamentosController(_context, _userManager, _hostingEnvironment);
            var newAgrupamento = new Agrupamento
            {
                Id = agrupamento.Id,
                Nome = agrupamento.Nome,
                Codigo = agrupamento.Codigo,
                ListaProdutos = agrupamento.ListaProdutos
            };

            var newCliente = new Cliente
            {
                Id = cliente.Id,
                Name = cliente.Name,
                UserId = cliente.UserId,
                Localizacao = cliente.Localizacao
            };


            controller.TempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
            var result = await controller.AddMemberLeitor(agrupamento.Id, newCliente.Id);
            var viewResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal(nameof(Index), viewResult.ActionName);
        }

        [Fact]
        public async void RemoveMember_ReturnsView()
        {
            Restart_Context();
            var controller = new AgrupamentosController(_context, _userManager, _hostingEnvironment);
            var newAgrupamento = new Agrupamento
            {
                Id = agrupamento.Id,
                Nome = agrupamento.Nome,
                Codigo = agrupamento.Codigo,
                ListaProdutos = agrupamento.ListaProdutos
            };

            var newCliente = new Cliente
            {
                Id = cliente.Id,
                Name = cliente.Name,
                UserId = cliente.UserId,
                Localizacao = cliente.Localizacao
            };


            var result = await controller.RemoveMember(agrupamento.Id, newCliente.Id);
            var viewResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal(nameof(Index), viewResult.ActionName);
        }

        [Fact]
        public async void RemoveMembers_ReturnsView()
        {
            Restart_Context();
            var controller = new AgrupamentosController(_context, _userManager, _hostingEnvironment);
            var newAgrupamento = new Agrupamento
            {
                Id = agrupamento.Id,
                Nome = agrupamento.Nome,
                Codigo = agrupamento.Codigo,
                ListaProdutos = agrupamento.ListaProdutos
            };

            controller.TempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
            var result = await controller.RemoveMembers(agrupamento.Id, new List<Guid>());
            var viewResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal(nameof(Index), viewResult.ActionName);
        }

        [Fact]
        public async void RemoveList_ReturnsView()
        {
            Restart_Context();
            var controller = new AgrupamentosController(_context, _userManager, _hostingEnvironment);
            var newAgrupamento = new Agrupamento
            {
                Id = agrupamento.Id,
                Nome = agrupamento.Nome,
                Codigo = agrupamento.Codigo,
                ListaProdutos = agrupamento.ListaProdutos
            };

            var newListaProdutos = new ListaProdutos
            {
                Id = listaProdutos.Id,
                name = listaProdutos.name,
                ClienteId = listaProdutos.ClienteId,
                state = listaProdutos.state,
                detalheListaProds = listaProdutos.detalheListaProds
            };

            controller.TempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
            var result = await controller.RemoveList(agrupamento.Id, newListaProdutos.Id);
            var viewResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal(nameof(Index), viewResult.ActionName);
        }


        [Fact]
        public async void RemoveLists_ReturnsView()
        {
            Restart_Context();
            var controller = new AgrupamentosController(_context, _userManager, _hostingEnvironment);
            var newAgrupamento = new Agrupamento
            {
                Id = agrupamento.Id,
                Nome = agrupamento.Nome,
                Codigo = agrupamento.Codigo,
                ListaProdutos = agrupamento.ListaProdutos
            };


            var result = await controller.RemoveLists(agrupamento.Id, new List<Guid>());
            var viewResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal(nameof(Index), viewResult.ActionName);
        }

        [Fact]
        public async void ChangePermissions_ReturnsView()
        {
            Restart_Context();
            var controller = new AgrupamentosController(_context, _userManager, _hostingEnvironment);
            var newAgrupamento = new Agrupamento
            {
                Id = agrupamento.Id,
                Nome = agrupamento.Nome,
                Codigo = agrupamento.Codigo,
                ListaProdutos = agrupamento.ListaProdutos
            };

            var newAgrupamentoMembro = new AgrupamentoMembro
            {
                Id = agrupamentoMembro.Id,
                Cliente = agrupamentoMembro.Cliente,
                Agrupamento = agrupamentoMembro.Agrupamento,
                Privilegio = agrupamentoMembro.Privilegio
            };

            controller.TempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
            var result = await controller.ChangePermissions(agrupamento.Id, ["Editor_"+newAgrupamentoMembro.Agrupamento.Id]);
            var viewResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal(nameof(Index), viewResult.ActionName);
        }

        [Fact]
        public async void Delete_ReturnsView()
        {
            Restart_Context();
            var controller = new AgrupamentosController(_context, _userManager, _hostingEnvironment);
            var newAgrupamento = new Agrupamento
            {
                Id = agrupamento.Id,
                Nome = agrupamento.Nome,
                Codigo = agrupamento.Codigo,
                ListaProdutos = agrupamento.ListaProdutos
            };

            var result = await controller.Delete(newAgrupamento.Id);
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void DeleteConfirmed_ReturnsView()
        {
            Restart_Context();
            var controller = new AgrupamentosController(_context, _userManager, _hostingEnvironment);
            var newAgrupamento = new Agrupamento
            {
                Id = agrupamento.Id,
                Nome = agrupamento.Nome,
                Codigo = agrupamento.Codigo,
                ListaProdutos = agrupamento.ListaProdutos
            };

            var result = await controller.DeleteConfirmed(newAgrupamento.Id);
            var viewResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal(nameof(Index), viewResult.ActionName);
        }

        [Fact]
        public async void EnterWithCode_ReturnsView()
        {
            Restart_Context();
            var controller = new AgrupamentosController(_context, _userManager, _hostingEnvironment);
            
            var newAgrupamento = new Agrupamento
            {
                Id = agrupamento.Id,
                Nome = agrupamento.Nome,
                Codigo = agrupamento.Codigo,
                ListaProdutos = agrupamento.ListaProdutos
            };

            controller.TempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
            var result = await controller.EnterWithCode(newAgrupamento.Codigo);
            var viewResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal(nameof(Index), viewResult.ActionName);
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

        public static byte[] GenerateRandomBytes(int length)
        {
            Random rand = new Random();
            byte[] buffer = new byte[length];
            rand.NextBytes(buffer);
            return buffer;
        }
    }
}
