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
    
    public partial class SubmitMeeting
    {
        public int Id { get; set; }
        public int TutorProfileID { get; set; }
        public System.DateTime Date { get; set; }
        public System.DateTime StartTime { get; set; }
        public System.DateTime EndTime { get; set; }
        public string Subject { get; set; }
        public string LessonReview { get; set; }
        public double HourlyRate { get; set; }
        public double Duration { get; set; }
    }
}
