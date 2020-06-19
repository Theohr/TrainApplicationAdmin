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
    public class JourneysController : Controller
    {
        private Entities1 db = new Entities1();

        // GET: Journeys
        public ActionResult Index(string searchBy, string search)
        {
            var jOURNEYs = db.JOURNEYs.Include(j => j.STATION).Include(j => j.STATION1);

            if (searchBy == "JourneyID")
            {
                return View(db.JOURNEYs.Where(j => j.JOURNEYID.ToString().Contains(search) || search == null).ToList());
            }
            else
            {
                return View(db.JOURNEYs.ToList());
            }
        }

        // GET: Journeys/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JOURNEY jOURNEY = db.JOURNEYs.Find(id);
            if (jOURNEY == null)
            {
                return HttpNotFound();
            }
            return View(jOURNEY);
        }

        // GET: Journeys/Create
        public ActionResult Create()
        {
            ViewBag.ARRIVAL = new SelectList(db.STATIONs, "STATIONID", "STATIONNAME");
            ViewBag.DEPARTURE = new SelectList(db.STATIONs, "STATIONID", "STATIONNAME");
            JOURNEY newJourney = new JOURNEY();

            var lastJourney = db.JOURNEYs.OrderByDescending(x => x.JOURNEYID).FirstOrDefault();

            if (lastJourney == null)
            {
                newJourney.JOURNEYID = 6001;
            }
            else if (lastJourney.JOURNEYID != 0)
            {
                newJourney.JOURNEYID = lastJourney.JOURNEYID + 1;
            }

            return View(newJourney);
        }

        // POST: Journeys/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JOURNEYID,DEPARTURE,ARRIVAL,DEPARTURETIME,ARRIVALTIME")] JOURNEY jOURNEY)
        {
            if (ModelState.IsValid)
            {
                db.JOURNEYs.Add(jOURNEY);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ARRIVAL = new SelectList(db.STATIONs, "STATIONID", "STATIONNAME", jOURNEY.ARRIVAL);
            ViewBag.DEPARTURE = new SelectList(db.STATIONs, "STATIONID", "STATIONNAME", jOURNEY.DEPARTURE);
            return View(jOURNEY);
        }

        // GET: Journeys/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JOURNEY jOURNEY = db.JOURNEYs.Find(id);
            if (jOURNEY == null)
            {
                return HttpNotFound();
            }
            ViewBag.ARRIVAL = new SelectList(db.STATIONs, "STATIONID", "STATIONNAME", jOURNEY.ARRIVAL);
            ViewBag.DEPARTURE = new SelectList(db.STATIONs, "STATIONID", "STATIONNAME", jOURNEY.DEPARTURE);
            return View(jOURNEY);
        }

        // POST: Journeys/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JOURNEYID,DEPARTURE,ARRIVAL,DEPARTURETIME,ARRIVALTIME")] JOURNEY jOURNEY)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jOURNEY).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ARRIVAL = new SelectList(db.STATIONs, "STATIONID", "STATIONNAME", jOURNEY.ARRIVAL);
            ViewBag.DEPARTURE = new SelectList(db.STATIONs, "STATIONID", "STATIONNAME", jOURNEY.DEPARTURE);
            return View(jOURNEY);
        }

        // GET: Journeys/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JOURNEY jOURNEY = db.JOURNEYs.Find(id);
            if (jOURNEY == null)
            {
                return HttpNotFound();
            }
            return View(jOURNEY);
        }

        // POST: Journeys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            JOURNEY jOURNEY = db.JOURNEYs.Find(id);
            db.JOURNEYs.Remove(jOURNEY);
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
