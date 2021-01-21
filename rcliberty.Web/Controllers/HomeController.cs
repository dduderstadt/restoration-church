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

        public ActionResult Media()
        {
            PodcastModels.SeriesEpisodeViewModel seVM = new PodcastModels.SeriesEpisodeViewModel();
            seVM.Series = PodcastModels.GetSeriesData();
            seVM.Episodes = PodcastModels.GetEpisodeData();
            return View(seVM);
        }

        public ActionResult Grow()
        {
            return View();
        }

        public ActionResult Connect()
        {
            return View();
        }

        [HttpPost, ActionName("Connect")]
        [ValidateAntiForgeryToken]
        public ActionResult ContactForm(string firstName, string lastName, string subject, string email, string message)
        {
            //build email body
            string body =
                $"<h4>Connect with us - {lastName}</h4>"
                + $"<h6><i>Date Submitted: {DateTime.Now.ToShortDateString()}</i></h6>"
                + $"<h6><b>Name:</b> {firstName} {lastName}</h6>"
                + $"<h6><b>Email:</b> {email}</h6>"
                + $"<h6><b>Subject:</b> {(subject != "" ? subject : "Not provided")}</h6>"
                + $"<h6><b>Message:</b></br> {message}</h6>";

            //configure MailMessage
            MailMessage msg = new MailMessage(
                email
                , "hello@rcliberty.com" //update to deployed email - "hello@"
                                        //, "dduderstadt@rcliberty.com"
                , "Connect with us - CONTACT"
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
                TempData["ConnectFieldValues"] = string.Format("PopulateFieldsOnError('{0}','{1}','{2}','{3}','{4}');", firstName, lastName, subject, email, message);
                return View();
            }
            TempData["EmailConfirm"] = $"Your message was sent successfully. Thanks for connecting with us, {firstName}!";
            return RedirectToAction("Index");
        }

        public ActionResult Give()
        {
            return View();
        }

        public ActionResult Ministries()
        {
            return View();
        }

        public PartialViewResult GuestVisit()
        {
            return PartialView();
        }

        [HttpPost, ActionName("GuestVisit")]
        [ValidateAntiForgeryToken]
        public ActionResult FirstTimeGuest(string firstName, string lastName, string email, string phoneNbr, string preferredContact, bool isBringingKids, byte? totalNbrOfKids, string additionalQuestions)
        {
            string returnUrl = TempData["CurrentAction"].ToString();

            //build email body
            string body =
                $"<h4>Plan Your Visit - {lastName}</h4>"
                + $"<h6><i>Date Submitted: {DateTime.Now.ToShortDateString()}</i></h6>"
                + $"<h6><b>Name:</b> {firstName} {lastName}</h6>"
                + $"<h6><b>Email:</b> {(email.Length >= 1 ? email : "Not Provided")}</h6>"
                + $"<h6><b>Phone #:</b> {(phoneNbr.Length >= 1 ? phoneNbr : "Not Provided")}</h6>"
                + $"<h6><b>Preferred Contact:</b> {preferredContact}</h6>"
                + $"<h6><b>Bringing kids? </b> {(isBringingKids ? "Yes" : "No")}</h6>"
                + (isBringingKids ? $"<h6><b>Number of Kids:</b> {totalNbrOfKids}</h6>" : "")
                + $"<h6><b>Additional Questions:</b> {(additionalQuestions != "" ? $"<br />{additionalQuestions}" : "Not provided")}</h6>";

            //configure MailMessage
            MailMessage msg = new MailMessage(
                email
                                        , "hello@rcliberty.com" //update to deployed email - "hello@"
                                        //, "dduderstadt@rcliberty.com"
                , "PLAN YOUR VISIT - Guest"
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
                TempData["GuestVisitFieldValues"] = string.Format("PopulateGuestVisitFieldsOnError('{0}','{1}','{2}','{3}','{4}', '{5}', '{6}', '{7}');", firstName, lastName, email, phoneNbr, preferredContact, isBringingKids, totalNbrOfKids, additionalQuestions);
                return RedirectToAction(returnUrl);
            }
            TempData["EmailConfirm"] = $"Thanks for planning your visit, {firstName}!\nWe will be in contact with you soon!";
            return RedirectToAction("Index");
        }

        //public ActionResult BandRegistration(BandViewModel bandReg)
        //{
        //    return View();
        //}

        //public ActionResult EventRegistration()
        //{
        //    ViewBag.BandRegConfirm = TempData["BandRegConfirm"];
        //    ViewBag.BandRegError = TempData["BandRegError"];
        //    return View();
        //}

        //[HttpPost, ActionName("BandRegistration")]
        //[ValidateAntiForgeryToken]
        //public ActionResult EventRegistration(BandViewModel bandReg)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //build email body
        //        string body =
        //            $"<strong>Band Name</strong>: {bandReg.BandName}<br />"
        //            + $"<strong>Contact Name</strong>: {bandReg.ContactName}<br />"
        //            + $"<strong>Contact Email</strong>: {bandReg.ContactInfo}<br />"
        //            + $"<strong>Nbr of Members</strong>: {bandReg.NbrOfMembers}<br />"
        //            + $"<strong>Rhythm Guitar</strong>: {(bandReg.RhythmGuitar == true ? "Yes" : "No")}<br />"
        //            + $"<strong>Lead Guitar</strong>: {(bandReg.LeadGuitar == true ? "Yes" : "No")}<br />"
        //            + $"<strong>Bass Guitar</strong>: {(bandReg.BassGuitar == true ? "Yes" : "No")}<br />"
        //            + $"<strong>Drums </strong>: {(bandReg.Drums == true ? "Yes" : "No")}<br />"
        //            + $"<strong>Other Instruments</strong>: {(bandReg.Other == true ? bandReg.OtherInstrumentDetails : "None")}<br />"
        //            + $"<strong>Nbr of Vocals</strong>: {bandReg.NbrOfVocals}<br />"
        //            + $"<strong>Additional Info</strong>: {bandReg.Details}";


        //        //configure MailMessage
        //        MailMessage msg = new MailMessage(
        //            bandReg.ContactInfo
        //            , "dduderstadt@rcliberty.com" //update to deployed email - "hello@"
        //            , "Band Registration - Battle of the Bands"
        //            , body);

        //        try
        //        {
        //            //send email
        //            EmailSettings.SendEmail(msg);
        //        }
        //        catch (Exception ex)
        //        {
        //            Debug.Write(ex.Message);
        //            TempData["BandRegError"] = "Oops! Something went wrong. Please try again later.";
        //            return RedirectToAction("EventRegistration");
        //        }

        //        //ViewBag.EmailConfirmation 
        //        TempData["BandRegConfirm"] = $"Your message was sent successfully. Thanks for registering, {bandReg.BandName}!";
        //        return RedirectToAction("EventRegistration");
        //    }
        //    return View("EventRegistration", bandReg);
        //}
    }
}