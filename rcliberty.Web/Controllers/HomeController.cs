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
            ViewBag.PodcastEpisodes = rcliberty.Web.Models.PodcastModels.GetPodcastEpisodes();

            return View();
        }

        public ActionResult Grow()
        {
            return View();
        }
    }
}