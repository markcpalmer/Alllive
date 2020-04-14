using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alllive.Models
{
    public class ScheduleModel
    {
        public string SessionName { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int TimeZone { get; set; }
        public bool Recurr { get; set; }
        public int  Frequency { get; set; }
        public int RepeatEvery { get; set; }
        public int RepeatFrequency { get; set; }
        public bool  Sunday { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool RepeatMonthRadio1 { get; set; }
        public bool RepeatMonthRadio2 { get; set; }
        public int Radio2List1 { get; set; }
        public int Radio2List2 { get; set; }
        public DateTime EndDateBy { get; set; }
        public DateTime EndDateAfter { get; set; }




    }
}