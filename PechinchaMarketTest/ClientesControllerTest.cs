using Microsoft.AspNetCore.Mvc;
using PechinchaMarket.Areas.Identity.Data;
using PechinchaMarket.Controllers;

namespace PechinchaMarketTest
{
    public class ClientesControllerTest : IClassFixture<ApplicationDbContextFixture>
    {

        private DBPechinchaMarketContext _context;

        public ClientesControllerTest(ApplicationDbContextFixture context)
        {
            _context = context.DbContext;
        }

    }
}
