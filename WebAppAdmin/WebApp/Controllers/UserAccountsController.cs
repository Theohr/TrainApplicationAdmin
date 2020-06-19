using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;
using System.Text;

namespace WebApp.Controllers
{
    public class UserAccountsController : Controller
    {
        private Entities1 db = new Entities1(); // entities of the database
        
        // GET: UserAccounts
        public ActionResult Index(string searchBy, string search)
        {
            // passing in the values for the user to search 
            // if the value he searched is an actual username or there is in a username
            if (searchBy == "Username")
            {
                return View(db.USERACCOUNTs.Where(x => x.USERNAME.Contains(search) || search == null).ToList()); // returns the usernames with that value
            }
            else
            {
                return View(db.USERACCOUNTs.Where(x => x.USERTYPE.Contains(search) || search == null).ToList()); // returns the values of usertype or even null 
            }
        }

        // GET: UserAccounts/Details/5
        public ActionResult Details(decimal id)
        {
            // checks if id is null 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USERACCOUNT uSERACCOUNT = db.USERACCOUNTs.Find(id);
            if (uSERACCOUNT == null) // checks if account exists
            {
                return HttpNotFound();
            }
            return View(uSERACCOUNT); // returns the account in details
        }

        // GET: UserAccounts/Create
        public ActionResult Create()
        {
            // on create button click creates a new user id
            USERACCOUNT newUser = new USERACCOUNT();

            // checks the last user created
            var lastUser = db.USERACCOUNTs.OrderByDescending(x => x.USERID).FirstOrDefault();
            
            if (lastUser == null) // if there is no user it starts from the start
            {
                newUser.USERID = 1001;
            }
            else if(lastUser.USERID != 0) // or +1 to the last id created
            {
                newUser.USERID = lastUser.USERID + 1;
            }

            Convert.ToInt32(newUser.USERID); //makes the id an integer

            return View(newUser); //returns the id
        }

        // POST: UserAccounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken] 
        public ActionResult Create([Bind(Include = "USERID,USERTYPE,FIRSTNAME,LASTNAME,PHONENUMBER,EMAILADDRESS,STREETNUMBER,POSTCODE,USERNAME,PASSWORD")] USERACCOUNT uSERACCOUNT)
        {
            if (ModelState.IsValid) // checks if its valid for secucity reasons
            {
                //StringBuilder sbComments = new StringBuilder();
                //sbComments.Append(HttpUtility.HtmlEncode(uSERACCOUNT.)); // code for another security tried to implement
                db.USERACCOUNTs.Add(uSERACCOUNT); // adds user account  
                db.SaveChanges(); // saves changes
                return RedirectToAction("Index"); // goes back to index
            }

            return View(uSERACCOUNT.ToString());
        }

        // GET: UserAccounts/Edit/5
        public ActionResult Edit(decimal id)
        {
            // passes id to check if the id exists
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest); // no id follwing the error
            }
            USERACCOUNT uSERACCOUNT = db.USERACCOUNTs.Find(id);
            if (uSERACCOUNT == null) // checks if there is the acc valid
            {
                return HttpNotFound();
            }
            return View(uSERACCOUNT); // return the account for edit
        }

        // POST: UserAccounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken] // protection for overposting data
        public ActionResult Edit([Bind(Include = "USERID,USERTYPE,FIRSTNAME,LASTNAME,PHONENUMBER,EMAILADDRESS,STREETNUMBER,POSTCODE,USERNAME,PASSWORD")] USERACCOUNT uSERACCOUNT)
        {
            // if statement for protection
            if (ModelState.IsValid)
            {
                db.Entry(uSERACCOUNT).State = EntityState.Modified; // gets the modified account
                db.SaveChanges(); // saves changes
                return RedirectToAction("Index"); // return back to index
            }
            return View(uSERACCOUNT);
        }

        // GET: UserAccounts/Delete/5
        public ActionResult Delete(decimal id)
        {
            // if there is no id then the page will crush throwing the folowing error
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USERACCOUNT uSERACCOUNT = db.USERACCOUNTs.Find(id);
            if (uSERACCOUNT == null)
            {
                return HttpNotFound(); // if there is no account with that id
            }
            return View(uSERACCOUNT); // returns the account with the specific id for deletion
        }

        // POST: UserAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            // confirming the deletion of a user
            USERACCOUNT uSERACCOUNT = db.USERACCOUNTs.Find(id); // finds the id
            db.USERACCOUNTs.Remove(uSERACCOUNT); // deletes the whole account
            db.SaveChanges(); // saves changes
            return RedirectToAction("Index"); // to the index of the table
        }

        // closing the database connections so the resources hold will be freed as soon as possible
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
