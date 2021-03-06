using DependencyInjection.Controllers;
using DependencyInjection.Infrastructure;
using DependencyInjection.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;


namespace DependencyInjection.Tests
{
    public class DITests
    {
        [Fact]
        public void ControllerTest(){
            //Arrange
            var data = new[] { new Product { Name = "Test", Price = 100 } };
            var mock = new Mock<IRepository>();
            mock.SetupGet(m => m.Products).Returns(data);
            TypeBroker.SetTestObjet(mock.Object);
            HomeController controller = new HomeController();

            //Act
            ViewResult result = controller.Index();

            //Assert
            Assert.Equal(data, result.ViewData.Model);
        }

        [Fact]
        public void ControllerTestDI(){
            //Arrange
            var data = new[] { new Product { Name = "Test", Price = 100 } };
            var mock = new Mock<IRepository>();
            mock.SetupGet(m => m.Products).Returns(data);
            HomeDIController controller = new HomeDIController(mock.Object);

            //Act
            ViewResult result = controller.Index();

            //Assert
            Assert.Equal(data, result.ViewData.Model);
        }
    }
}