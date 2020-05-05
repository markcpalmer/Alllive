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
            User = new User();
            TutorProfile = new TutorProfile();
        }
        public TutorViewModel(TutorProfile tp, User u)
        {
            User = u;
            TutorProfile = tp;
        }
        public User User { get; set; }
        public TutorProfile TutorProfile { get; set; }
       
    }
}