using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private Entities1 db = new Entities1();

        //returns the view of the home page
        public ActionResult Index()
        {
            return View();
        }

        //redirects to the login page for the user to log out
        public ActionResult LogOut()
        {
            return Redirect("/Login/Index"); // redirects to internal url
        }

       
    }
}