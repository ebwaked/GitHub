﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using BudgetBoss.Models;
using System.Threading.Tasks;


namespace BudgetBoss.Controllers
{
    [RequireHousehold]
    public class HouseholdAccountsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: HouseholdAccounts
        public ActionResult Index()
        {
            var householdId = Int32.Parse(User.Identity.GetHouseholdId());
            var householdAccounts = db.HouseholdAccounts.Include(h => h.Household).Where(c => c.HouseholdId == householdId);
            return View(householdAccounts.ToList());
        }

        // GET: HouseholdAccounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HouseholdAccount householdAccount = db.HouseholdAccounts.Find(id);
            if (householdAccount == null)
            {
                return HttpNotFound();
            }
            return View(householdAccount);
        }

        // GET: HouseholdAccounts/Create
        public ActionResult Create()
        {
            ViewBag.HouseholdId = new SelectList(db.Households, "Id", "Name");
            return View();
        }

        // POST: HouseholdAccounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Balance,ReconciledBalance")] HouseholdAccount householdAccount)
        {
            if (ModelState.IsValid)
            {
                householdAccount.HouseholdId = Int32.Parse(User.Identity.GetHouseholdId());
                db.HouseholdAccounts.Add(householdAccount);
                var user = db.Users.Find(User.Identity.GetUserId());
                db.SaveChanges();
                await ControllerContext.HttpContext.RefreshAuthentication(user);
                return RedirectToAction("Index");
            }

            ViewBag.HouseholdId = new SelectList(db.Households, "Id", "Name", householdAccount.HouseholdId);
            return View(householdAccount);
        }

        // GET: HouseholdAccounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HouseholdAccount householdAccount = db.HouseholdAccounts.Find(id);
            if (householdAccount == null)
            {
                return HttpNotFound();
            }
            ViewBag.HouseholdId = new SelectList(db.Households, "Id", "Name", householdAccount.HouseholdId);
            return View(householdAccount);
        }

        // POST: HouseholdAccounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Balance,ReconciledBalance,HouseholdId")] HouseholdAccount householdAccount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(householdAccount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HouseholdId = new SelectList(db.Households, "Id", "Name", householdAccount.HouseholdId);
            return View(householdAccount);
        }

        // GET: HouseholdAccounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HouseholdAccount householdAccount = db.HouseholdAccounts.Find(id);
            if (householdAccount == null)
            {
                return HttpNotFound();
            }
            return View(householdAccount);
        }

        // POST: HouseholdAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HouseholdAccount householdAccount = db.HouseholdAccounts.Find(id);
            db.HouseholdAccounts.Remove(householdAccount);
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
