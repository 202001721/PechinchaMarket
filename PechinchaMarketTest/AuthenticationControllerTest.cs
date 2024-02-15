using Microsoft.AspNetCore.Mvc;
using PechinchaMarket.Controllers;

namespace PechinchaMarketTest
{
    public class AuthenticationControllerTest
    {
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public void AuthenticationSupport_ReturnsView(int someValue) { 
            var controller = new AuthenticationController(null);
            
            var result = controller.AuthenticationSupport(someValue);

            var viewResult = Assert.IsType<ViewResult>(result);
        }
    }
}
