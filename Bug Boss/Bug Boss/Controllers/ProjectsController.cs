﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bug_Boss.Models;

namespace Bug_Boss.Controllers
{
    [Authorize(Roles = "Submitter,Developer,Administrator,Project Manager")]
    public class ProjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Projects
        public ActionResult Index()
        {
            return View(db.Projects.ToList());
        }

        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

         
        // GET: Projects/Create
        [Authorize(Roles = "Administrator,Project Manager")]
        public ActionResult Create()
        {
            return View();
        }

        
        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Administrator,Project Manager")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Created")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Projects.Add(project);
                project.Created = System.DateTimeOffset.Now;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            
            return View(project);
        }

        // GET: Projects/Edit/5
        [Authorize(Roles = "Administrator,Project Manager")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            var model = new ProjectViewModel { Id = project.Id, Name = project.Name };
            model.PossibleUsersToRemove = new MultiSelectList(project.ApplicationUsers.ToList(), "Id", "UserName");
            model.PossibleUsersToAssign = new MultiSelectList(db.Users.Where(u => !u.Projects.Any(p => p.Id == id)).ToList(), "Id", "UserName");

            return View(model);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrator,Project Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Created,Updated")]Project id, ProjectViewModel model)
        {
            var project = db.Projects.Find(model.Id);
            if (model.NewlyRemovedUsers != null)
            {
                foreach (var userId in model.NewlyRemovedUsers)
                {
                    var tempUser = new ApplicationUser();
                    tempUser.Id = userId;
                    db.Users.Attach(tempUser);
                    project.ApplicationUsers.Remove(tempUser);
                }
            }

            if (model.NewlyAssignedUsers != null)
            {
                foreach (var userId in model.NewlyAssignedUsers)
                {
                    var tempUser = new ApplicationUser();
                    tempUser.Id = userId;
                    db.Users.Attach(tempUser);
                    project.ApplicationUsers.Add(tempUser);
                }
            }

            project.Updated = DateTimeOffset.Now;
            db.SaveChanges();
            return RedirectToAction("Edit", new { Id = model.Id });

        }

        // GET: Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
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
