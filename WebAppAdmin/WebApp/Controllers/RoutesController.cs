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
    public class RoutesController : Controller
    {
        private Entities1 db = new Entities1();

        // GET: Routes
        public ActionResult Index(string searchBy, string search)
        { 
            var rOUTEs = db.ROUTEs.Include(r => r.STATION).Include(r => r.STATION1).Include(r => r.STATION2);

            if (searchBy == "Departure")
            {
                return View(db.ROUTEs.Where(r => r.STATION.STATIONNAME.Contains(search) || search == null).ToList());
            }
            else
            {
                return View(db.ROUTEs.ToList());
            }
        }

        // GET: Routes/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ROUTE rOUTE = db.ROUTEs.Find(id);
            if (rOUTE == null)
            {
                return HttpNotFound();
            }
            return View(rOUTE);
        }

        // GET: Routes/Create
        public ActionResult Create()
        {
            ViewBag.DEPARTURE = new SelectList(db.STATIONs, "STATIONID", "STATIONNAME");
            ViewBag.DESTINATION = new SelectList(db.STATIONs, "STATIONID", "STATIONNAME");
            ViewBag.FIRSTSTOP = new SelectList(db.STATIONs, "STATIONID", "STATIONNAME");
            ROUTE newRoute = new ROUTE();

            var lastRoute = db.ROUTEs.OrderByDescending(x => x.ROUTEID).FirstOrDefault();

            if (lastRoute == null)
            {
                newRoute.ROUTEID = 5001;
            }
            else if (lastRoute.ROUTEID != 0)
            {
                newRoute.ROUTEID = lastRoute.ROUTEID + 1;
            }

            return View(newRoute);
        }

        // POST: Routes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ROUTEID,DEPARTURE,FIRSTSTOP,DESTINATION")] ROUTE rOUTE)
        {
            if (ModelState.IsValid)
            {
                db.ROUTEs.Add(rOUTE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DEPARTURE = new SelectList(db.STATIONs, "STATIONID", "STATIONNAME", rOUTE.DEPARTURE);
            ViewBag.DESTINATION = new SelectList(db.STATIONs, "STATIONID", "STATIONNAME", rOUTE.DESTINATION);
            ViewBag.FIRSTSTOP = new SelectList(db.STATIONs, "STATIONID", "STATIONNAME", rOUTE.FIRSTSTOP);
            return View(rOUTE);
        }

        // GET: Routes/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ROUTE rOUTE = db.ROUTEs.Find(id);
            if (rOUTE == null)
            {
                return HttpNotFound();
            }
            ViewBag.DEPARTURE = new SelectList(db.STATIONs, "STATIONID", "STATIONNAME", rOUTE.DEPARTURE);
            ViewBag.DESTINATION = new SelectList(db.STATIONs, "STATIONID", "STATIONNAME", rOUTE.DESTINATION);
            ViewBag.FIRSTSTOP = new SelectList(db.STATIONs, "STATIONID", "STATIONNAME", rOUTE.FIRSTSTOP);
            return View(rOUTE);
        }

        // POST: Routes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ROUTEID,DEPARTURE,FIRSTSTOP,DESTINATION")] ROUTE rOUTE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rOUTE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DEPARTURE = new SelectList(db.STATIONs, "STATIONID", "STATIONNAME", rOUTE.DEPARTURE);
            ViewBag.DESTINATION = new SelectList(db.STATIONs, "STATIONID", "STATIONNAME", rOUTE.DESTINATION);
            ViewBag.FIRSTSTOP = new SelectList(db.STATIONs, "STATIONID", "STATIONNAME", rOUTE.FIRSTSTOP);
            return View(rOUTE);
        }

        // GET: Routes/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ROUTE rOUTE = db.ROUTEs.Find(id);
            if (rOUTE == null)
            {
                return HttpNotFound();
            }
            return View(rOUTE);
        }

        // POST: Routes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            ROUTE rOUTE = db.ROUTEs.Find(id);
            db.ROUTEs.Remove(rOUTE);
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
