using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace theCapitol.Web.Controllers
{
    public class HomeController : Controller
    {
        #region site structure
        /*
         * we want a site that is easy to navigate for visitors
         * unfamiliar to the Capitol. LESS IS MORE
         * 
         * Home/Index will contain...
         * - navigation links (5 total)
         * - about/welcome
         * - why choose us (brief overview - what we offer)
         *  - !IMPORTANT: main service info (Sunday nights @ 6pm)
         * - testimonials/reviews
         * - links to recent [blog] articles
         * ======================================================
         * NAVIGATION
         * theCapitol (home, about) || general info
         * services || service times/ministries (what we offer)
         * media || pictures & video
         * team || leadership/who we are
         * contact || contact form, Google Maps, get involved
         * blog (*) || articles/resources to read (SEPARATE CONTROLLER)
         *  - NOTE: this will be linked from media; can update
         *  main navigation at a later time
         */
         //REMOVED ITEMS
        ////Prices/pricing table - N/A
        ////Number - N/A
        #endregion

        //default page (home/about) - theCapitol
        public ActionResult Index()
        {
            return View();
        }

        //services (what we offer)
        //+ details of small groups/additional ministries
        public ActionResult Services()
        {
            return View();
        }

        //media (pictures, video, podcasts, blog)
        public ActionResult Media()
        {
            return View();
        }

        //team (who/leadership)
        public ActionResult Team()
        {
            return View();
        }

        //contact form, Google maps, get involved
        public ActionResult Contact()
        {
            return View();
        }
    }
}