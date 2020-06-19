using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class TransactionsController : Controller
    {
        private Entities1 db = new Entities1();

        // GET: Transactions
        public ActionResult Index(string searchBy, string search)
        {
            // links the transactions table with the payment details table
            var tRANSACTIONS = db.TRANSACTIONS.Include(t => t.PAYMENTDETAIL);

            // passing in the values for the user to search 
            // if the value he searched is an actual user id 
            if (searchBy == "USERID")
            {
                return View(db.TRANSACTIONS.Where(t => t.USERID.ToString().Contains(search) || search == null).ToList()); //returns the search of the user or the whole list if its null
            }
            else
            {
                return View(db.TRANSACTIONS.ToList()); // returns the whole list
            }
            
        }

        // GET: Transactions/Details/5
        /// <summary>
        /// sents the transaction id to check if the transaction exists
        /// </summary>
        /// <param name="id"></param> Transaction
        /// <returns></returns>
        public ActionResult Details(decimal id)
        {
            //searches for the id and if the id and the whole table to display
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TRANSACTION tRANSACTION = db.TRANSACTIONS.Find(id);
            if (tRANSACTION == null)
            {
                return HttpNotFound();
            }
            return View(tRANSACTION);
        }

        // GET: Transactions/Create
        /// <summary>
        ///  checks the last transaction id created and adds up to auto generate ids when the create button is pressed
        /// </summary>
        /// <returns></returns> Transaction ID 
        public ActionResult Create()
        {
            //gets the id from the user 
            ViewBag.USERID = new SelectList(db.USERACCOUNTs, "USERID", "USERID");
            TRANSACTION newTransaction = new TRANSACTION();
            // creates a new transaction so it can generate a unique id for each transaction
            var lastTransaction = db.TRANSACTIONS.OrderByDescending(x => x.TRANSACTIONID).FirstOrDefault();
            // if there are no transactions the first one gets assigned and then it gets incremented
            if (lastTransaction == null)
            {
                newTransaction.TRANSACTIONID = 8001;
            }
            else if (lastTransaction.TRANSACTIONID != 0)
            {
                newTransaction.TRANSACTIONID = lastTransaction.TRANSACTIONID + 1;
            }
            //converts to int
            Convert.ToInt32(newTransaction.TRANSACTIONID);

            return View(newTransaction); // returns the new transaction
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        // using forgery token for security
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TRANSACTIONID,USERID,PRICE,DATEOFPURCHASE")] TRANSACTION tRANSACTION)
        {
            //checks if its valid for security reasons
            if (ModelState.IsValid)
            {
                db.TRANSACTIONS.Add(tRANSACTION); // adds the transaction
                db.SaveChanges(); // saves changes
                return RedirectToAction("Index"); // goers back to table page
            }

            ViewBag.USERID = new SelectList(db.PAYMENTDETAILS, "USERID", "USERID", tRANSACTION.USERID); // passes the user id
            return View(tRANSACTION); // returns the transaction created
        }

        // checks if the id and the element exists in the database
        // GET: Transactions/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TRANSACTION tRANSACTION = db.TRANSACTIONS.Find(id);
            if (tRANSACTION == null)
            {
                return HttpNotFound();
            }
            ViewBag.USERID = new SelectList(db.PAYMENTDETAILS, "USERID", "STREETNUMBER", tRANSACTION.USERID);
            return View(tRANSACTION);
        }

        //passes the new changes of the specific element to the table
        // POST: Transactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TRANSACTIONID,USERID,NAMEONCARD,CARDNUMBER,EXPIRYDATE,PRICE,DATEOFPURCHASE")] TRANSACTION tRANSACTION)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tRANSACTION).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.USERID = new SelectList(db.PAYMENTDETAILS, "USERID", "STREETNUMBER", tRANSACTION.USERID);
            return View(tRANSACTION);
        }

        //takes the user to the delete view with the specific element to be deleted and asks the user if he wants to delete
        // GET: Transactions/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TRANSACTION tRANSACTION = db.TRANSACTIONS.Find(id);
            if (tRANSACTION == null)
            {
                return HttpNotFound();
            }
            return View(tRANSACTION);
        }

        // confirms the deletion of the element
        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            TRANSACTION tRANSACTION = db.TRANSACTIONS.Find(id);
            db.TRANSACTIONS.Remove(tRANSACTION);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // disposes the database to save changes the soonest
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
