using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Alllive.Models
{
    public class ScheduleModel
    {
        [Required(ErrorMessage = "Session Name Required.")]
        public string SessionName { get; set; }

        
        public string Description { get; set; }

        [Required(ErrorMessage = "Date is Required.")]
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string TimeZone { get; set; }
        public bool Recurr { get; set; }
        public int  Frequency { get; set; }
        public int RepeatEvery { get; set; }
        public int RepeatDaily { get; set; }
        public int RepeatWeekly { get; set; }
        public int RepeatMonthly { get; set; }
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
        public int EndDateAfter { get; set; }




    }
}