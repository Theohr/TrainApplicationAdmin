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
    public class Coach_StandardController : Controller
    {
        private Entities1 db = new Entities1();

        // GET: Coach_Standard
        public ActionResult Index(string searchBy, string search)
        {
            var cOACH_STANDARD = db.COACH_STANDARD.Include(c => c.TRAIN);

            if (searchBy == "CoachNO")
            {
                return View(db.COACH_STANDARD.Where(c => c.COACHNO.ToString().Contains(search) || search == null).ToList());
            }
            else
            {
                return View(db.COACH_STANDARD.ToList());
            }
        }

        // GET: Coach_Standard/Details/5
        public ActionResult Details(decimal id, decimal idcoach)
        {
            if (id == null && idcoach == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COACH_STANDARD cOACH_STANDARD = db.COACH_STANDARD.Find(id, idcoach);
            if (cOACH_STANDARD == null)
            {
                return HttpNotFound();
            }
            return View(cOACH_STANDARD);
        }

        // GET: Coach_Standard/Create
        public ActionResult Create()
        {
            ViewBag.TRAINID = new SelectList(db.TRAINs, "TRAINID", "TRAINID");
            return View();
        }

        // POST: Coach_Standard/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TRAINID,COACHNO,WINDOWSEATS,AISLESEATS,TABLESEATS")] COACH_STANDARD cOACH_STANDARD)
        {
            if (ModelState.IsValid)
            {
                db.COACH_STANDARD.Add(cOACH_STANDARD);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TRAINID = new SelectList(db.TRAINs, "TRAINID", "STATUS", cOACH_STANDARD.TRAINID);
            return View(cOACH_STANDARD);
        }

        // GET: Coach_Standard/Edit/5
        public ActionResult Edit(decimal id, decimal idcoach)
        {
            if (id == null && idcoach == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COACH_STANDARD cOACH_STANDARD = db.COACH_STANDARD.Find(id, idcoach);
            if (cOACH_STANDARD == null)
            {
                return HttpNotFound();
            }
            ViewBag.TRAINID = new SelectList(db.TRAINs, "TRAINID", "TRAINID", cOACH_STANDARD.TRAINID);
            return View(cOACH_STANDARD);
        }

        // POST: Coach_Standard/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TRAINID,COACHNO,WINDOWSEATS,AISLESEATS,TABLESEATS")] COACH_STANDARD cOACH_STANDARD)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cOACH_STANDARD).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TRAINID = new SelectList(db.TRAINs, "TRAINID", "TRAINID", cOACH_STANDARD.TRAINID);
            return View(cOACH_STANDARD);
        }

        // GET: Coach_Standard/Delete/5
        public ActionResult Delete(decimal id, decimal idcoach)
        {
            if (id == null && idcoach == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COACH_STANDARD cOACH_STANDARD = db.COACH_STANDARD.Find(id, idcoach);
            if (cOACH_STANDARD == null)
            {
                return HttpNotFound();
            }
            return View(cOACH_STANDARD);
        }

        // POST: Coach_Standard/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id, decimal idcoach)
        {
            COACH_STANDARD cOACH_STANDARD = db.COACH_STANDARD.Find(id, idcoach);
            db.COACH_STANDARD.Remove(cOACH_STANDARD);
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
