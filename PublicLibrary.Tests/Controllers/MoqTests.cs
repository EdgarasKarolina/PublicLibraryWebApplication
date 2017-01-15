using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PublicLibrary.Models.Abstract;
using Moq;
using PublicLibrary.Controllers;
using PublicLibrary.Models;
using System.Web.Mvc;

namespace PublicLibrary.Tests.Controllers
{
    [TestClass]
    public class MoqTests
    {
        [TestMethod]
        public void AddBook()
        {

            //Arrange
            //Create a BooksController obj.
            //Want to pass a mock
            var mockRepo = new Mock<IBookRepository>(); //creating IBookRepository Mock
            BooksController controller = new BooksController(mockRepo.Object); //passing mock 

            Book book = new Book();
            

            //Act
            controller.Create(book);

            //Assert
            mockRepo.Verify(repo => repo.InsertOrUpdate(book));



        }

        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void Contact()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}

