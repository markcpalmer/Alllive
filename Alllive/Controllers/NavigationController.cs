using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Alllive.Models;

namespace Alllive.Controllers
{
    public class NavigationController : Controller
    {
        // GET: Navagation
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ScheduleMeeting()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ScheduleMeeting(ScheduleModel m)
        {
            AllliveDBDataContext db = new AllliveDBDataContext();
            
            //Time Sessions
            TimeSpan Start, End = new TimeSpan();
            Start = m.StartTime.TimeOfDay;
            End = m.EndTime.TimeOfDay;

            //TimeZone

            //Radio button logic
            //You MUST get ALL the values for Recurr
            if (m.Recurr == true)
            {
                //Monthly
                if(m.Frequency == 3)
                {
                    //m.RepeatFrequency
                }
            }
            else // sets the following to defaults
            {
                m.Frequency = 0; m.RepeatEvery = 0; m.RepeatDaily = 0; m.RepeatWeekly = 0; m.RepeatMonthly = 0; m.Sunday = false; m.Monday= false; m.Tuesday = false; 
                m.Wednesday = false; m.Thursday = false; m.Friday = false; m.Saturday = false; m.RepeatMonthRadio1 = false;
                m.RepeatMonthRadio2 = false; m.Radio2List1 = 0; m.Radio2List2 = 0; m.EndDateBy = new DateTime(); m.EndDateAfter = new DateTime();
            }
            
            //Inserts the values
            db.insertscheduledmeeting(m.SessionName, m.Description, m.Date, Start, End, m.TimeZone, m.Recurr, m.Frequency,
                m.RepeatEvery, m.RepeatDaily, m.RepeatWeekly, m.RepeatMonthly, m.Sunday, m.Monday, m.Tuesday, m.Wednesday, m.Thursday, m.Friday, m.Saturday, m.RepeatMonthRadio1,
                m.RepeatMonthRadio2, m.Radio2List1, m.Radio2List2, m.EndDateBy, m.EndDateAfter);
            return View();
        }
        public ActionResult JoinMeeting()
        {
            return View();
        }
        public ActionResult StartMeeting()
        {
            return View();
        }
        public ActionResult DistanceLearning()
        {
            return View();
        }
        public ActionResult SearchTutor()
        {
            return View();
        }
        public ActionResult OnlineYoga()
        {
            return View();
        }
    }
}