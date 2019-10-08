using SimpleBlog.Core.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleBlog //todo analyze
{
    public static class Extensions
    {
        //public static string ToConfigLocalTime(this DateTime utcDT)
        //{
        //    var systemTimezone = TimeZoneInfo.FindSystemTimeZoneById(ConfigurationManager.AppSettings["Timezone"]);
        //    return String.Format("{0} ({1})", TimeZoneInfo.ConvertTimeFromUtc(utcDT, systemTimezone).ToShortDateString(), ConfigurationManager.AppSettings["TimezoneAbbreviation"]);
        //}

        public static string Href(this Post post, UrlHelper helper)
        {
            return helper.RouteUrl(new { controller = "Blog", action = "Post", year = post.PublishedOn.Year, month = post.PublishedOn.Month, title = post.UrlSlug });
        }
    }
}