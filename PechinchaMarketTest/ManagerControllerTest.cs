using Microsoft.AspNetCore.Http.HttpResults;
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
    public class ManagerControllerTest : IClassFixture<ApplicationDbContextFixture>
    {
        private DBPechinchaMarketContext _context;
        private Comerciante comerciante;
        private Produto produto;
        private PechinchaMarketUser utilizador;

        public ManagerControllerTest(ApplicationDbContextFixture context)
        {
            _context = context.DbContext;

            Restart_Context();
        }

        private void Restart_Context()
        {
            _context.Comerciante.RemoveRange(_context.Comerciante);
            _context.Produto.RemoveRange(_context.Produto);
            _context.Users.RemoveRange(_context.Users);



            var guid_Comerciante = Guid.NewGuid();

            utilizador = new PechinchaMarketUser
            {
                Email = "email@gmail.com",
                UserName = "utilizador",
                EmailConfirmed = true,
                Id = Guid.NewGuid().ToString(),

            };
            comerciante = new Comerciante { Id = guid_Comerciante, 
                                            Name = "Continente", 
                                            UserId =utilizador.Id, 
                                            contact = 987654321, 
                                            logo = GenerateRandomBytes(100),
                                            document = GenerateRandomBytes(100),
                                            isApproved = false,
                                            Lojas = new List<Loja>()};
            produto = new Produto
            {
                Id=100,
                Name= "Bruno",
                Image= GenerateRandomBytes(100),
                Brand="Human",
                ProdCategoria = Categoria.Peixaria,
                Unidade= UnidadeMedida.Kg,
                Weight = 2.0f,
            };

            _context.Comerciante.Add(comerciante);
            _context.Produto.Add(produto);

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
        public async void NonConfirmedList()
        {
            Restart_Context(); //Como os testes não são executados sequencialmente

            var controller = new ManagerController(_context);

            var result = await controller.NonConfirmedList();

            var viewResult = Assert.IsType<ViewResult>(result); //Verifica se uma view é retornada

            var model = Assert.IsAssignableFrom<List<Tuple<Comerciante, PechinchaMarketUser>>>(viewResult.ViewData.Model); //Verifica se o Model é do tipo Lista Tupple <Comerciante, PechinchaMarketUser>
             
        }

        [Fact]
        public async void NonAprovedProducts()
        {
            Restart_Context(); //Como os testes não são executados sequencialmente

            var controller = new ManagerController(_context);

            var result = await controller.NonAprovedProducts(produto.Id);

            var viewResult = Assert.IsType<ViewResult>(result); //Verifica se uma view é retornada
            var model = Assert.IsAssignableFrom<List<Produto>>(viewResult.ViewData.Model); //Verifica se o Model é do tipo Lista de produtos

        }
        public async void NonAprovedProducts_ProductDoestExist()
        {
            Restart_Context(); //Como os testes não são executados sequencialmente

            var controller = new ManagerController(_context);

            var result = await controller.NonAprovedProducts(000000);

            var viewResult = Assert.IsType<ViewResult>(result); //Verifica se uma view é retornada
            var model = Assert.IsAssignableFrom<List<Produto>>(viewResult.ViewData.Model); //Verifica se o Model é do tipo Lista de produtos

        }


        [Fact]
        public async void DetailsComerciante_returnsNotFound_WhenUserDontExist()
        {
            Restart_Context(); //Como os testes não são executados sequencialmente

            var controller = new ManagerController(_context);

            var result = await controller.DetailsComerciante(new Guid());

            Assert.IsType<ViewResult>(result); //Verifica se uma view é retornada

        }
        [Fact]
        public async void DetailsComerciante_returnsNotFound_WhenIdNull()
        {
            Restart_Context(); //Como os testes não são executados sequencialmente

            var controller = new ManagerController(_context);

            var result = await controller.DetailsComerciante(null);

            Assert.IsType<NotFoundResult>(result); //Verifica se uma view é retornada

        }

        [Fact]
        public async void DetailsComerciante()
        {
            Restart_Context(); //Como os testes não são executados sequencialmente

            var controller = new ManagerController(_context);

            var result = await controller.DetailsComerciante(comerciante.Id);

            var viewResult = Assert.IsType<ViewResult>(result); //Verifica se uma view é retornada

        }
        [Fact]
        public async void DetailsProduct()
        {
            Restart_Context(); //Como os testes não são executados sequencialmente

            var controller = new ManagerController(_context);

            var result = await controller.DetailsProduct(produto.Id);

            var viewResult = Assert.IsType<ViewResult>(result); //Verifica se uma view é retornada

            var model = Assert.IsAssignableFrom<List<Tuple<Produto, ProdutoLoja>>>(viewResult.ViewData.Model);



        }

        [Fact]
        public async void DetailsProduct_idNull()
        {
            Restart_Context(); //Como os testes não são executados sequencialmente

            var controller = new ManagerController(_context);

            var result = await controller.DetailsProduct(null);

           Assert.IsType<NotFoundResult>(result); //Verifica se uma view é retornada



        }
        // 
        [Fact]
        public async void Aprove()
        {
            Restart_Context(); //Como os testes não são executados sequencialmente

            var controller = new ManagerController(_context);

            var result = await controller.Aprove(comerciante.Id);


            var viewResult = Assert.IsType<ViewResult>(result); //Verifica se uma view é retornada
            //Assert.Equal(nameof(NonConfirmedList), viewResult.ActionName);
        }
        [Fact]
        public async void AproveConfirmed()
        {
            Restart_Context(); //Como os testes não são executados sequencialmente

            var controller = new ManagerController(_context);

            var result = await controller.AproveConfirmed(comerciante.Id);


            var viewResult = Assert.IsType<RedirectToActionResult>(result); //Verifica se uma view é retornada
            Assert.Equal(nameof(NonConfirmedList), viewResult.ActionName);
        }


        [Fact]
        public async void ShowLogo()
        {
            Restart_Context(); //Como os testes não são executados sequencialmente
            var controller = new ManagerController(_context);

            var result = await controller.ShowLogo(comerciante.Id);

            var viewResult = Assert.IsType<FileContentResult>(result);

        }
        [Fact]
        public async void Show_IdNull()
        {
            Restart_Context(); //Como os testes não são executados sequencialmente
            var controller = new ManagerController(_context);
            var result = await controller.ShowLogo(null);

            var viewResult = Assert.IsType<NotFoundResult>(result);

        }




    }
}
