using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using teenBook;
using teenBook.Controllers;
using teenBook.Data;

namespace teenBook.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]

        public void GetTopicsTest()
        {
            // Arrange
            ITeenbookRepository _repo = new TeenbookMockRepository();

            //Act
            var topics = _repo.GetTopics()
            .OrderByDescending(t => t.Created)
            .Take(25)
            .ToList();

            //Assert
            Assert.IsTrue(topics.Count == 2);
            
        }
        [TestMethod]
        public void Index()
        {
            //need to fix the DI

            ITeenbookRepository tbMockRepo = new TeenbookMockRepository();
           // Arrange
           HomeController controller = new HomeController(tbMockRepo);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About()
        {
            ITeenbookRepository tbMockRepo = new TeenbookMockRepository();
            // Arrange
            HomeController controller = new HomeController(tbMockRepo);

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void Contact()
        {
            ITeenbookRepository tbMockRepo = new TeenbookMockRepository();
            // Arrange
            HomeController controller = new HomeController(tbMockRepo);

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
