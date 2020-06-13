﻿using Alllive.Helpers;
using Alllive.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Alllive.Controllers
{
    public class MeetingController : AllLiveControllerBase
    {
        // GET: Meeting
        [Authorize]
        public ActionResult ScheduleMeeting(int sessionID = 0)
        {
            ViewBag.minuteOptions = Utilities.GetTimeFrames();
            var profile = Dc.TutorProfiles.FirstOrDefault(a => a.UserID == currentUser.UserId);
            if (profile != null)
            {
                ViewBag.Rate = profile.Rate;
            }
            var m = new ScheduleMeeting();
            if (sessionID > 0)
            {
                m = Dc.ScheduleMeetings.Include("Attendees").FirstOrDefault(a => a.SessionID == sessionID);
            }
            else
            {
                m.StartTime = DateTime.UtcNow;
                m.EndTime = DateTime.UtcNow.AddMinutes(45);
                m.EndDateBy = DateTime.UtcNow.AddMonths(1);
                m.HostUserID = currentUser.UserId;
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


                    Dc.ScheduleMeetings.Add(m);
                    Dc.SaveChanges();
                    var schedule = new Schedule()
                    {
                        SessionID = m.SessionID,
                        UserID = currentUser.UserId
                    };
                    Dc.Schedules.Add(schedule);
                    Dc.SaveChanges();
                    ModelState.Clear();
                    // needs to input into the schedule table because thats the table that is populating
                    // the schedule
                    //
                    //redirects user to different action"                    
                    //This needs to change (Either Success view or take user to "Add to Calendar Page/Review Meeting Page"

                    // jquery is preventing any kind of action
                    // return RedirectToAction("Schedule", "User", new { ID = currentUser.UserId });//redirects user to different action"                    
                }
                m.Attendees = Dc.Attendees.Where(a => a.SessionID == m.SessionID).ToList();
            }
            //m.EndDateBy = new DateTime();
            //m.EndDateAfter = new DateTime();

            // Validation failed and returning current fields.
            return PartialView("ScheduleMeeting", m);

        }
        [HttpPost]
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
                if (m.UserID.HasValue)
                {
                    var findSchedule = Dc.Schedules.FirstOrDefault(a => a.UserID == m.UserID.Value && a.SessionID == m.SessionID);
                    if (findSchedule == null)
                    {
                        findSchedule = new Schedule()
                        {
                            SessionID = m.SessionID,
                            UserID = m.UserID.Value
                        };
                        Dc.Schedules.Add(findSchedule);
                    }
                }
                Dc.SaveChanges();
            }
            return PartialView("_Attendee", m);
        }


        public ActionResult DeleteAttendee(int attendeeID)
        {
            var attendee = Dc.Attendees.Find(attendeeID);
            if (attendee != null)
            {
                Dc.Attendees.Remove(attendee);
                Dc.SaveChanges();
                return Json(new { result = "ok" }, JsonRequestBehavior.AllowGet );
        }
            return Json(new { result = "error"}, JsonRequestBehavior.AllowGet );
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
        public ActionResult CancelMeeting(int ID)
        {
            Dc.CancelMeeting(ID);
            var getUser = Dc.Schedules.Where(a => a.SessionID == ID).FirstOrDefault();
            var attendee = Dc.Attendees.Where(a => a.SessionID == ID).Select(b=>b.AttendeeID).ToList();

            SendEmailToUsers(attendee, getUser.User.ToString());
            return RedirectToAction("Schedule", "User", new { ID = getUser.UserID });
        }
        public void SendEmailToUsers(List<int> To, string Host)
        {
            MailMessage Mail = new MailMessage();

            Mail.From = new MailAddress("Mail@itsalllive.com");
             
            foreach(var person in To)
            {
                var personID = Dc.Users.Where(a => a.UserID == person).Select(b=>b.UserName).FirstOrDefault();
                Mail.To.Add(personID);
                Mail.Subject = "Your meeting has been cancelled";
                Mail.Body = "Your meeting with " + Host + " has been cancelled.";

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "Mail@itsalllive.com",
                        Password = "G00gl3!@"
                    };
                    smtp.Port = 465;
                    smtp.Credentials = credential;
                    smtp.Host = "smtpout.secureserver.net";
                    smtp.EnableSsl = true;
                    smtp.Send(Mail);
                }
            }
            
            
        }
    }

}