using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BugTracker.Models;

namespace BugTracker.Controllers
{
    public class ProjectUsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ProjectUsers
        public ActionResult Index()
        {
            var projectUsers = db.ProjectUsers.Include(p => p.Project).Include(p => p.User);
            return View(projectUsers.ToList());
        }

        // GET: ProjectUsers/Assign
        public ActionResult Assign()
        {
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name");
            ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Email");
            return View();
        }

        // POST: ProjectUsers/Assign
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Assign([Bind(Include = "Id,ProjectId,UserId")] ProjectUser projectUser)
        {
            if (ModelState.IsValid)
            {
                db.ProjectUsers.Add(projectUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", projectUser.ProjectId);
            ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Email", projectUser.UserId);
            return View(projectUser);
        }

        // GET: ProjectUsers/Unassign/5
        public ActionResult Unassign(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectUser projectUser = db.ProjectUsers.Find(id);
            if (projectUser == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", projectUser.ProjectId);
            ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Email", projectUser.UserId);
            return View(projectUser);
        }

        // POST: ProjectUsers/Unassign/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Unassign([Bind(Include = "Id,ProjectId,UserId")] ProjectUser projectUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(projectUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", projectUser.ProjectId);
            ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Email", projectUser.UserId);
            return View(projectUser);
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
