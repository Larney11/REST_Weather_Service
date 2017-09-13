using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using REST_Weather_Service;
using REST_Weather_Service.Controllers;

namespace REST_Weather_Service.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Home Page", result.ViewBag.Title);
        }
    }
}
