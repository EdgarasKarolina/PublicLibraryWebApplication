using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PublicLibrary.ViewModels
{
    public class BookingViewModel
    {
        public int BookId { get; set; }

        
        public string Title { get; set; }
        public string Author { get; set; }

        [Display(Name = "Year published")]
        public string YearPublished { get; set; }

        public string Genre { get; set; }

        [Display(Name = "Number of pages")]
        public int NumberOfPages { get; set; }

        [Display(Name = "Do you want to receive an email?")]
        public bool IfWantEmail { get; set; }

        [Display(Name = "Enter your email")]
        public string EnterEmail { get; set; }
    }
}