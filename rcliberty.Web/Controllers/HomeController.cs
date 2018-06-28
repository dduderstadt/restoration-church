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
                    contact.Email
                    , "hello@rcliberty.com" //update to deployed email - "hello@"
                    , "Contact Form - rcliberty.com"
                    , body);

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

        public ActionResult BandRegistration(BandViewModel bandReg)
        {
            return View();
        }

        public ActionResult EventRegistration()
        {
            ViewBag.BandRegConfirm = TempData["BandRegConfirm"];
            ViewBag.BandRegError = TempData["BandRegError"];
            return View();
        }

        [HttpPost, ActionName("BandRegistration")]
        [ValidateAntiForgeryToken]
        public ActionResult EventRegistration(BandViewModel bandReg)
        {
            if (ModelState.IsValid)
            {
                //build email body
                string body =
                    $"<strong>Band Name</strong>: {bandReg.BandName}<br />"
                    + $"<strong>Contact Name</strong>: {bandReg.ContactName}<br />"
                    + $"<strong>Contact Email</strong>: {bandReg.ContactInfo}<br />"
                    + $"<strong>Nbr of Members</strong>: {bandReg.NbrOfMembers}<br />"
                    + $"<strong>Rhythm Guitar</strong>: {(bandReg.RhythmGuitar == true ? "Yes" : "No")}<br />"
                    + $"<strong>Lead Guitar</strong>: {(bandReg.LeadGuitar == true ? "Yes" : "No")}<br />"
                    + $"<strong>Bass Guitar</strong>: {(bandReg.BassGuitar == true ? "Yes" : "No")}<br />"
                    + $"<strong>Drums </strong>: {(bandReg.Drums == true ? "Yes" : "No")}<br />"
                    + $"<strong>Other Instruments</strong>: {(bandReg.Other == true ? bandReg.OtherInstrumentDetails : "None")}<br />"
                    + $"<strong>Nbr of Vocals</strong>: {bandReg.NbrOfVocals}<br />"
                    + $"<strong>Additional Info</strong>: {bandReg.Details}";


                //configure MailMessage
                MailMessage msg = new MailMessage(
                    bandReg.ContactInfo
                    , "dduderstadt@rcliberty.com" //update to deployed email - "hello@"
                    , "Band Registration - Battle of the Bands"
                    , body);

                try
                {
                    //send email
                    EmailSettings.SendEmail(msg);
                }
                catch (Exception ex)
                {
                    Debug.Write(ex.Message);
                    TempData["BandRegError"] = "Oops! Something went wrong. Please try again later.";
                    return RedirectToAction("EventRegistration");
                }

                //ViewBag.EmailConfirmation 
                TempData["BandRegConfirm"] = $"Your message was sent successfully. Thanks for registering, {bandReg.BandName}!";
                return RedirectToAction("EventRegistration");
            }
            return View("EventRegistration", bandReg);
        }
    }
}