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

        public static bool GetLiveNotification(this HtmlHelper html)
        {
            if (DateTime.Now.DayOfWeek != DayOfWeek.Sunday) return false;

            DateTime uStart = DateTime.Now.ToUniversalTime();
            DateTime uEnd = DateTime.Now.ToUniversalTime();
            var startTime = ((uStart.Hour >= 16 && uStart.Minute > 24));
            var endTime = ((uEnd.Hour <= 18 && uEnd.Minute < 10));

            return startTime && endTime ? true : false;
        }
    }
}