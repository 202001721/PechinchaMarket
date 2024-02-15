using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PechinchaMarket.Areas.Identity.Data;
using PechinchaMarket.Controllers;
using PechinchaMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PechinchaMarketTest
{
    public class ComerciantesControllerTest : IClassFixture<ApplicationDbContextFixture>
    {
        private DBPechinchaMarketContext _context;
        private Comerciante comerciante;

        public ComerciantesControllerTest(ApplicationDbContextFixture context)
        {
            _context = context.DbContext;

            Restart_Context();
        }

        private void Restart_Context()
        {
            _context.Comerciante.RemoveRange(_context.Comerciante);

            var guid_Comerciante = Guid.NewGuid();
            comerciante = new Comerciante { Id = guid_Comerciante, 
                                            Name = "Continente", 
                                            UserId = guid_Comerciante.ToString(), 
                                            contact = 987654321, 
                                            logo = GenerateRandomBytes(100),
                                            document = GenerateRandomBytes(100),
                                            Lojas = new List<Loja>()};

            _context.Comerciante.Add(comerciante);

            _context.SaveChanges();
        }

        public static byte[] GenerateRandomBytes(int length)
        {
            Random rand = new Random();
            byte[] buffer = new byte[length];
            rand.NextBytes(buffer);
            return buffer;
        }

        [Fact]
        public async void Index_returnsView()
        {
            Restart_Context(); //Como os testes não são executados sequencialmente

            var controller = new ComerciantesController(_context);

            var result = await controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result); //Verifica se uma view é retornada
            var model = Assert.IsAssignableFrom<IEnumerable<Comerciante>>(viewResult.ViewData.Model); //Verifica se o Model é do tipo Lista Comerciante
            Assert.Single(model); //Verifica se o model passado só tem 1 unico Comerciante
        }

        [Fact]
        public async void Details_returnsNotFound_WhenIdNull()
        {
            Restart_Context(); //Como os testes não são executados sequencialmente

            var controller = new ComerciantesController(_context);

            var result = await controller.Details(null);

            Assert.IsType<NotFoundResult>(result); //Verifica se uma view é retornada
        }

        [Fact]
        public async void Details_returnsNotFound_WhenUserDontExist()
        {
            Restart_Context(); //Como os testes não são executados sequencialmente

            var controller = new ComerciantesController(_context);

            var result = await controller.Details(new Guid());

            Assert.IsType<NotFoundResult>(result); //Verifica se uma view é retornada

        }

        [Fact]
        public async void Details_returnsView()
        {
            Restart_Context(); //Como os testes não são executados sequencialmente

            var controller = new ComerciantesController(_context);

            var result = await controller.Details(comerciante.Id);

            var viewResult = Assert.IsType<ViewResult>(result); //Verifica se uma view é retornada
            Assert.IsAssignableFrom<Comerciante>(viewResult.ViewData.Model); //Verifica se o Model é do tipo Comerciante
        }

        [Fact]
        public void Create_HttpGet_returnsView()
        {
            Restart_Context(); //Como os testes não são executados sequencialmente

            var controller = new ComerciantesController(_context);

            var result = controller.Create();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void Create_HttpPost_returnsRedirectToAction()
        {
            Restart_Context(); //Como os testes não são executados sequencialmente

            var controller = new ComerciantesController(_context);

            var guid_Comerciante = Guid.NewGuid();
            var newComerciante = new Comerciante{ Id = guid_Comerciante,
                                                  Name = "Lidl",
                                                  UserId = guid_Comerciante.ToString(),
                                                  contact = 987654321,
                                                  logo = GenerateRandomBytes(100),
                                                  document = GenerateRandomBytes(100),
                                                  Lojas = new List<Loja>()
                                                };

            var result = await controller.Create(newComerciante);

            var viewResult = Assert.IsType<RedirectToActionResult>(result); //Verifica se uma view é retornada
            Assert.Equal(nameof(Index), viewResult.ActionName); //Verifica se o redirecionamento é para a action Index

            var createdComerciante = await _context.Comerciante.FirstOrDefaultAsync(c => c.Id == newComerciante.Id);
            Assert.NotNull(createdComerciante); //Verifica se o novo Comerciante foi adicionado ao context
        }

        [Fact]
        public async void Create_HttpPost_returnsView()
        {
            Restart_Context(); //Como os testes não são executados sequencialmente

            var controller = new ComerciantesController(_context);
            controller.ModelState.AddModelError("Key", "ErrorMessage"); //Mete o ModelState Invalido para um retorno da View

            var guid_Comerciante = Guid.NewGuid();
            var newComerciante = new Comerciante
            {
                Id = guid_Comerciante,
                Name = "Lidl",
                UserId = guid_Comerciante.ToString(),
                contact = 987654321,
                logo = GenerateRandomBytes(100),
                document = GenerateRandomBytes(100),
                Lojas = new List<Loja>()
            };

            var result = await controller.Create(newComerciante);

            var viewResult = Assert.IsType<ViewResult>(result); //Verifica se uma view é retornada
            var model = Assert.IsAssignableFrom<Comerciante>(viewResult.ViewData.Model); //Verifica se o Model é do tipo Comerciante
            Assert.Equal(newComerciante.Id, model.Id); //Verifica se o Comerciante passado é igual passado na view
        }

    }
}
