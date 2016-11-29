using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PublicLibrary.Models
{
    public class Reader
    {
        public int ReaderId { get; set; }
       public string AccountNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        //reference to a user that holds Readers account
        public virtual ApplicationUser User { get; set; }
        public string ApplicationUserId { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }

        //2 more will be
        //list of borrowed books
        //list of booked books

       // public virtual ICollection<LoaningBook> LoaningBooks { get; set; }


    }
}