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
using PublicLibrary.Models.Repositories;
using System.Data.Entity.Infrastructure;

using Microsoft;
using PublicLibrary.ViewModels;

namespace PublicLibrary.Controllers
{
    [Authorize]
    public class BookingsController : Controller
    {
/*
        private IBookingRepository bookingRepo;
        private IReaderRepository readerRepo;

        public BookingsController(IBookingRepository bookingRepo, IReaderRepository readerRepo)
        {
            this.bookingRepo = bookingRepo;
            this.readerRepo = readerRepo;
        }

            */

        private ApplicationDbContext db = new ApplicationDbContext();

      

        
        // GET: Bookings
        [Authorize(Roles = "Boss")]
        public ActionResult Index()
        {
            var bookings = db.Bookings.Include(b => b.Book).Include(b => b.Reader);
            
            return View(bookings.ToList());
        }  


        public ActionResult ShowUserBookings()
        {
            var userId = User.Identity.GetUserId();
            int readerId = db.Readers.Where(c => c.ApplicationUserId == userId).First().ReaderId;
            var bookings = db.Bookings.Include(b => b.Book).Include(b => b.Reader).Where(b=> b.ReaderId == readerId);
             return View(bookings.ToList());
            
        }
        /*
        // GET: Bookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = bookingRepo.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }  */

        
        // GET: Bookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }  

        // GET: Bookings/Create
        //using modelView instead of model
        public ActionResult Create(int? id)
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
            // ViewBag.BookId = new SelectList(db.Books, "BookId", "Title");
            // ViewBag.BookIdd = id;
            //ViewBag.ReaderId = new SelectList(db.Readers, "ReaderId", "AccountNumber");

            /* var userId = User.Identity.GetUserId();
             int readerId = db.Readers.Where(c => c.ApplicationUserId == userId).First().ReaderId;
             ViewBag.ReaderId = readerId; */
            // ViewBag.ApplicationUserId = readerId;
            BookingViewModel bookingViewModel = new BookingViewModel();
            bookingViewModel.BookId = book.BookId;
            bookingViewModel.Title = book.Title;
            bookingViewModel.Author = book.Author;
            bookingViewModel.YearPublished = book.YearPublished;
            bookingViewModel.Genre = book.Genre;
            bookingViewModel.NumberOfPages = book.NumberOfPages;


            return View(bookingViewModel);
        }

        // POST: Bookings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ReaderId,BookId,IfWantEmail,EnterEmail")] BookingViewModel bookingViewModel)
        {
            Booking booking = new Booking();
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                int readerId = db.Readers.Where(c => c.ApplicationUserId == userId).First().ReaderId;

                //assigning values to booking object
                booking.ReaderId = readerId;
                booking.BookId = bookingViewModel.BookId;
                
                
                db.Bookings.Add(booking);
                bool value = bookingViewModel.IfWantEmail;
                string value2 = bookingViewModel.EnterEmail;
                db.SaveChanges();
                
                return RedirectToAction("ShowUserBookings");
            }

            ViewBag.BookId = new SelectList(db.Books, "BookId", "Title", booking.BookId);
           // ViewBag.ReaderId = new SelectList(db.Readers, "ReaderId", "AccountNumber", booking.ReaderId);

         /*   var userId = User.Identity.GetUserId();
            int readerId = db.Readers.Where(c => c.ApplicationUserId == userId).First().ReaderId;
            // ViewBag.ApplicationUserId = readerId;
            ViewBag.ReaderId = readerId;  */
            return View(booking);
        }

        // GET: Bookings/Edit/5
        [Authorize(Roles = "Boss")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            ViewBag.BookId = new SelectList(db.Books, "BookId", "Title", booking.BookId);
            ViewBag.ReaderId = new SelectList(db.Readers, "ReaderId", "AccountNumber", booking.ReaderId);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Boss")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ReaderId,BookId")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BookId = new SelectList(db.Books, "BookId", "Title", booking.BookId);
            ViewBag.ReaderId = new SelectList(db.Readers, "ReaderId", "AccountNumber", booking.ReaderId);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        [Authorize(Roles = "Boss")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Boss")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking booking = db.Bookings.Find(id);
            db.Bookings.Remove(booking);
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
    }
}
