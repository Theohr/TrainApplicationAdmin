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
    public class TicketsController : Controller
    {
        private Entities1 db = new Entities1();

        /// <summary>
        /// displays the table in the view and searches for the specific element if the user needs to find something
        /// </summary>
        /// <param name="searchBy"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        // GET: Tickets
        public ActionResult Index(string searchBy, string search)
        {
            var tICKETs = db.TICKETs.Include(t => t.JOURNEY).Include(t => t.TRANSACTION);

            if (searchBy == "JourneyID")
            {
                return View(db.TICKETs.Where(x => x.JOURNEYID.ToString().Contains(search) || search == null).ToList());
            }
            else
            {
                return View(db.TICKETs.ToList());
            }
        }

        // gets the id and displays the whole element with its values in the details view
        // GET: Tickets/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TICKET tICKET = db.TICKETs.Find(id);
            if (tICKET == null)
            {
                return HttpNotFound();
            }
            return View(tICKET);
        }

        /// <summary>
        /// auto generates unique id when is taken on the create page according to the last id
        /// </summary>
        /// <returns></returns>
        // GET: Tickets/Create
        public ActionResult Create()
        {
            ViewBag.JOURNEYID = new SelectList(db.JOURNEYs, "JOURNEYID", "JOURNEYID");
            ViewBag.TRANSACTIONID = new SelectList(db.TRANSACTIONS, "TRANSACTIONID", "TRANSACTIONID");
            ViewBag.USERID = new SelectList(db.TRANSACTIONS, "USERID", "USERID");
            TICKET newTicket = new TICKET();

            var lastTicket = db.TICKETs.OrderByDescending(x => x.TICKETID).FirstOrDefault();

            if (lastTicket == null)
            {
                newTicket.TICKETID = 2001;
            }
            else if (lastTicket.TICKETID != 0)
            {
                newTicket.TICKETID = lastTicket.TICKETID + 1;
            }

            return View(newTicket);
        }

        /// <summary>
        /// passes the new values to the database and saves changes and displays them on table
        /// </summary>
        /// <param name="tICKET"></param>
        /// <returns></returns>
        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TICKETID,USERID,TRANSACTIONID,JOURNEYID,PRICE,SEATRESERVATION")] TICKET tICKET)
        {
            if (ModelState.IsValid)
            {
                db.TICKETs.Add(tICKET);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.JOURNEYID = new SelectList(db.JOURNEYs, "JOURNEYID", "JOURNEYID", tICKET.JOURNEYID);
            ViewBag.TRANSACTIONID = new SelectList(db.TRANSACTIONS, "TRANSACTIONID", "TRANSACTIONID", tICKET.TRANSACTIONID);
            ViewBag.USERID = new SelectList(db.TRANSACTIONS, "USERID", "USERID");
            return View(tICKET);
        }

        /// <summary>
        /// takes the id and returns the element with its values for edit 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Tickets/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TICKET tICKET = db.TICKETs.Find(id);
            if (tICKET == null)
            {
                return HttpNotFound();
            }
            ViewBag.JOURNEYID = new SelectList(db.JOURNEYs, "JOURNEYID", "JOURNEYID", tICKET.JOURNEYID);
            ViewBag.TRANSACTIONID = new SelectList(db.TRANSACTIONS, "TRANSACTIONID", "TRANSACTIONID", tICKET.TRANSACTIONID);
            ViewBag.USERID = new SelectList(db.TRANSACTIONS, "USERID", "USERID");
            return View(tICKET);
        }

        // if its valid then it passes the changes to the table
        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TICKETID,USERID,TRANSACTIONID,JOURNEYID,PRICE,SEATRESERVATION")] TICKET tICKET)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tICKET).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.JOURNEYID = new SelectList(db.JOURNEYs, "JOURNEYID", "JOURNEYID", tICKET.JOURNEYID);
            ViewBag.TRANSACTIONID = new SelectList(db.TRANSACTIONS, "TRANSACTIONID", "TRANSACTIONID", tICKET.TRANSACTIONID);
            ViewBag.USERID = new SelectList(db.TRANSACTIONS, "USERID", "USERID");
            return View(tICKET);
        }

        // displays the element with the specific id to be deleted
        // GET: Tickets/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TICKET tICKET = db.TICKETs.Find(id);
            if (tICKET == null)
            {
                return HttpNotFound();
            }
            return View(tICKET);
        }

        // deletes the specific element and its values and saves changes
        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            TICKET tICKET = db.TICKETs.Find(id);
            db.TICKETs.Remove(tICKET);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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
