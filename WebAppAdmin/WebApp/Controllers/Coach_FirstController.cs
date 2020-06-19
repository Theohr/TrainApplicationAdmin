using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

// This is the Coach First Folder Controller. Where every function for the Index, Create, Delete, Edit, Details is done.
namespace WebApp.Controllers
{
    public class Coach_FirstController : Controller
    {
        private Entities1 db = new Entities1(); // we create a 

        // GET: This is where we get the coach first table from the database and display it on the screen for the user to view
        // by passing inside the parameters for the search value so the user can also search in case they want to find something specific
        public ActionResult Index(string searchBy, string search)
        {
            var cOACH_FIRST = db.COACH_FIRST.Include(c => c.TRAIN); // create a variable to include the train table aswell

            // searching by Train ID
            if (searchBy == "CoachNO")
            {
                return View(db.COACH_FIRST.Where(c => c.COACHNO.ToString().Contains(search) || search == null).ToList()); // returning the table with the search of the user
            }
            else
            {
                return View(db.COACH_FIRST.ToList()); // returning the table as it is 
            }
            
        }

        // GET: Coach_First/Details/5
        public ActionResult Details(int id, int idcoach)
        {
            if (id == null && idcoach == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COACH_FIRST cOACH_FIRST = db.COACH_FIRST.Find(id, idcoach);
            if (cOACH_FIRST == null)
            {
                return HttpNotFound();
            }
            return View(cOACH_FIRST);
        }

        // GET: Coach_First/Create
        public ActionResult Create()
        {
            ViewBag.TRAINID = new SelectList(db.TRAINs, "TRAINID", "TRAINID");
            return View();
        }

        // POST: Coach_First/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TRAINID,COACHNO,SEATING")] COACH_FIRST cOACH_FIRST)
        {
            if (ModelState.IsValid)
            {
                db.COACH_FIRST.Add(cOACH_FIRST);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TRAINID = new SelectList(db.TRAINs, "TRAINID", "TRAINID", cOACH_FIRST.TRAINID);
            return View(cOACH_FIRST);
        }

        // GET: Coach_First/Edit/5
        public ActionResult Edit(int id, int idcoach)
        {
            if (id == null && idcoach == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COACH_FIRST cOACH_FIRST = db.COACH_FIRST.Find(id, idcoach);
            if (cOACH_FIRST == null)
            {
                return HttpNotFound();
            }
            ViewBag.TRAINID = new SelectList(db.TRAINs, "TRAINID", "TRAINID", cOACH_FIRST.TRAINID);
            return View(cOACH_FIRST);
        }

        // POST: Coach_First/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TRAINID,COACHNO,SEATING")] COACH_FIRST cOACH_FIRST)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cOACH_FIRST).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TRAINID = new SelectList(db.TRAINs, "TRAINID", "TRAINID", cOACH_FIRST.TRAINID);
            return View(cOACH_FIRST);
        }

        // GET: Coach_First/Delete/5
        public ActionResult Delete(int id, int idcoach)
        {
            if (id == null && idcoach == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COACH_FIRST cOACH_FIRST = db.COACH_FIRST.Find(id, idcoach);
            if (cOACH_FIRST == null)
            {
                return HttpNotFound();
            }
            return View(cOACH_FIRST);
        }

        // POST: Coach_First/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int idcoach)
        {
            COACH_FIRST cOACH_FIRST = db.COACH_FIRST.Find(id, idcoach);
            db.COACH_FIRST.Remove(cOACH_FIRST);
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
