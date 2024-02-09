using Microsoft.AspNetCore.Mvc;
using PechinchaMarket.Controllers;


namespace PechinchaMarketTest
{
    public class HomeControllerTest
    {
        [Fact]
        public void Index_ReturnsViewResult()
        {
            var controller = new HomeController(null);
            
            var result = controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Privacy_ReturnsPartialViewResult() 
        {
            var controller = new HomeController(null);

            var result = controller.Privacy();

            var viewResult = Assert.IsType<ViewResult>(result);
        }
    }
}