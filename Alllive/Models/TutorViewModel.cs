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

        }
        public TutorViewModel(TutorProfile tp, User u)
        {
            UserId = u.UserID;
            UserName = u.UserName;
            LastName = u.LastName;
            FirstName = u.FirstName;
            Description = tp.Description;

        }
        public int UserId { get; set; }

        
        public string UserName { get; set; }

        
        public string LastName { get; set; }

      
        public string FirstName { get; set; }
        public string Description { get; set; }
        public int HourlyRate { get; set; }
        public bool Sunday { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public int StudentLevel { get; set; }

    }
}