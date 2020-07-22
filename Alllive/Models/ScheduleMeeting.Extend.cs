using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Alllive.Models
{
    public partial class ScheduleMeeting
    {
        [NotMapped]
        public string Host { get; set; }
    }
}