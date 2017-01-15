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
      

        private IBookingRepository bookingRepo;
        private IReaderRepository readerRepo;
        private IBookRepository bookRepo;

        public BookingsController(IBookingRepository bookingRepo, IReaderRepository readerRepo, IBookRepository bookRepo)
        {
            this.bookingRepo = bookingRepo;
            this.readerRepo = readerRepo;
            this.bookRepo = bookRepo;
        }

            

        // GET: Bookings
        [Authorize(Roles = "Boss")]
        public ActionResult Index()
        {
           

             var bookings = from booking in bookingRepo.GetAll()
             select booking;
            
            return View(bookings.ToList());
        }  


        public ActionResult ShowUserBookings()
        {
            var userId = User.Identity.GetUserId();
          
          int readerId = readerRepo.GetAll().Where(c => c.ApplicationUserId == userId).First().ReaderId;


            var bookings = from booking in bookingRepo.GetAll()
                           where booking.ReaderId == readerId
                           select booking;
                               

             return View(bookings.ToList());
            
        }
       

        
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
        }  

        // GET: Bookings/Create
        public ActionResult Create(int? id)
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
               
                int readerId = readerRepo.GetAll().Where(c => c.ApplicationUserId == userId).First().ReaderId;

            
                booking.ReaderId = readerId;
                booking.BookId = bookingViewModel.BookId;


                
                bookingRepo.InsertOrUpdate(booking);

                //THIS IS VALUES THAT WE GET FROM VIEW MODEL
                //AND WE ARE NOT USING THEM IN THE DATABASE
                //OUR IDEA IS TO USE THEM IN ORDER TO SEND EMAILS TO USERS
                bool value = bookingViewModel.IfWantEmail;
                string value2 = bookingViewModel.EnterEmail;
                
                
                return RedirectToAction("ShowUserBookings");
            }
            
           
            ViewBag.BookId = new SelectList(bookRepo.GetAll(), "BookId", "Title", booking.BookId);
           
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
            
            Booking booking = bookingRepo.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            
            ViewBag.BookId = new SelectList(bookRepo.GetAll(), "BookId", "Title", booking.BookId);
            
            ViewBag.ReaderId = new SelectList(readerRepo.GetAll(), "ReaderId", "AccountNumber", booking.ReaderId);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        [HttpPost]
        [Authorize(Roles = "Boss")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ReaderId,BookId")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                ;
                bookingRepo.InsertOrUpdate(booking);
                return RedirectToAction("Index");
            }
            
            ViewBag.BookId = new SelectList(bookRepo.GetAll(), "BookId", "Title", booking.BookId);
           
            ViewBag.ReaderId = new SelectList(readerRepo.GetAll(), "ReaderId", "AccountNumber", booking.ReaderId);
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
           
            Booking booking = bookingRepo.Find(id);
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
            
            Booking booking = bookingRepo.Find(id);
            
            bookingRepo.Delete(booking);
            
            return RedirectToAction("Index");
        }

      /*  protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        } */
    }
}
