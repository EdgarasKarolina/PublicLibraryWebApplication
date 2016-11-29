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

namespace PublicLibrary.Controllers
{
    [Authorize]
    public class BookingsController : Controller
    {
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
            // return "These are my bookings";
        }

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


            return View(book);
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ReaderId,BookId")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                int readerId = db.Readers.Where(c => c.ApplicationUserId == userId).First().ReaderId;
                
                booking.ReaderId = readerId;
                
                db.Bookings.Add(booking);      
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
