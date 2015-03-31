using SendGrid;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using Bug_Boss.Models;

namespace Bug_Boss.Controllers
{
    public class HomeController : Controller
    {
        // GET: Demo
        public ActionResult Index()
        {
            return View();
        }

    }
}