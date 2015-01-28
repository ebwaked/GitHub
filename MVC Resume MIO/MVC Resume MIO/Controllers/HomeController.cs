using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Resume_MIO.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Keypoints()
        {
            ViewBag.Message = "Keypoints.";

            return View();
        }

        public ActionResult Experience()
        {
            ViewBag.Message = "Experience.";

            return View();
        }

        public ActionResult Portfolio()
        {
            ViewBag.Message = "Portfolio.";

            return View();
        }

        public ActionResult Education()
        {
            ViewBag.Message = "Education.";

            return View();
        }

        public ActionResult Skills()
        {
            ViewBag.Message = "Skills.";

            return View();
        }

        public ActionResult Blog()
        {
            ViewBag.Message = "Blog.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact.";

            return View();
        }

        public ActionResult JavascriptExercises()
        {
            ViewBag.Message = "JavascriptExercises.";

            return View();
        }
    }
}