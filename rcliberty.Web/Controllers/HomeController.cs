using rcliberty.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public ActionResult Sermons()
        {
            return View(PodcastModels.GetPodcastEpisodes());
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
        public ActionResult ContactForm(ContactViewModel contact)
        {
            return View(contact);
        }
    }
}