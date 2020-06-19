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
    public class SchedulesController : Controller
    {
        private Entities1 db = new Entities1();

        // GET: Schedules
        public ActionResult Index(string searchBy, string search)
        {
            var sCHEDULEs = db.SCHEDULEs.Include(s => s.ROUTE).Include(s => s.TRAIN);

            if (searchBy == "ROUTEID")
            {
                return View(db.SCHEDULEs.Where(s => s.ROUTEID.ToString().Contains(search) || search == null).ToList());
            }
            else
            {
                return View(sCHEDULEs.ToList());
            }
           
        }

        // GET: Schedules/Details/5
        public ActionResult Details(decimal id, decimal idsch)
        {
            if (id == null && idsch == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SCHEDULE sCHEDULE = db.SCHEDULEs.Find(id, idsch);
            if (sCHEDULE == null)
            {
                return HttpNotFound();
            }
            return View(sCHEDULE);
        }

        // GET: Schedules/Create
        public ActionResult Create()
        {
            ViewBag.ROUTEID = new SelectList(db.ROUTEs, "ROUTEID", "ROUTEID");
            ViewBag.TRAINID = new SelectList(db.TRAINs, "TRAINID", "TRAINID");
            return View();
        }

        // POST: Schedules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ROUTEID,TRAINID,DEPARTURETIME,ARRIVALTIME")] SCHEDULE sCHEDULE)
        {
            if (ModelState.IsValid)
            {
                db.SCHEDULEs.Add(sCHEDULE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ROUTEID = new SelectList(db.ROUTEs, "ROUTEID", "ROUTEID", sCHEDULE.ROUTEID);
            ViewBag.TRAINID = new SelectList(db.TRAINs, "TRAINID", "TRAINID", sCHEDULE.TRAINID);
            return View(sCHEDULE);
        }

        // GET: Schedules/Edit/5
        public ActionResult Edit(decimal id, decimal idsch)
        {
            if (id == null && idsch == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SCHEDULE sCHEDULE = db.SCHEDULEs.Find(id, idsch);
            if (sCHEDULE == null)
            {
                return HttpNotFound();
            }
            ViewBag.ROUTEID = new SelectList(db.ROUTEs, "ROUTEID", "ROUTEID", sCHEDULE.ROUTEID);
            ViewBag.TRAINID = new SelectList(db.TRAINs, "TRAINID", "TRAINID", sCHEDULE.TRAINID);
            return View(sCHEDULE);
        }

        // POST: Schedules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ROUTEID,TRAINID,DEPARTURETIME,ARRIVALTIME")] SCHEDULE sCHEDULE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sCHEDULE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ROUTEID = new SelectList(db.ROUTEs, "ROUTEID", "ROUTEID", sCHEDULE.ROUTEID);
            ViewBag.TRAINID = new SelectList(db.TRAINs, "TRAINID", "TRAINID", sCHEDULE.TRAINID);
            return View(sCHEDULE);
        }

        // GET: Schedules/Delete/5
        public ActionResult Delete(decimal id, decimal idsch)
        {
            if (id == null && idsch == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SCHEDULE sCHEDULE = db.SCHEDULEs.Find(id, idsch);
            if (sCHEDULE == null)
            {
                return HttpNotFound();
            }
            return View(sCHEDULE);
        }

        // POST: Schedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id, decimal idsch)
        {
            SCHEDULE sCHEDULE = db.SCHEDULEs.Find(id, idsch);
            db.SCHEDULEs.Remove(sCHEDULE);
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
