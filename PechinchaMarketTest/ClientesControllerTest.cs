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

        public ClientesControllerTest(ApplicationDbContextFixture context)
        {
            _context = context.DbContext;

            _context.Cliente.Add(new Cliente { Id = Guid.NewGuid(), UserId = "", Localizacao = "Lisboa", Preferencias = new List<Categoria>() });

            _context.SaveChanges();
        }

        [Fact]
        public void Index_returnsView() { 
            var controller = new ClientesController(_context);

            var result = controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Details_ReturnsNotFound_WhenUserDontExist() {

            var mockUserManager = new Mock<UserManager<PechinchaMarketUser>>(new Mock<IUserStore<PechinchaMarketUser>>().Object,
                                                                            new Mock<IOptions<IdentityOptions>>().Object,
                                                                            new Mock<IPasswordHasher<PechinchaMarketUser>>().Object,
                                                                            new IUserValidator<PechinchaMarketUser>[0],
                                                                            new IPasswordValidator<PechinchaMarketUser>[0],
                                                                            new Mock<ILookupNormalizer>().Object,
                                                                            new Mock<IdentityErrorDescriber>().Object,
                                                                            new Mock<IServiceProvider>().Object,
                                                                            new Mock<ILogger<UserManager<PechinchaMarketUser>>>().Object);

            var controller = new ClientesController(_context);

            var result = controller.Details(null);

            Assert.IsType<NotFoundResult>(result);
        }


    }
}
