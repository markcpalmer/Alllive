using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Alllive.Models;

namespace Alllive.Controllers
{
    public class NavigationController : AllLiveControllerBase
    {
        

      
        // GET: Navagation
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult ScheduleMeeting()
        {
            
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult ScheduleMeeting(ScheduleModel m)
        {
            

            AllliveDBDataContext db = new AllliveDBDataContext();
            if (ModelState.IsValid)
            {
                //Time Sessions
                TimeSpan Start, End = new TimeSpan();
                Start = m.StartTime.TimeOfDay;
                End = m.EndTime.TimeOfDay;

                //TimeZone

                m.MeetingLink = "";

                //Radio button logic
                //You MUST get ALL the values for Recurr
                if (m.Recurr == true)
                {
                    //Monthly
                    if (m.Frequency == 3)
                    {
                        //m.RepeatFrequency
                    }
                }
                else // sets the following to defaults
                {
                    m.Frequency = 0;  m.RepeatDaily = 0; m.RepeatWeekly = 0; m.RepeatMonthly = 0; m.Sunday = false; m.Monday = false; m.Tuesday = false;
                    m.Wednesday = false; m.Thursday = false; m.Friday = false; m.Saturday = false; m.RepeatMonthRadio1 = false;
                    m.RepeatMonthRadio2 = false; m.Radio2List1 = 0; m.Radio2List2 = 0; m.EndDateBy = new DateTime(); m.EndDateAfter = 0;
                    
                }

                //Inserts the values
                db.insertscheduledmeeting(m.SessionName, m.Description, m.Date, Start, End, m.TimeZone, m.Recurr, m.Frequency,
                     m.RepeatDaily, m.RepeatWeekly, m.RepeatMonthly, m.Sunday, m.Monday, m.Tuesday, m.Wednesday, m.Thursday, m.Friday, m.Saturday, m.RepeatMonthRadio1,
                    m.RepeatMonthRadio2, m.Radio2List1, m.Radio2List2, m.EndDateBy, m.EndDateAfter,currentUser.UserId,m.MeetingLink);

                
                //This needs to change (Either Success view or take user to "Add to Calendar Page/Review Meeting Page"
                return View();
            }
            //m.EndDateBy = new DateTime();
            //m.EndDateAfter = new DateTime();

            // Validation failed and returning current fields.
            return View(m);
        }
        [OverrideAuthorization]
        public ActionResult JoinMeeting()
        {
            return View();
        }
        [Authorize]
        public ActionResult StartMeeting(int? ID)
        {
          

            if (ID == null)
            {
                ID = currentUser.UserId;
            }
            var DisplaySchedule = Dc.UserSchedule(ID);
            return View(DisplaySchedule);
        }
        [OverrideAuthorization]
        public ActionResult DistanceLearning()
        {
            return View();
        }
        [OverrideAuthorization]
        public ActionResult SearchTutor()
        {
            return View();
        }
        [OverrideAuthorization]
        public ActionResult SearchResults(SearchTutorModel m)
        {
            
            var results = Dc.TutorProfiles.Join(Dc.Users,
                    tp=>tp.UserID,
                    u=>u.UserID,
                    (tp,u)=>new { tp, u }
                ).Where(a =>
                (a.tp.Sunday && m.Sunday)||
                (a.tp.Monday && m.Monday)||
                (a.tp.Tuesday && m.Tuesday) ||
                (a.tp.Wednesday && m.Wednesday) ||
                (a.tp.Thursday && m.Thursday) ||
                (a.tp.Friday && m.Friday) ||
                (a.tp.Saturday && m.Saturday)
                ).Select(a=>new TutorViewModel(a.tp,a.u));


            return PartialView(results);
        }
        [OverrideAuthorization]
        public ActionResult OnlineYoga()
        {
            return View();
        }
    }
}