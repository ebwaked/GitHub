using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BudgetBoss.Models;

namespace BudgetBoss.Controllers
{
    [RequireHousehold]
    public class TransactionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Transactions
        [Route("~/Accounts/{accountId}/Transactions", Name="transaction")] 
        public ActionResult Index(int accountId)
        {
            ViewBag.HouseholdAccountId = accountId; 
            var transactions = db.Transactions.Include(t => t.Category);
            return View(transactions.ToList());
        }

        //Search for Transactions 
        public ActionResult Index(int? page, string query)
        {
            var transactions = db.Transactions.AsQueryable();
            if (!String.IsNullOrWhiteSpace(query))
            {
                transactions = transactions.Where(p => p.Category.Equals(query) || p.Amount.Equals(query) || 
                    p.AbsAmount.Equals(query) || p.AbsReconciledAmount.Equals(query) || p.Description.Contains(query) 
                    || p.ReconciledAmount.Equals(query));

                ViewBag.Query = query;
            }
            return View(transactions.ToList());
        }

        // GET: Transactions/Details/5
        [Route("~/Accounts/{accountId}/Transactions/{transactionId}", Name = "transactionDetails")]
        public ActionResult Details(int? id, int accountId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // GET: Transactions/Create
        [Route("~/Accounts/{accountId}/Transactions/Create", Name = "transactionCreate")]
        public ActionResult Create(int accountId)
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            ViewBag.HouseholdAccountId = accountId; 

            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("~/Accounts/{accountId}/Transactions/Create")]
        public ActionResult Create([Bind(Include = "Id,HouseholdAccountId,Amount,AbsAmount,ReconciledAmount,AbsReconciledAmount,Date,Description,Updated,UpdatedUserId,CategoryId")] Transaction transaction, int accountId)
        {
            if (ModelState.IsValid)
            {
                db.Transactions.Add(transaction);
                var account = db.HouseholdAccounts.Find(transaction.HouseholdAccountId);
                account.Balance += transaction.Amount;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", transaction.CategoryId);
            return View(transaction);
        }

        // GET: Transactions/Edit/5
        [Route("~/Accounts/{accountId}/Transactions/Edit/{transactionId}")]
        public ActionResult Edit(int? transactionId, int accountId)
        {
            if (transactionId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(transactionId);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", transaction.CategoryId);
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("~/Accounts/{accountId}/Transactions/Edit/{transactionId}")]
        public ActionResult Edit([Bind(Include = "Id,HouseholdAccountId,Amount,AbsAmount,ReconciledAmount,AbsReconciledAmount,Date,Description,Updated,UpdatedUserId,CategoryId")] Transaction transaction, int accountId)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaction).State = EntityState.Modified;

                var account = db.HouseholdAccounts.Find(transaction.HouseholdAccountId);

                account.Balance = (from t in db.Transactions
                                   where t.HouseholdAccountId == account.Id
                                   select t.Amount).Sum();
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", transaction.CategoryId);
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        [Route("~/Accounts/{accountId}/Transactions/Delete/{transactionId}")]
        public ActionResult Delete(int? transactionId, int accountId)
        {
            if (transactionId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(transactionId);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("~/Accounts/{accountId}/Transactions/Delete/{transactionId}")]
        public ActionResult DeleteConfirmed(int transactionId, int accountId)
        {
            Transaction transaction = db.Transactions.Find(transactionId);
            db.Transactions.Remove(transaction);
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
