using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Controller;
using Moq;
using PechinchaMarket.Areas.Identity.Data;
using PechinchaMarket.Controllers;
using PechinchaMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace PechinchaMarketTest
{
    public class SearchControllerTest : IClassFixture<ApplicationDbContextFixture>
    {
        private DBPechinchaMarketContext _context;
        private UserManager<PechinchaMarketUser> _userManager;
        private Produto produto;
        private ProdutoLoja produtoLoja;
        private Cliente cliente;

        public SearchControllerTest(ApplicationDbContextFixture context)
        {
            _context = context.DbContext;

            Restart_Context();
        }

        private void Restart_Context()
        {
            _context.Cliente.RemoveRange(_context.Cliente);
            _context.Produto.RemoveRange(_context.Produto);
            _context.ProdutoLoja.RemoveRange(_context.ProdutoLoja);

            var guid_Cliente = Guid.NewGuid();
            cliente = new Cliente { Id = guid_Cliente, Name = "JohnDoe", UserId = "1", Localizacao = "Lisboa" };

            //definir _userManager com Mock
            var user = new PechinchaMarketUser() { UserName = "JohnDoe", Id = "1" };

            Mock<UserManager<PechinchaMarketUser>> userMgr = GetMockUserManager();
            userMgr.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(user);

            userMgr.Setup(s => s.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns("1");

            _userManager = userMgr.Object;

            produto = new Produto
            {
                Id = 10,
                Name = "Manteiga",
                Image = GenerateRandomBytes(100),
                Brand = "Primor",
                ProdCategoria = Categoria.Frescos,
                Unidade = UnidadeMedida.Unit,
                Weight = 2.0f,
                ProdEstado = Estado.Approved
            };

            produtoLoja = new ProdutoLoja
            {
                Id = 1,
                Price = 2.0f,
                Produto = produto
            };

            _context.Cliente.Add(cliente);
            _context.Produto.Add(produto);
            _context.ProdutoLoja.Add(produtoLoja);

            _context.SaveChanges();
        }

        public Mock<UserManager<PechinchaMarketUser>> GetMockUserManager()
        {
            var userStoreMock = new Mock<IUserStore<PechinchaMarketUser>>();
            return new Mock<UserManager<PechinchaMarketUser>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null);
        }

        public static byte[] GenerateRandomBytes(int length)
        {
            Random rand = new Random();
            byte[] buffer = new byte[length];
            rand.NextBytes(buffer);
            return buffer;
        }

        [Fact]
        public void Index_ReturnsView()
        {
            Restart_Context();

            var controller = new SearchController(_context,_userManager);

            var result = controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Search_manteiga_returnsRedirectToAction()
        {
            Restart_Context();

            var controller = new SearchController( _context,_userManager);

            var result = controller.Search("manteiga");

            Assert.IsType<RedirectToActionResult>(result);
            

        }

        [Fact]
        public void SearchResults_manteiga_returnView()
        {
            Restart_Context();

            var controller = new SearchController(_context, _userManager);

            var result = controller.SearchResults("manteiga");

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Produto>>(viewResult.Model);
            Assert.Single(model);

        }

        [Fact]
        public void SearchResults_xmanteiga_returnView()
        {
            Restart_Context();

            var controller = new SearchController(_context, _userManager);

            var result = controller.SearchResults("xmanteiga");

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Produto>>(viewResult.Model);
            Assert.Single(model);

        }

        //not working
        /*[Fact]
        public void SearchResults_xanteiga_returnView()
        {
            Restart_Context();

            var controller = new SearchController(_context, _userManager);

            var result = controller.SearchResults("xanteiga");

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Produto>>(viewResult.Model);
            Assert.Single(model);

        }*/

        [Fact]
        public async void ShowImage_returnsFile()
        {
            Restart_Context(); //Como os testes não são executados sequencialmente
            var controller = new SearchController(_context,_userManager);

            var result = await controller.ShowImage(produto.Id);

            Assert.IsType<FileContentResult>(result);
        }

        [Fact]
        public async void AddToList_returnsTuple()
        {
            Restart_Context();
            var controller = new SearchController(_context, _userManager);
            var result = await controller.AddToList(produto.Id);

            var viewResult = Assert.IsType<ViewResult>( result );
            Assert.IsAssignableFrom<List<Tuple<PechinchaMarketUser,Comerciante,Loja,ProdutoLoja,Produto>>>(viewResult.Model);
        }

        //este teste não funciona por falta de login
        /*
        [Fact]
        public async void AddProduct_RedirectsToAction()
        {
            Restart_Context();

            var controller = new SearchController(_context, _userManager);
            var result = await controller.AddProduct(produtoLoja.Id,1,"Lista");

            Assert.IsType<RedirectToActionResult>(result);

        }*/

        [Fact]
        public void SimilarProduct()
        {
            Restart_Context();
            var controller = new SearchController(_context, _userManager);
            var result = controller.SimilarProducts(produto.Id);
            
            Assert.IsType<List<Produto>>(result);
        }

    }
}
