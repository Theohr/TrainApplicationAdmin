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
    public class TrainsController : Controller
    {
        private Entities1 db = new Entities1();

        /// <summary>
        ///  index displaying the table and using the search bar to display a specific element the user searches for
        /// </summary>
        /// <param name="searchBy"></param> id of the search selection
        /// <param name="search"></param> the user's input search value
        /// <returns></returns>
        // GET: Trains
        public ActionResult Index(string searchBy, string search)
        {
            if (searchBy == "TrainID")
                {
                    return View(db.TRAINs.Where(x => x.TRAINID.ToString().Contains(search) || search == null).ToList());
                }
                else
                {
                    return View(db.TRAINs.ToList());
                }
        }

        /// <summary>
        /// searches and displays the account in the details view
        /// </summary>
        /// <param name="id"></param> id of the train
        /// <returns></returns>
        // GET: Trains/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TRAIN tRAIN = db.TRAINs.Find(id);
            if (tRAIN == null)
            {
                return HttpNotFound();
            }
            return View(tRAIN);
        }

        /// <summary>
        /// on create button click goes through a validation so the unique id should be generated then returns the id and displays it on the create page
        /// </summary>
        /// <returns></returns>
        // GET: Trains/Create
        public ActionResult Create()
        {
            TRAIN newTrain = new TRAIN();

            var lastTrain = db.TRAINs.OrderByDescending(x => x.TRAINID).FirstOrDefault();

            if (lastTrain == null)
            {
                newTrain.TRAINID = 3001;
            }
            else if (lastTrain.TRAINID != 0)
            {
                newTrain.TRAINID = lastTrain.TRAINID + 1;
            }

            return View(newTrain);
        }

        /// <summary>
        /// using forgery token for seciurity
        /// adds the new train to the database saves changes and displays it on the table redirecting to the train table
        /// </summary>
        /// <param name="tRAIN"></param> 
        /// <returns></returns>
        // POST: Trains/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TRAINID,COACH_FIRST,COACH_STANDARD,STATUS,TOTALSEATS")] TRAIN tRAIN)
        {
            if (ModelState.IsValid)
            {
                db.TRAINs.Add(tRAIN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tRAIN);
        }

        /// <summary>
        /// takes the specific element to edit 
        /// </summary>
        /// <param name="id"></param> gets the id
        /// <returns></returns>
        // GET: Trains/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TRAIN tRAIN = db.TRAINs.Find(id);
            if (tRAIN == null)
            {
                return HttpNotFound();
            }
            return View(tRAIN);
        }

        /// <summary>
        /// saves changes when the element is edited
        /// </summary>
        /// <param name="tRAIN"></param>
        /// <returns></returns>
        // POST: Trains/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TRAINID,COACH_FIRST,COACH_STANDARD,STATUS,TOTALSEATS")] TRAIN tRAIN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tRAIN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tRAIN);
        }

        /// <summary>
        /// searches for the id to display the element on the delete page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Trains/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TRAIN tRAIN = db.TRAINs.Find(id);
            if (tRAIN == null)
            {
                return HttpNotFound();
            }
            return View(tRAIN);
        }

        /// <summary>
        /// deletes the specific element with the id it had
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // POST: Trains/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            TRAIN tRAIN = db.TRAINs.Find(id);
            db.TRAINs.Remove(tRAIN);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // disposes database to free the resources
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
