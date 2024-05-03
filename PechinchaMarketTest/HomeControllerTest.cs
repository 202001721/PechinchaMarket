using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using PechinchaMarket.Areas.Identity.Data;
using PechinchaMarket.Controllers;
using Moq;
using PechinchaMarket.Models;
using System.Security.Claims;


namespace PechinchaMarketTest
{
    public class HomeControllerTest : IClassFixture<ApplicationDbContextFixture>
    {
        private DBPechinchaMarketContext _context;
        private UserManager<PechinchaMarketUser> _userManager;
        private Produto produto;
        private Loja loja;

        public HomeControllerTest(ApplicationDbContextFixture context)
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
        public void Index_ReturnsViewResult()
        {
            var controller = new HomeController(_context , _userManager, null);
            
            var result = controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var viewModel = Assert.IsAssignableFrom<List<Produto>>(viewResult.Model); 
        }

        [Fact]
        public void Privacy_ReturnsPartialViewResult() 
        {
            var controller = new HomeController(_context, _userManager, null);

            var result = controller.Privacy();

            var viewResult = Assert.IsType<ViewResult>(result);
        }
    }
}