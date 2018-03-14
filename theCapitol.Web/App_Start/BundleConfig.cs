using System.Web;
using System.Web.Optimization;

namespace theCapitol.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //template css (Content/css)
            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/css/bootstrap.min.css",
                "~/Content/css/font-awesome.min.css",
                "~/Content/css/magnific-popup.css",
                "~/Content/css/owl.carousel.css",
                "~/Content/css/owl.theme.default.css",
                "~/Content/css/style.css"));

            //template JavaScript (Scripts/js)
            bundles.Add(new ScriptBundle("~/Scripts/js").Include(
                "~/Scripts/js/jquery.min.js",
                "~/Scripts/js/bootstrap.min.js",
                "~/Scripts/js/jquery.magnific-popup.js",
                "~/Scripts/js/owl.carousel.min.js",
                "~/Scripts/js/main.js"));
        }
    }
}
