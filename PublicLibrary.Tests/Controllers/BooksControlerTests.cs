using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PublicLibrary.Models.Abstract;
using PublicLibrary.Controllers;
using PublicLibrary.Models;
//using Moq;
using Telerik.JustMock;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace PublicLibrary.Tests.Controllers
{
    [TestClass]
    public class BooksControlerTests
    {
        

        [TestMethod]
        public void IndexDisplayingAllBooks()
        {
            //it will use this fake repository instead of the one in the controller class
            //when this test will come to controller class it will use fake repository
            //that we incejted in this test instead of real repository in the controller
            //in this way it will not have anything to do with real database and it will
            //not insert anything to the db
            //if we didnt have dependency injection then in tests we would use connection
            //with real database:
            // BooksController controller = new BookController();
            //cauze in BookController class we would have :
            //this.bookRepo = new BookRepository();
            //this is the one which is connecting to real db
            var repo = Mock.Create<IBookRepository>();
            Mock.Arrange(() => repo.GetAll()).Returns(
                new[]
                {
                    new Book(),
                    new Book(),
                    new Book()
                });
            var controller = new BooksController(repo);

            //checking if Index() in controller will return expected ammount of books
            var result = (ViewResult)controller.Index();
            //model hold list of books
            var model = (IEnumerable<Book>)result.Model;

            Assert.AreEqual(3, model.Count());

        }

        [TestMethod]
        public void IndexDisplayingBooksWithTitleAndGenreTest()
        {

            var repo = Mock.Create<IBookRepository>();
            Mock.Arrange(() => repo.GetBooksWithTitleAndGenre("halo", "elo")).Returns(
                new[]
                {
                    new Book(),
                    new Book(),

                });
            var controller = new BooksController(repo);

            var result = (ViewResult)controller.Index("halo", "elo");
            var model = (IEnumerable<Book>)result.Model;

            Assert.AreEqual(2, model.Count());

        }


        [TestMethod]
        public void IndexDisplayingBooksWithTitleTest()
        {

            var repo = Mock.Create<IBookRepository>();
            Mock.Arrange(() => repo.GetBooksWithTitle("lo")).Returns(
                new[]
                {
                    new Book(),


                });
            var controller = new BooksController(repo);

            var result = (ViewResult)controller.Index("lo");
            var model = (IEnumerable<Book>)result.Model;

            Assert.AreEqual(1, model.Count());

        }

        [TestMethod]
        public void IndexDisplayingBooksWithGenreTest()
        {

            var repo = Mock.Create<IBookRepository>();
            Mock.Arrange(() => repo.GetBooksWithGenre("e")).Returns(
                new[]
                {
                    new Book(),
                    new Book(),
                    new Book(),
                    new Book(),


                });
            var controller = new BooksController(repo);

            var result = (ViewResult)controller.Index("", "e");
            var model = (IEnumerable<Book>)result.Model;

            Assert.AreEqual(4, model.Count());

        }

        // Create ----------------------------------------------------------------
        [TestMethod]
        public void CreateBookIsCalled()
        {

            var repo = Mock.Create<IBookRepository>();
            var controller = new BooksController(repo);
            Mock.Arrange(() => repo.InsertOrUpdate(Arg.IsAny<Book>())).MustBeCalled();

            var book = new Book()
            {
                BookId = 35691,
                Title = "Edgaras",
                Author = "Something",
                YearPublished = "2006",
                Genre = "scify",
                NumberOfPages = 300,
                IsItAvailable = true
            };



            var result = controller.Create(book);

            //Mock.Assert(repo);

            Assert.IsTrue(result is RedirectToRouteResult);

        }

        [TestMethod]
        public void CreateBookReturnsViewWhenModelStateIsInvalid()
        {

            var repo = Mock.Create<IBookRepository>();
            var controller = new BooksController(repo);


            var book = new Book()
            {
                BookId = 35691,
                Title = "Edgaras",
                Author = "Something",
                YearPublished = "2006",
                Genre = "scify",
                NumberOfPages = 300,
                IsItAvailable = true
            };





            controller.ViewData.ModelState.AddModelError("key", "errormessage");

            var result = (ViewResult)controller.Create(book);

            Assert.AreEqual(book, result.Model as Book);

        }


        //Details---------------------------

        [TestMethod]
        public void DetailsRetursModelToView()
        {

            var repo = Mock.Create<IBookRepository>();

            Mock.Arrange(() => repo.Find(1)).Returns(

                    new Book { BookId = 1 }
                );

            var controller = new BooksController(repo);





            var result = (ViewResult)controller.Details(1);
            var model = (Book)result.Model;



            Assert.AreEqual(model, result.Model as Book);

        }

        [TestMethod]
        public void DetailsWithNullRetursHttpNotFound()
        {

            var repo = Mock.Create<IBookRepository>();

            Mock.Arrange(() => repo.Find(1)).Returns(() => null);

            var controller = new BooksController(repo);





            var result = controller.Details(1);




            Assert.IsTrue(result is HttpNotFoundResult);

        }


    }
}

