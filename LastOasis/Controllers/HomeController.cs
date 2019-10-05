using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LastOasis.utils;
using LastOasis.Models;

namespace LastOasis.Controllers
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

        public ActionResult Send_Email()
        {
            return View(new SendEmailModel());
        }

        [HttpPost]
        public ActionResult Send_Email(SendEmailModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    String toEmail = "nche0024@student.monash.edu";
                    String subject = model.Subject;
                    String contents = model.Contents;

                    EmailSender es = new EmailSender();
                    es.Send(toEmail, subject, contents);

                    ViewBag.Result = "Email has been send.";

                    ModelState.Clear();

                    return View(new SendEmailModel());
                }
                catch
                {
                    return View();
                }
            }

            return View();
        }
    }
}
