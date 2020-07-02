using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alllive.Helpers
{
    public static class Extensions
    {
        public static DateTime GetLocalTime(this DateTime dt, string timeZoneName)
        {
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneName);
            return TimeZoneInfo.ConvertTimeFromUtc(dt, timeZone);
        }
        public static DateTime GetFromLocalTime(this DateTime dt, string timeZoneName)
        {
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneName);
            return TimeZoneInfo.ConvertTimeToUtc(dt, timeZone);
        }

    }
}