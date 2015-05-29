using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using BudgetBoss.Models;
using System.Threading.Tasks;

namespace BudgetBoss.Controllers
{
    [Authorize]
    public class HouseholdsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LeaveHousehold()
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            user.HouseholdId = null;
            db.SaveChanges();
            await ControllerContext.HttpContext.RefreshAuthentication(user);
            return RedirectToAction("Create", "Household");
        }

        // GET: Households
        public ActionResult Index()
        {
            return View(db.Households.ToList());
        }

        // GET: Households/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Household household = db.Households.Find(id);
            if (household == null)
            {
                return HttpNotFound();
            }
            return View(household);
        }

        // GET: Households/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Households/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name")] Household household)
        {
            if (ModelState.IsValid)
            {
                db.Households.Add(household);
                db.SaveChanges();
                var user = db.Users.Find(User.Identity.GetUserId());
                user.HouseholdId = household.Id;
                db.SaveChanges();
                // Basic Categories for Household
                var food = new Category
                {
                    Name = "Food",
                    HouseholdId = household.Id
                };
                db.Categories.Add(food);
                var travel = new Category
                {   
                    Name = "Travel",
                    HouseholdId = household.Id
                };
                db.Categories.Add(travel);
                var rent = new Category
                {
                    Name = "Rent",
                    HouseholdId = household.Id
                };
                db.Categories.Add(rent);
                var mortgage = new Category
                {
                    Name = "Mortgage",
                    HouseholdId = household.Id
                };
                db.Categories.Add(mortgage);
                var education = new Category
                {
                    Name = "Education",
                    HouseholdId = household.Id
                };
                db.Categories.Add(education);
                var miscExprenses = new Category
                {
                    Name = "Misc. Expenses",
                    HouseholdId = household.Id
                };
                db.Categories.Add(miscExprenses);
                var savings = new Category
                {
                    Name = "Savings",
                    HouseholdId = household.Id
                };
                db.Categories.Add(savings);
                var medical = new Category
                {
                    Name = "Medical",
                    HouseholdId = household.Id
                };
                db.Categories.Add(medical);
                var childCare = new Category
                {
                    Name = "Child Care",
                    HouseholdId = household.Id
                };
                db.Categories.Add(childCare);
                db.SaveChanges();
                await ControllerContext.HttpContext.RefreshAuthentication(user);
                return RedirectToAction("Index");
            }

            return View(household);
        }

        // GET: Households/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Household household = db.Households.Find(id);
            if (household == null)
            {
                return HttpNotFound();
            }
            return View(household);
        }

        // POST: Households/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Household household)
        {
            if (ModelState.IsValid)
            {
                db.Entry(household).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(household);
        }

        // GET: Households/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Household household = db.Households.Find(id);
            if (household == null)
            {
                return HttpNotFound();
            }
            return View(household);
        }

        // POST: Households/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Household household = db.Households.Find(id);
            db.Households.Remove(household);
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
