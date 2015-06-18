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
            return RedirectToAction("Create", "Households");
        }

        // GET: Households
        [RequireHousehold]
        public ActionResult Index()
        {
            var household = db.Households.Find(Int32.Parse(User.Identity.GetHouseholdId()));
            return View(household);
        }

        // GET: Households/Details/5
        [RequireHousehold]
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
        [RequireHousehold]
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
        [RequireHousehold]
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
            if (household.Users.Count() == 1)
            {
                return View(household);
            }
            return RedirectToAction("Index");
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

        public async Task<ActionResult> SendInvitation(string ToEmail)
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            var hId = Int32.Parse(User.Identity.GetHouseholdId());
            var charSet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var rndm = new Random();
            var code = new string(Enumerable.Range(1,6).Select(n => charSet[rndm.Next(charSet.Length -1)]).ToArray());
            var invite = new Invitation()
            {
                HouseholdId = hId,
                ToEmail = ToEmail,
                FromUserId = User.Identity.GetUserId(),
                Code = code
            };

            db.Invitaions.Add(invite);
            db.SaveChanges();
            await ControllerContext.HttpContext.RefreshAuthentication(user);
            var mailer = new EmailService();
            mailer.Send(new IdentityMessage() 
            {
                Destination = ToEmail,
                Subject = "Join " + user.FirstName + " " + user.LastName + "'s household",
                Body = "You have been invited to join a household by " + user.FirstName + " " + user.LastName + ". Go to http://www.ewaked-budgetboss.azurewebsites.net/households/join or login and click join household. Use the following code to join the household after registering or logging in. " + code
                
            });

            return RedirectToAction("Dashboard", "Home");
        }

        [HttpGet]
        public ActionResult Join()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Join (string code)
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            var invite = db.Invitaions.Where(i => i.Code == code && i.ToEmail == user.Email).FirstOrDefault();

            if (invite != null)
            {
                user.HouseholdId = invite.HouseholdId;
                db.SaveChanges();
                await HttpContext.RefreshAuthentication(user);
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("Code", "Invalid Code");

            return View(new Invitation() { Code = code });
        }
    }
}
