using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alllive.Models
{
    public class MeetingViewModel:ScheduleMeeting
    {
        
        public string Host { get; set; }
        public MeetingViewModel(ScheduleMeeting meeting, User user)
        {
            Active = meeting.Active;

            if (user != null)
            {
                Host = user.FirstName + " " + user.LastName;
            }
        }
    }
}