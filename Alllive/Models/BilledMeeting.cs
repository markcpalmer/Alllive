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
    using System.Collections.Generic;
    
    public partial class BilledMeeting
    {
        public int BilledMeetingID { get; set; }
        public System.DateTime StartTime { get; set; }
        public System.DateTime EndTime { get; set; }
        public int Duration { get; set; }
        public decimal Rate { get; set; }
        public int StudentUserID { get; set; }
        public int TutorUserID { get; set; }
        public string Summary { get; set; }
        public string Status { get; set; }
    }
}