using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Oracle.DataAccess;
using Oracle.ManagedDataAccess.Client;
using WebApp.Models;
using System.Web.Security;

namespace WebApp.Controllers
{
    public class LoginController : Controller
    {
        
        // GET: Login
        public ActionResult Index()
        {
            return View(); //returns the view of the login page
        }
        
        // this method doesnt work but it was supposed to work on the login page where it gets the input of the user and checks 
        // if the existing account is in the database for admins to login
        // Unfortunately i didnt manage to make it working or even hardcode the passwords
        [HttpPost]
        public ActionResult Login(string USERNAME, string PASSWORD)
        {
            USERACCOUNT existingUser = new USERACCOUNT(); //gets the username password

            existingUser.USERNAME = USERNAME; 
            existingUser.PASSWORD = PASSWORD;

            if (existingUser.USERNAME == "acolonel" && existingUser.PASSWORD == "colonelaa")
            {
                return Redirect("/Home/Index"); // redirects to internal url
            }
            else
            {
                return View();
            }
        }
    }
}