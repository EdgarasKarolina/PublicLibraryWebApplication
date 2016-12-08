using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PublicLibrary.Models
{
    public class Book
    {
        public int BookId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [Required(ErrorMessage = "Specify the year of publication")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Year must be numeric")]
        public string YearPublished { get; set; }
        [Required]
        public string Genre { get; set; }
        [Required(ErrorMessage = "Specify the number of pages")]
        public int NumberOfPages { get; set; }
        public bool IsItAvailable { get; set; }
    }
}