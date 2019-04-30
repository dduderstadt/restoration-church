using rcliberty.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace rcliberty.Web.Controllers
{
    public class AdminController : Controller
    {
        rclibertyEntities db = new rclibertyEntities();

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        // GET: AddNewSeries
        public ActionResult AddNewSeries()
        {
            return View();
        }

        // POST: AddNewSeries
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddNewSeries([Bind(Include = "Id,Name,Image,Description")] Series series,
            HttpPostedFileBase seriesImage)
        {
            if (ModelState.IsValid)
            {
                string imageName = "";

                if (seriesImage != null)
                {
                    try
                    {
                        string ext = Path.GetExtension(seriesImage.FileName).ToLower();
                        string[] acceptExts = { ".jpeg", ".jpg", ".png" };

                        if (acceptExts.Contains(ext))
                        {
                            imageName =
                                series.Name.Replace(' ', '-').Replace("&", "and").ToLower() + ext;

                            //TODO Update with real folder structure for series image uploads
                            string path = Path.Combine(Server.MapPath("~/Content/img/!test/"), imageName);
                            seriesImage.SaveAs(path);
                            series.Image = imageName;
                        }
                    }
                    catch
                    {
                        //TODO Handle exceptions
                    }
                }

                db.Set<Series>().Add(series);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(series);
        }
    }
}