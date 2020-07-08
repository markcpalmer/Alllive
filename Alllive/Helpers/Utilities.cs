using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alllive.Helpers
{
    public class Utilities
    {
        public static IEnumerable<SelectListItem> GetTimeZones()
        {
            return TimeZoneInfo.GetSystemTimeZones().Select(a => new SelectListItem() { Text = a.DisplayName, Value = a.Id });

        }

        public static IEnumerable<SelectListItem> GetTimeFrames()
        {
            string[] minutes = new string[]
         {
                "00","15","30","45"
         };
            var options = new List<string>();
            for (int h = 0; h < 24; h++)
            {
                string suffix = "am";
                if (h > 11)
                {
                    suffix = "pm";
                }
                foreach (string m in minutes)
                {
                    if (h > 12)
                    {
                        var hourConverter = h + ":" + m;

                        //DateTime dt = DateTime.ParseExact(hourConverter, "HH:mm", null, DateTimeStyles.None);
                        DateTime dt = DateTime.ParseExact(hourConverter, "HH:mm", null, System.Globalization.DateTimeStyles.None);

                        string time12 = dt.ToString("HH:mm");
                        options.Add(dt.ToShortTimeString().ToLower());
                    }
                    else if (h == 0)
                    {
                        options.Add("12:" + m + " " + suffix);
                    }
                    else
                    {
                        options.Add(h.ToString() + ":" + m + " " + suffix);
                    }
                }
            }
            return options.Select(o => new SelectListItem { Value = o, Text = o });

        }
           }
}