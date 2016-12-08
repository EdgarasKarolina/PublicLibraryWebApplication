using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PublicLibrary.ViewModels
{
    public class BookViewModel
    {
        public string Title { get; set; }
        public string Author { get; set; }

        [Required(ErrorMessage = "Specify the year of publication")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Year must be numeric")]
        public string YearPublished { get; set; }

        public string Genre { get; set; }
        public int NumberOfPages { get; set; }
        public bool IsItAvailable { get; set; }
    }
}