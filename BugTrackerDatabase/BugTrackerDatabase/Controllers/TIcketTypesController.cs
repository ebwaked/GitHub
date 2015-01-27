using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BugTrackerDatabase.Models;

namespace BugTrackerDatabase.Controllers
{
    public class TIcketTypesController : Controller
    {
        private BTEntities db = new BTEntities();

        // GET: TIcketTypes
        public ActionResult Index()
        {
            return View(db.TIcketTypes.ToList());
        }

        // GET: TIcketTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIcketType tIcketType = db.TIcketTypes.Find(id);
            if (tIcketType == null)
            {
                return HttpNotFound();
            }
            return View(tIcketType);
        }

        // GET: TIcketTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TIcketTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] TIcketType tIcketType)
        {
            if (ModelState.IsValid)
            {
                db.TIcketTypes.Add(tIcketType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tIcketType);
        }

        // GET: TIcketTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIcketType tIcketType = db.TIcketTypes.Find(id);
            if (tIcketType == null)
            {
                return HttpNotFound();
            }
            return View(tIcketType);
        }

        // POST: TIcketTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] TIcketType tIcketType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tIcketType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tIcketType);
        }

        // GET: TIcketTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIcketType tIcketType = db.TIcketTypes.Find(id);
            if (tIcketType == null)
            {
                return HttpNotFound();
            }
            return View(tIcketType);
        }

        // POST: TIcketTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TIcketType tIcketType = db.TIcketTypes.Find(id);
            db.TIcketTypes.Remove(tIcketType);
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
