using Microsoft.AspNet.Identity;
using PublicLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PublicLibrary.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            /* var userId = User.Identity.GetUserId();
             var readerId = db.Readers.Where(c => c.ApplicationUserId == userId).First().ApplicationUserId;
             ViewBag.ApplicationUserId = readerId; */
            return View(db.Books.ToList());
           // return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

       
    }
}