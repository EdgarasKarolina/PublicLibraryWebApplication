using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PublicLibrary;
using PublicLibrary.Controllers;
using PublicLibrary.Models.Abstract;
using PublicLibrary.Models;
using Moq;


namespace PublicLibrary.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void AddBook()
        {

            //Arrange
            //Create a BooksController obj.
            //Want to pass a mock
            var mockRepo = new Mock<IBookRepository>(); //creating IBookRepository Mock
            BooksController controller = new BooksController(mockRepo.Object); //passing mock 

            Book book = new Book() {BookId =35691, Title = "Edgaras", Author = "Something",
            YearPublished = "2006", Genre = "scify", NumberOfPages = 300, IsItAvailable = true  };

            //Act
            controller.Create(book);

            //Assert
            mockRepo.Verify(repo => repo.InsertOrUpdate(book));
            


        }

        [TestMethod]
        public void DeleteBook()
        {
            var mockRepo = new Mock<IBookRepository>();
            BooksController controller = new BooksController(mockRepo.Object);

            Book book = new Book()
            {
                BookId = 1,
                Title = "Edgaras",
                Author = "Something",
                YearPublished = "2006",
                Genre = "scify",
                NumberOfPages = 300,
                IsItAvailable = true
            };

            controller.Create(book);

            // set up the repository’s Delete call
            //mockRepo.Setup(r => r.Delete(It.IsAny<Book>()));


            mockRepo.Verify(repo => repo.Find(book.BookId));


            //controller.Delete(book.BookId);

            //test to see if user has been deleted
            //Assert.AreEqual(null, book);
            // mockRepo.Verify(repo => repo.Delete(book));
            //Assert.IsNotNull(book);
            // mockRepo.Verify(repo => repo.Find(1));




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
