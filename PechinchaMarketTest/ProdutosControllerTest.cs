using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PechinchaMarket.Areas.Identity.Data;
using PechinchaMarket.Controllers;
using PechinchaMarket.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PechinchaMarketTest
{
    public class ProdutosControllerTest : IClassFixture<ApplicationDbContextFixture>
    {

        private DBPechinchaMarketContext _context;
        private UserManager<PechinchaMarketUser> _userManager;
        private Produto produto;
        private Loja loja;

        public ProdutosControllerTest(ApplicationDbContextFixture context) 
        {
            _context = context.DbContext;

            Restart_Context();

        }

        private void Restart_Context()
        {
            _context.Cliente.RemoveRange(_context.Cliente);
            _context.Produto.RemoveRange(_context.Produto);
            _context.Loja.RemoveRange(_context.Loja);

            _context.SaveChanges();

            //definir _userManager com Mock
            var user = new PechinchaMarketUser() { UserName = "JohnDoe", Id = "1" };

            Mock<UserManager<PechinchaMarketUser>> userMgr = GetMockUserManager();
            userMgr.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user);
            userMgr.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(user);
            userMgr.Setup(s => s.GetUserId(It.IsAny<ClaimsPrincipal>())).Returns("1");

            _userManager = userMgr.Object;
            //

            loja = new Loja
            {
                Id = Guid.NewGuid(),
                Address = "morada",
                OpeningTime = DateTime.Now,
                ClosingTime = DateTime.Now,
                UserId = "1",
            };

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

            

            _context.Produto.Add(produto);
            _context.Loja.Add(loja);

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
        public async void Create_ReturnsView_newProduto()
        {
            Restart_Context();
            var controller = new ProdutosController(_context,_userManager);

            var content = "Hello World from a Fake File";
            var fileName = "test.pdf";
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(content);
            writer.Flush();
            stream.Position = 0;
            IFormFile file = new FormFile(stream, 0, stream.Length, "id_from_form", fileName);

            var newProduto = new Produto
            {
                Id = 2,
                Name = "product",
                Brand = "brand",
                Image = GenerateRandomBytes(100),
                Unidade = UnidadeMedida.Kg,
                ProdCategoria = Categoria.Vegan,
                ProdEstado = Estado.Approved
            };

            var result = await controller.Create(newProduto, file, [2,2]);
            var viewResult = Assert.IsType<RedirectToActionResult>(result);

        }


        [Fact]
        public async void Edit_ReturnsRedirectToAction()
        {
            Restart_Context();
            var controller = new ProdutosController(_context,_userManager);

            var content = "Hello World from a Fake File";
            var fileName = "test.pdf";
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(content);
            writer.Flush();
            stream.Position = 0;
            IFormFile file = new FormFile(stream, 0, stream.Length, "id_from_form", fileName);

            var result = await controller.Edit(1, produto, [2], [10], file, ["10/10/2024 - 11/11/2024"]);
            var viewResult = Assert.IsType<RedirectToActionResult>(result);

        }
      

    }
}
