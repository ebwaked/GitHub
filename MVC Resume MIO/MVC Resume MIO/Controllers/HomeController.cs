using SendGrid;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using MVC_Resume_MIO.Models;


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

        public ActionResult Resume()
        {
            ViewBag.Message = "Resume.";

            return View();
        }

        
        // GET: Contact
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(Contact ContactForm)
        {
            var MyAddress = ConfigurationManager.AppSettings["ContactEmail"];
            var MyUsername = ConfigurationManager.AppSettings["Username"];
            var MyPassword = ConfigurationManager.AppSettings["Password"];

            if(ModelState.IsValid)
            {
                SendGridMessage mail = new SendGridMessage();
                mail.From = new MailAddress(ContactForm.Email);
                mail.AddTo(MyAddress);
                mail.Subject = ContactForm.Subject;
                mail.Text = ContactForm.Message;

                var credentials = new NetworkCredential(MyUsername, MyPassword);
                var transportWeb = new Web(credentials);
                transportWeb.Deliver(mail);

            }

            return View();
        }
    

        public ActionResult JavascriptExercises()
        {
            ViewBag.Message = "JavascriptExercises.";

            return View();
        }
    }
}