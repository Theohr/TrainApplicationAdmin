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
    public class StationsController : Controller
    {
        private Entities1 db = new Entities1();

        // GET: Stations
        public ActionResult Index(string searchBy, string search)
        {

            if (searchBy == "StationName")
            {
                return View(db.STATIONs.Where(x => x.STATIONNAME.Contains(search) || search == null).ToList());
            }
            else
            {
                return View(db.STATIONs.ToList());
            }
        }

        // GET: Stations/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            STATION sTATION = db.STATIONs.Find(id);
            if (sTATION == null)
            {
                return HttpNotFound();
            }
            return View(sTATION);
        }

        // GET: Stations/Create
        public ActionResult Create()
        {
            STATION newStation = new STATION();

            var lastStation = db.STATIONs.OrderByDescending(x => x.STATIONID).FirstOrDefault();

            if (lastStation == null)
            {
                newStation.STATIONID = 4001;
            }
            else if (lastStation.STATIONID != 0)
            {
                newStation.STATIONID = lastStation.STATIONID + 1;
            }

            return View(newStation);
        }

        // POST: Stations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "STATIONID,STATIONNAME,PLATFORMS")] STATION sTATION)
        {
            if (ModelState.IsValid)
            {
                db.STATIONs.Add(sTATION);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sTATION);
        }

        // GET: Stations/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            STATION sTATION = db.STATIONs.Find(id);
            if (sTATION == null)
            {
                return HttpNotFound();
            }
            return View(sTATION);
        }

        // POST: Stations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "STATIONID,STATIONNAME,PLATFORMS")] STATION sTATION)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sTATION).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sTATION);
        }

        // GET: Stations/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            STATION sTATION = db.STATIONs.Find(id);
            if (sTATION == null)
            {
                return HttpNotFound();
            }
            return View(sTATION);
        }

        // POST: Stations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            STATION sTATION = db.STATIONs.Find(id);
            db.STATIONs.Remove(sTATION);
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
