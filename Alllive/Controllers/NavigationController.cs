using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Alllive.Helpers;
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
        public ActionResult ScheduleMeeting(int sessionID=0)
        {
            ViewBag.minuteOptions = Utilities.GetTimeFrames();
            var profile = Dc.TutorProfiles.FirstOrDefault(a => a.UserID == currentUser.UserId);
            if(profile != null)
            {
                ViewBag.Rate = profile.Rate;
            }
            var m = new ScheduleMeeting();
            if (sessionID > 0)
            {
                m = Dc.ScheduleMeetings.Include("Attendees").FirstOrDefault(a=>a.SessionID==sessionID);
            }
            else
            {
                //todo: find users default timezone
                m.TimeZone = "";
            }

            
            return View(m);
        }

        [HttpPost]
        [Authorize]
        public ActionResult SaveMeeting(ScheduleMeeting m)
        {
            

            if (ModelState.IsValid)
            {
                //Time Sessions
                //TimeSpan Start, End = new TimeSpan();
                //Start = m.StartTime.TimeOfDay;
                //End = m.EndTime.TimeOfDay;

                //TimeZone
                if (m.SessionID > 0)
                {
                    Dc.ScheduleMeetings.Attach(m);
                    Dc.SaveChanges();
                }
                else
                {
                    m.HostUserID = currentUser.UserId;
                    m.Active = "Y"; 
                    //meeting link
                    string meetingID = Membership.GeneratePassword(10, 2);

                    m.MeetingLink = "https://www.groupworld.net/room/3437/" + meetingID + "?show_timer=true&timer_two_users=true";

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
                        m.Frequency = 0; m.RepeatDaily = 0; m.RepeatWeekly = 0; m.RepeatMonthly = 0; m.Sunday = false; m.Monday = false; m.Tuesday = false;
                        m.Wednesday = false; m.Thursday = false; m.Friday = false; m.Saturday = false; m.RepeatMonthRadio1 = false;
                        m.RepeatMonthRadio2 = false; m.Radio2List1 = 0; m.Radio2List2 = 0; m.EndDateBy = new DateTime(); m.EndDateAfter = 0;

                    }

                    //Inserts the values
                    ////Dc.insertscheduledmeeting(m.SessionName, m.Description, m.Date, m.StartTime, m.EndTime, m.TimeZone, m.Recurr, m.Frequency,
                    ////     m.RepeatDaily, m.RepeatWeekly, m.RepeatMonthly, m.RepeatMonthlyDate, m.Sunday, m.Monday, m.Tuesday, m.Wednesday, m.Thursday, m.Friday, m.Saturday, m.RepeatMonthRadio1,
                    ////    m.RepeatMonthRadio2, m.Radio2List1, m.Radio2List2, m.EndDateBy, m.EndDateAfter, currentUser.UserId, m.MeetingLink);
                    Dc.ScheduleMeetings.Add(m);
                    Dc.SaveChanges();

                    //This needs to change (Either Success view or take user to "Add to Calendar Page/Review Meeting Page"
                    //return RedirectToAction("Schedule", "User", new { ID = currentUser.UserId });//redirects user to different action"                    

                }
            }
            //m.EndDateBy = new DateTime();
            //m.EndDateAfter = new DateTime();

            // Validation failed and returning current fields.
            return View("ScheduleMeeting",m);

        }
        [HttpPut]
        public ActionResult SaveAttendee(Attendee m)
        {
            if (m.SessionID > 0)
            {
                if (m.AttendeeID > 0)
                {
                    Dc.Attendees.Attach(m);
                }
                else
                {
                    Dc.Attendees.Add(m);
                }
                Dc.SaveChanges();
            }
            return PartialView("_Attendee", m);
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
        public ActionResult OnlineYoga()
        {
            return View();
        }
    }
}