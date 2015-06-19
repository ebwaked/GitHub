using BudgetBoss.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Threading.Tasks;

namespace BudgetBoss.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }
        
        [RequireHousehold]
        public ActionResult Dashboard()
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            var householdId = db.Households.Find(user.HouseholdId);

            var model = new DashboardViewModel()
            {
                HouseholdAccounts = householdId.HouseholdAccounts.ToList(),
                Transactions = (from account in householdId.HouseholdAccounts
                                    from transaciton in account.Transactions
                                    select transaciton).ToList()
            };
            return View(model);
        }

        public ActionResult GettingStarted()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> GetChartData()
        {
            var user = db.Users.Find(User.Identity.GetUserId());

            var data = (from c in user.Household.Categories
                         select new
                         {
                             Name = c.Name,
                             ActualAmount = c.Transactions.Select(t => t.Amount).DefaultIfEmpty().Sum(),
                             BudgetAmount = c.BudgetItems.Select(t => t.Amount).DefaultIfEmpty().Sum()
                         }).ToArray();

            var data2 = (from c in user.Household.Categories
                         select new
                        {
                            label = c.Name,
                            value = c.Transactions.Where( ct => ct.Amount > 0).Select(t => t.Amount).DefaultIfEmpty().Sum(),
                        }).ToArray();

            var data3 = (from c in user.Household.HouseholdAccounts
                         select new
                         {
                             label = c.Balance,
                             value = c.Household.HouseholdAccounts.Where(d => d.Balance > 0).Select(t => t.Balance).DefaultIfEmpty().Sum(),
                         }).ToArray();

            var allCharts = new
            {
                bar = data,
                donut = data2
            };

            await HttpContext.RefreshAuthentication(user);

            return Json(data2);
        }
    }
}