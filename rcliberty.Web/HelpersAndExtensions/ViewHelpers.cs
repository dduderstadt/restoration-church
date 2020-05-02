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
            return ((controller == GetCurrentController(html) && action == GetCurrentAction(html)) ? "active-nav" : "");
        }

        public static string GetCurrentAction(this HtmlHelper html)
        {
            return (string)html.ViewContext.RouteData.Values["action"];
        }

        public static string GetCurrentController(this HtmlHelper html)
        {
            return (string)html.ViewContext.RouteData.Values["controller"];
        }
    }
}