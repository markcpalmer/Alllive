//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Alllive.Models
{
    using System;
    
    public partial class UserSchedule_Result
    {
        public int UserID { get; set; }
        public int SessionID { get; set; }
        public string SessionName { get; set; }
        public string Description { get; set; }
        public System.DateTime Date { get; set; }
        public System.DateTime StartTime { get; set; }
        public System.DateTime EndTime { get; set; }
        public string TimeZone { get; set; }
        public bool Recurr { get; set; }
        public int Frequency { get; set; }
        public int RepeatDaily { get; set; }
        public int RepeatWeekly { get; set; }
        public int RepeatMonthly { get; set; }
        public bool Sunday { get; set; }
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
        public System.DateTime EndDateBy { get; set; }
        public int EndDateAfter { get; set; }
        public string MeetingLink { get; set; }
        public int RepeatMonthlyDate { get; set; }
    }
}
