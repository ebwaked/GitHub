using Cool_Thing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cool_Thing.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult LinqFu()
        {
            var cool = new CoolThing();

            var KungFu = cool.Descriptions
                .Select(m => m)
                .Where(d => d.Username == "mytearscurecancer")
                .Union(    //or .Intersect(...........);
                cool.Descriptions.Select(d => d).Where(u => u.Username == "tparish")
                ).ToList();



            var Kung = cool.Descriptions
                       .Select(d => d)
                       .Where(s => s.StatusCode == 101);

            var Fu = cool.Users
                     .Select(u => u)
                     .Where(m => Kung.Any(t => t.Username == m.Username));


            return View(KungFu);
        }


        public ActionResult KungFu()
        {
            return View();
        }

        [HttpPost]
        public ActionResult KungFu(string username)
        {
            var cool = new CoolThing();

            var KungFu = cool.Descriptions
                         .Select(desc => desc);
                         .Where(u => u.Username == name || name == "")
                         .ToList();
                        
            
            

        }


        public object name { get; set; }
    }
}