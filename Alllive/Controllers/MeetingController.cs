using Alllive.Helpers;
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
                m.StartTime = DateTime.UtcNow.GetLocalTime(currentUser.TimeZone);
                m.EndTime = DateTime.UtcNow.AddMinutes(45).GetLocalTime(currentUser.TimeZone);
                m.EndDateBy = DateTime.UtcNow.AddMonths(1);
                m.HostUserID = currentUser.UserId;
                //todo: find users default timezone
                m.TimeZone = currentUser.TimeZone;
            }


            return View(m);
        }

        [HttpPost]
        [Authorize]
        public ActionResult SaveMeeting(ScheduleMeeting m)
        {


            if (ModelState.IsValid)
            {
                try
                {

                
                               //Time Sessions
                //TimeSpan Start, End = new TimeSpan();
                //Start = m.StartTime.TimeOfDay;
                //End = m.EndTime.TimeOfDay;
                m.StartTime = m.StartTime.GetFromLocalTime(m.TimeZone);
                m.EndTime = m.EndTime.GetFromLocalTime(m.TimeZone);

                    //TimeZone
                    if (m.SessionID > 0)
                    {
                        Dc.ScheduleMeetings.Attach(m);
                        Dc.Entry(m).State = System.Data.Entity.EntityState.Modified;
                        Dc.SaveChanges();
                    }
                    else
                    {
                        m.HostUserID = currentUser.UserId;
                        m.Active = "Y";
                        m.Date = m.StartTime.Date;
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
                            m.RepeatMonthRadio2 = false; m.Radio2List1 = 0; m.Radio2List2 = 0; m.EndDateBy = DateTime.UtcNow; m.EndDateAfter = 0; m.Subject = "";
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
                        // needs to input into the schedule table because thats the table that is populating
                        // the schedule
                        //
                        //redirects user to different action"                    
                        //This needs to change (Either Success view or take user to "Add to Calendar Page/Review Meeting Page"

                        // jquery is preventing any kind of action
                        // return RedirectToAction("Schedule", "User", new { ID = currentUser.UserId });//redirects user to different action"                    
                    }
                
            }
                catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                {
                    Exception raise = ex;
                    foreach (var validationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            // raise a new exception nesting  
                            // the current instance as InnerException  
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
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
                var findUser = Dc.Users.FirstOrDefault(u => u.UserName.ToLower() == m.Email.ToLower());
                if (findUser != null)
                {
                    m.UserID = findUser.UserID;
                }
                else
                {
                    m.UserID = null;
                }
                if (m.AttendeeID > 0)
                {
                    Dc.Attendees.Attach(m);
                    Dc.Entry(m).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    Dc.Attendees.Add(m);
                }

                if (m.UserID.HasValue && m.UserID.Value>0)
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
        public ActionResult Meeting()
        {
            //TODO: NEED to grab queryString from the meeting app/website
            //Need to pass meetingId instead of URL
            //
            var id= Request.QueryString["id"];
            var getMeeting = Dc.ScheduleMeetings.FirstOrDefault(a => a.SessionID.ToString() == id);
            ViewBag.Url = getMeeting.MeetingLink;
      
            return View();
        }

       [HttpGet]
        public ActionResult SubmitMeeting(string type)
        {
            var getMeeting = Dc.ScheduleMeetings.FirstOrDefault(a => a.MeetingLink == type);

            //getting the hourly rate
            var Rate = Dc.TutorProfiles.FirstOrDefault(a => a.TutorProfileID == getMeeting.HostUserID).Rate;
            SubmitMeeting submitMeeting = new SubmitMeeting()
            { 
                TutorProfileID = getMeeting.HostUserID ??0,
                Date = getMeeting.Date,
                StartTime = getMeeting.StartTime,
                EndTime = getMeeting.EndTime,
                HourlyRate = Rate,
                Subject = getMeeting.Subject,

            };

            


            return View(submitMeeting);
        }

        [HttpPost]
        public ActionResult SubmitMeeting(SubmitMeeting meeting)
        {
            BillingController billing = new BillingController();
            billing.CheckOut(Convert.ToInt64(meeting.HourlyRate));
            //create a review page in meeting folder
            return View();
        }
        public ActionResult CancelMeeting(int ID)
        {
            Dc.CancelMeeting(ID);
            var getUser = Dc.Schedules.Where(a => a.SessionID == ID).FirstOrDefault();
            var attendee = Dc.Attendees.Where(a => a.SessionID == ID).Select(b=>b.AttendeeID).ToList();

            SendEmailToUsers(attendee, getUser.User.FirstName + " " + getUser.User.LastName);
            return RedirectToAction("Schedule", "User");
        }
        public ActionResult ReactivateMeeting(int ID)
        {
            var meeting = Dc.ScheduleMeetings.Find(ID);
            if(meeting != null)
            {
                meeting.Active = "Y";
                Dc.SaveChanges();
                return RedirectToAction("ScheduleMeeting",new {sessionID=ID });
            }
            else
            {
                ViewBag.message = "Meeting not found";
                return RedirectToAction("Schedule", "User");
            }
        }
        public void SendEmailToUsers(List<int> To, string Host)
        {
                         
            foreach(var person in To)
            {
                var ToPerson = Dc.Users.FirstOrDefault(a => a.UserID == person);
                if(ToPerson != null)
                {
                    MailMessage message = Email.GetEmailMessage(ToPerson.UserName, ToPerson.FirstName + " " + ToPerson.LastName, string.Empty, string.Empty);
                    message.Subject = "Your meeting has been cancelled";
                    message.Body = "Your meeting with " + Host + " has been cancelled.";
                    Email.SendMessage(message);
                }
               
            }
            
            
        }
    }

}