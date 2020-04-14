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
            //Do validation on timespan and date times

            db.insertscheduledmeeting(m.SessionName, m.Description, m.Date, m.StartTime, m.EndTime, m.TimeZone, m.Recurr, m.Frequency,
                m.RepeatEvery, m.RepeatFrequency, m.Sunday, m.Monday, m.Tuesday, m.Wednesday, m.Thursday, m.Friday, m.Saturday, m.RepeatMonthRadio1,
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