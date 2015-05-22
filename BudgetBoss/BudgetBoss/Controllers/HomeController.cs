using BudgetBoss.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BudgetBoss.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        
        [RequireHousehold]
        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult GettingStarted()
        {
            return View();
        }
    }
}