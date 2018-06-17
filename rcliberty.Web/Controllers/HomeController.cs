using rcliberty.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace rcliberty.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.EmailConfirmation = TempData["EmailConfirm"];
            return View();
        }

        public ActionResult Discover()
        {
            return View();
        }

        public ActionResult Podcasts()
        {
            return View(PodcastModels.GetPodcastEpisodes());
        }

        public ActionResult Grow()
        {
            return View();
        }

        public ActionResult Connect()
        {
            ViewBag.ErrorMessage = TempData["EmailError"];
            return View();
        }

        [HttpPost, ActionName("Connect")]
        [ValidateAntiForgeryToken]
        public ActionResult ContactForm(ContactViewModel contact)
        {
            if (ModelState.IsValid)
            {
                //build email body
                string body =
                    $"<strong>Name</strong>: {contact.FirstName} {contact.LastName}<br />"
                    + $"<strong>Email</strong>: {contact.Email}<br />"
                    + $"<strong>Subject</strong>: {contact.Subject}<br />"
                    + $"<strong>Message</strong>: {contact.Message}<br />";

                //configure MailMessage
                MailMessage msg = new MailMessage(
                    "contact@dudercode.com"
                    ,"derek@dudercode.com" //TODO update to deployed email - "hello@"
                    ,"Contact Form - rcliberty.com"
                    ,body);

                try
                {
                    //send email
                    EmailSettings.SendEmail(msg);
                }
                catch (Exception ex)
                {
                    Debug.Write(ex.Message);
                    TempData["EmailError"] = "Oops! Something went wrong. Please try again later.";
                    return RedirectToAction("Connect");
                }

                //ViewBag.EmailConfirmation 
                    TempData["EmailConfirm"] = $"Your message was sent successfully. Thanks for connecting with us, {contact.FirstName}!";
                return RedirectToAction("Index");
            }
            return View("Connect", contact);
        }

        public ActionResult Give()
        {
            return View();
        }

        public ActionResult EventRegistration(BandBattleViewModel model)
        {
            return View(model);
        }
    }
}