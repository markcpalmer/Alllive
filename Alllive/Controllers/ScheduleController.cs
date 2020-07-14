using Alllive.Helpers;
using Alllive.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Alllive.Controllers
{
    public class ScheduleController : AllLiveControllerBase
    {
        // GET: Schedule
        [HttpGet]
        public ActionResult RequestTutor(int tutorProfileID)
        {
            if (Session["AllliveUser"] != null)
            {
                var myUser = (UserModel)Session["AllliveUser"];
                ViewBag.myUserID = myUser.UserId;
            }

            var tutor = Dc.TutorProfiles.Find(tutorProfileID);
            if (tutor != null)
            {
                var user = Dc.Users.Find(tutor.UserID);
                var VM = new TutorViewModel(tutor, user);
                return View(VM);
            }
            else
            {
                return RedirectToAction("SearchTutor");
            }
        }

        [HttpPost]
        public ActionResult RequestTutor(string StartTime, string EndTime, int receiverID)
        {
           
            var m = new Message();
            m.SenderID = currentUser.UserId;
            m.ReceiverID = receiverID;
            var sender = Dc.Users.Find(m.SenderID);
            var receiver = Dc.Users.Find(m.ReceiverID);
            if(sender != null && receiver != null)
            {
                string body = sender.FirstName + " " + sender.LastName + " wants to schedule a session\r\n";
                body += StartTime + " to " + EndTime + "\r\n";
                m.Message1 = body;
                m.TimeStamp = DateTime.UtcNow;
                Dc.Messages.Add(m);
                Dc.SaveChanges();
                MailMessage message = Email.GetEmailMessage(receiver.UserName, receiver.FirstName + " " + receiver.LastName, "you have recieved a message", body);
                Email.SendMessage(message);
            }
           
            //need to populate message object and then send it using message controller
            return RedirectToAction("MessageCenter","Message",new { userID = receiverID});
        }
    }
}