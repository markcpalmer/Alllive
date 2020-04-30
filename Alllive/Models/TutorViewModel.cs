using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alllive.Models
{
    public class TutorViewModel
    {
        public TutorViewModel()
        {
            UserId = 0;
            TutorProfileId = 0;
        }
        public TutorViewModel(TutorProfile tp, User u)
        {
            UserId = u.UserID;
            UserName = u.UserName;
            LastName = u.LastName;
            FirstName = u.FirstName;
            Bio = tp.Bio;
            TutorProfileId = tp.TutorProfileID;
        }
        public int UserId { get; set; }
        public int TutorProfileId { get; set; }


        public string UserName { get; set; }

        
        public string LastName { get; set; }

      
        public string FirstName { get; set; }
        public string Bio { get; set; }
        public int HourlyRate { get; set; }
        public bool Sunday { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public int StudentLevel { get; set; }
        public DateTime SundayStart { get; set; }
        public DateTime SundayEnd { get; set; }
        public DateTime MondayStart { get; set; }
        public DateTime MondayEnd { get; set; }
        public DateTime TuesdayStart { get; set; }
        public DateTime TuesdayEnd { get; set; }
        public DateTime WednesdayStart { get; set; }
        public DateTime WednesdayEnd { get; set; }
        public DateTime ThursdayStart { get; set; }
        public DateTime ThursdayEnd { get; set; }
        public DateTime FridayStart { get; set; }
        public DateTime FridayEnd { get; set; }
        public DateTime SaturdayStart { get; set; }
        public DateTime SaturdayEnd { get; set; }



    }
}