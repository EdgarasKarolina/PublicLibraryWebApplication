using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PublicLibrary.Models;
using Microsoft.AspNet.Identity;
using PublicLibrary.Models.Abstract;
using PublicLibrary.ViewModels;
using PublicLibrary.Models.Repositories;
using Ninject.Modules;
using Ninject;


namespace PublicLibrary.Controllers
{
    public class BooksController : Controller
    {

        private readonly IBookRepository _bookRepo;

        //injecting IBookRepository object into BooksController
        //when someone will be creating an instance of BooksController
        //it will always be expecting to pass in the dependency
        //which is IBookRepository in this case
        public BooksController(IBookRepository repo)
        {
            this._bookRepo = repo;
        }




        // GET: Books/Index
        public ActionResult Index(string title = "", string bookGenre = "")
        {
            var genreLst = new List<string>();


            
            var genreQry = from d in _bookRepo.GetAll()
                           orderby d.Genre
                           select d.Genre;

            //AddRange adds objects at the back of the list
            //TODO delete when done to test distinct
            genreLst.AddRange(genreQry.Distinct());
            ViewBag.bookGenre = new SelectList(genreLst);

            


            if (!string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(bookGenre))
            {
                return View(_bookRepo.GetBooksWithTitleAndGenre(title, bookGenre));
            }

            if (!string.IsNullOrEmpty(title))
            {
                return View(_bookRepo.GetBooksWithTitle(title));
            }

            if (!string.IsNullOrEmpty(bookGenre))
            {
                return View(_bookRepo.GetBooksWithGenre(bookGenre));
            }

            return View(_bookRepo.GetAll());

        }


        //Get: Books/Create
        public ActionResult Create()
        {

            return View();
        }

        //Post: Books/Create
        [HttpPost]
         [Authorize(Roles = "Boss")]
         [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "BookId,Title,Author,YearPublished,Genre,NumberOfPages,IsItAvailable")] Book book)
        {
            //please confirm your password????
            if (ModelState.IsValid)
            {


                _bookRepo.InsertOrUpdate(book);

                return RedirectToAction("Index");
            }

            return View(book);
        }

        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = _bookRepo.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Books/Delete/5
        [Authorize(Roles = "Boss")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = _bookRepo.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Boss")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = _bookRepo.Find(id);
            _bookRepo.Delete(book);

            return RedirectToAction("Index");
        }

        // GET: Books/Edit/5
        [Authorize(Roles = "Boss")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = _bookRepo.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        [HttpPost]
        [Authorize(Roles = "Boss")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = "BookId,Title,Author,YearPublished,Genre,NumberOfPages,IsItAvailable")] Book book)
        {
            if (ModelState.IsValid)
            {
                _bookRepo.InsertOrUpdate(book);
                return RedirectToAction("Index");
            }
            return View(book);
        }


    }
}