﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alllive.Helpers
{
    public class Utilities
    {
        public enum States
        {
            [Description("Alabama")]
            AL,
            [Description("Alaska")]
            AK,
            [Description("Arkansas")]
            AR,
            [Description("Arizona")]
            AZ,
            [Description("California")]
            CA,
            [Description("Colorado")]
            CO,
            [Description("Connecticut")]
            CT,
            [Description("D.C.")]
            DC,
            [Description("Delaware")]
            DE,
            [Description("Florida")]
            FL,
            [Description("Georgia")]
            GA,
            [Description("Hawaii")]
            HI,
            [Description("Iowa")]
            IA,
            [Description("Idaho")]
            ID,
            [Description("Illinois")]
            IL,
            [Description("Indiana")]
            IN,
            [Description("Kansas")]
            KS,
            [Description("Kentucky")]
            KY,
            [Description("Louisiana")]
            LA,
            [Description("Massachusetts")]
            MA,
            [Description("Maryland")]
            MD,
            [Description("Maine")]
            ME,
            [Description("Michigan")]
            MI,
            [Description("Minnesota")]
            MN,
            [Description("Missouri")]
            MO,
            [Description("Mississippi")]
            MS,
            [Description("Montana")]
            MT,
            [Description("North Carolina")]
            NC,
            [Description("North Dakota")]
            ND,
            [Description("Nebraska")]
            NE,
            [Description("New Hampshire")]
            NH,
            [Description("New Jersey")]
            NJ,
            [Description("New Mexico")]
            NM,
            [Description("Nevada")]
            NV,
            [Description("New York")]
            NY,
            [Description("Oklahoma")]
            OK,
            [Description("Ohio")]
            OH,
            [Description("Oregon")]
            OR,
            [Description("Pennsylvania")]
            PA,
            [Description("Rhode Island")]
            RI,
            [Description("South Carolina")]
            SC,
            [Description("South Dakota")]
            SD,
            [Description("Tennessee")]
            TN,
            [Description("Texas")]
            TX,
            [Description("Utah")]
            UT,
            [Description("Virginia")]
            VA,
            [Description("Vermont")]
            VT,
            [Description("Washington")]
            WA,
            [Description("Wisconsin")]
            WI,
            [Description("West Virginia")]
            WV,
            [Description("Wyoming")]
            WY
        }

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