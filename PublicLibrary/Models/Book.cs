using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PublicLibrary.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string YearPublished { get; set; }
        public string Genre { get; set; }
        public int NumberOfPages { get; set; }
        public bool IsItAvailable { get; set; }
        //public int Size { get; set; }
    }
}