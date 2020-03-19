using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alllive.Models
{
    public class ScheduleModel
    {
        public int UserID { get; set; }
        public DateTime DateTimeScheduledIn { get; set; }
        public DateTime DateTimeScheduledOut { get; set; }
        public int SessionID { get; set; }

    }
}