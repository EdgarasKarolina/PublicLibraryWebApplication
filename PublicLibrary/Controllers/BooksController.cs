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

        private IBookRepository bookRepo;
        
        //injecting IBookRepository object into BooksController
        //when someone will be creating an instance of BooksController
        //it will always be expecting to pass in the dependency
        //which is IBookRepository in this case
            public BooksController(IBookRepository repo)
        {
            this.bookRepo = repo;
        }

       

        //This is not necessary any more since I am using BookRepository
        private ApplicationDbContext db = new ApplicationDbContext();


        /* Action results that are using BookRepository!!!!!    */

        // GET: Books/Index
        public ActionResult Index(string bookGenre, string searchString)
        {
            var GenreLst = new List<string>();
            

            //var GenreQry = from d in db.Books
            var GenreQry = from d in bookRepo.GetAll() 
                           orderby d.Genre
                           select d.Genre;

            //AddRange adds objects at the back of the list
            //TODO delete when done to test distinct
            GenreLst.AddRange(GenreQry.Distinct());
            ViewBag.bookGenre = new SelectList(GenreLst);

            //var movies = from m in db.Books
            var movies = from m in bookRepo.GetAll()
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(bookGenre))
            {
                movies = movies.Where(x => x.Genre == bookGenre);
            }

            return View(movies);
        }

        
        //Get: Books/Create 
         public ActionResult Create()
        {
            
            return View();
        }

        //Post: Books/Create 
        [HttpPost]
        // [Authorize(Roles = "Boss")]
        // [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookId,Title,Author,YearPublished,Genre,NumberOfPages,IsItAvailable")] Book book)
        {
            //please confirm your password????
            if (ModelState.IsValid)
            {
                

                bookRepo.InsertOrUpdate(book);
                
                return RedirectToAction("Index");
            }

            return View("Index");
        }
        
        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = bookRepo.Find(id);
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
            Book book = bookRepo.Find(id);
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
            Book book = bookRepo.Find(id);
            bookRepo.Delete(book);
            
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
            Book book = bookRepo.Find(id);
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
        public ActionResult Edit([Bind(Include = "BookId,Title,Author,YearPublished,Genre,NumberOfPages,IsItAvailable")] Book book)
        {
            if (ModelState.IsValid)
            {
                bookRepo.InsertOrUpdate(book);
                return RedirectToAction("Index");
            }
            return View(book);
        }


        /*  // GET: Books
         public ActionResult Index()
         {
             return View(db.Books.ToList());
         }  */

        //Will have to be Index POST request
        // [HttpPost]
        /*  public ActionResult Index(string searchString)
          {

              var books = from b in db.Books
                          select b;

              if (!String.IsNullOrEmpty(searchString))
              {
                  books = books.Where(s => s.Title.Contains(searchString));
              }

              return View(books);
          } */

        /* public ActionResult Create()
         {
             BookViewModel model = new BookViewModel();

             return View(model);
         }



         [HttpPost]
        // [Authorize(Roles = "Boss")]
        // [ValidateAntiForgeryToken]
         public ActionResult Create([Bind(Include = "BookId,Title,Author,YearPublished,Genre,NumberOfPages,IsItAvailable")] BookViewModel viewModel)
         {
             if (ModelState.IsValid)
             {
                 var book = new Book()
                 {
                     Title = viewModel.Title,
                     Author = viewModel.Author,
                     YearPublished = viewModel.YearPublished,
                     Genre = viewModel.Genre,
                     NumberOfPages = viewModel.NumberOfPages,
                     IsItAvailable = viewModel.IsItAvailable

                 };
                 bookRepo.InsertOrUpdate(book);
                // db.SaveChanges();
                 return RedirectToAction("Index");
             }

             return View("Index");
         }  */
        /*
        
        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        //method to save a book into bookings list
        [Authorize]
        public ActionResult AddToBookings(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }
       

        public ActionResult Create()
        {
            BookViewModel model = new BookViewModel();

            return View(model);
        }


        //ask about THIS!!!!!!!!!
        [HttpPost]
        [Authorize(Roles = "Boss")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookId,Title,Author,YearPublished,Genre,NumberOfPages,IsItAvailable")] BookViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var book = new Book()
                {
                    Title = viewModel.Title,
                    Author = viewModel.Author,
                    YearPublished = viewModel.YearPublished,
                    Genre = viewModel.Genre,
                    NumberOfPages = viewModel.NumberOfPages,
                    IsItAvailable = viewModel.IsItAvailable

                };
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Index");
        } 

        // GET: Books/Edit/5

        [Authorize(Roles = "Boss")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Boss")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookId,Title,Author,YearPublished,Genre,NumberOfPages,IsItAvailable")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
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
            Book book = db.Books.Find(id);
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
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

       
    */

    }
}
