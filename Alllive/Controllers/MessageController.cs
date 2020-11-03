using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Web.Mvc;
using Alllive.Helpers;
using Alllive.Models;

namespace Alllive.Controllers
{
    [Authorize]
    public class MessageController : AllLiveControllerBase
    {

        // GET: Message
        public ActionResult Compose(int recieverID, int senderID)
        {
            var getValues = new Message();
            getValues.ReceiverID = recieverID;
            getValues.SenderID = senderID;
            return View(getValues);
        }

        [HttpPost]
        public ActionResult Compose(Message getMessage)
        {
            getMessage.TimeStamp = DateTime.UtcNow;

            Dc.Messages.Add(getMessage);
            Dc.SaveChanges();

            return RedirectToAction("MessageCenter","Message", new { userID = getMessage.ReceiverID });
        }

        [HttpPost]
        public ActionResult SendMessage(Message getMessage)
        {
            getMessage.TimeStamp = DateTime.UtcNow;

            Dc.Messages.Add(getMessage);
            Dc.SaveChanges();
            ViewBag.MyTimeZone = currentUser.TimeZone;
            string displayName = Dc.Users.Where(a => a.UserID == getMessage.SenderID).Select(a => a.FirstName + " " + a.LastName).FirstOrDefault();
            SendEmailToUsers(new List<int> { getMessage.ReceiverID }, displayName);
            return PartialView("_Message",getMessage);
        }

        public ActionResult MessageList(int userID)
        {
            ViewBag.myUserID = currentUser.UserId;
            ViewBag.receiverID = userID;

            int myID = currentUser.UserId;
            ViewBag.MyTimeZone = currentUser.TimeZone;

            var storeMessages = Dc.Messages.Where(a => (a.ReceiverID == userID && a.SenderID == myID) || (a.ReceiverID == myID && a.SenderID == userID)).ToList();
            return PartialView(storeMessages);
        }
        public ActionResult MessageCenter(int? userID)
        {
            int myID = currentUser.UserId;
            ViewBag.MyTimeZone = currentUser.TimeZone;
            ViewBag.myUserID = currentUser.UserId;
            var messageUsers = Dc.Messages
                .Where(a => a.ReceiverID == myID)
                .Select(a => new { userID = a.SenderID, a.TimeStamp })
                .Union(Dc.Messages
                .Where(a=>a.SenderID == myID)
                .Select(a => new { userID = a.ReceiverID, a.TimeStamp })
                ).OrderByDescending(a => a.TimeStamp)
                .GroupBy(a=>a.userID)
                .Select(a=>a.FirstOrDefault())
                .Join(Dc.Users,
                    m=>m.userID,
                    u=>u.UserID,
                    (m, u) => new MessageUserViewModel(){User=u,TimeStamp=m.TimeStamp}
                ).ToList();
            if(userID.HasValue && !messageUsers.Any(a=>a.User.UserID == userID.Value))
            {
                var receiver = Dc.Users.Find(userID.Value);
                messageUsers.Add(new MessageUserViewModel() { User = receiver, TimeStamp = DateTime.UtcNow });
            }

            var tutorID = Dc.TutorProfiles.FirstOrDefault(a => a.UserID == userID).TutorProfileID;
            ViewBag.receiverID = userID;
            ViewBag.tutorID = tutorID;
            return View(messageUsers);



        }
        private void SendEmailToUsers(List<int> To, string Host)
        {

            foreach (var person in To)
            {
                var ToPerson = Dc.Users.FirstOrDefault(a => a.UserID == person);
                if (ToPerson != null)
                {
                    MailMessage message = Email.GetEmailMessage(ToPerson.UserName, ToPerson.FirstName + " " + ToPerson.LastName, string.Empty, string.Empty);
                    message.Subject = "You have received a message";
                    message.Body = "You have received a message from " + Host;
                    Email.SendMessage(message);
                }

            }


        }
    }
}