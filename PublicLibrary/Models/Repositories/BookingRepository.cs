using PublicLibrary.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PublicLibrary.Models.Repositories
{
    public class BookingRepository : IBookingRepository
    {

        private ApplicationDbContext db = new ApplicationDbContext();


        public void Delete(Booking booking)
        {
            db.Bookings.Remove(booking);
            db.SaveChanges();
        }

        public Booking Find(int? id)
        {
            return db.Bookings.Find(id);
        }



        public IEnumerable<Booking> GetAll()
        {
            return db.Bookings;
        }

        public void InsertOrUpdate(Booking booking)
        {
            if (booking.BookId <= 0)
            {
                db.Bookings.Add(booking);
            }
            else
            {
                db.Entry(booking).State = System.Data.Entity.EntityState.Modified;
            }
            db.SaveChanges();
        }
    }
}