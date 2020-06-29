using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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

            return PartialView("_Message",getMessage);
        }

        public ActionResult MessageList(int userID)
        {
            ViewBag.myUserID = currentUser.UserId;
            ViewBag.receiverID = userID;

            int myID = currentUser.UserId;
            var storeMessages = Dc.Messages.Where(a => (a.ReceiverID == userID && a.SenderID == myID) || (a.ReceiverID == myID && a.SenderID == userID)).ToList();
            return PartialView(storeMessages);
        }
        public ActionResult MessageCenter(int? userID)
        {
            int myID = currentUser.UserId;

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
                );

            ViewBag.recieverID = userID;
            return View(messageUsers);



        }
    }
}