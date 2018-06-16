using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace rcliberty.Web.HelpersAndExtensions
{
    public static class ViewHelpers
    {
        public static string IsActive(this HtmlHelper html, string controller, string action)
        {
            var routeData = html.ViewContext.RouteData;

            string currAction = (string)routeData.Values["action"];
            string currController = (string)routeData.Values["controller"];

            return ((controller == currController && action == currAction) ? "active-nav" : "");
        }
    }
}