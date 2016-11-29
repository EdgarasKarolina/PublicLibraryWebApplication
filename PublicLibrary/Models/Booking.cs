using PublicLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PublicLibrary.Models
{
    public class Booking
    {
        public int Id { get; set; }

        public int ReaderId { get; set; }
        public virtual Reader Reader { get; set; }

        public int BookId { get; set; }
        public virtual Book Book { get; set; }


    }
}