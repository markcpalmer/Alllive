using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Alllive.Models;

namespace Alllive.Controllers
{
    public class MessageController : Controller
    {
        protected AllliveDBEntities Dc = new AllliveDBEntities();

        // GET: Message
        public ActionResult Compose(int recieverID, int tutorProfileID)
        {
            var getValues = new Message();
            getValues.ReceiverID = recieverID;
            getValues.SenderID = tutorProfileID;
            return View(getValues);
        }

        [HttpPost]
        public ActionResult Compose(Message getMessage)
        {
            getMessage.TimeStamp = DateTime.UtcNow;

            Dc.Messages.Add(getMessage);
            Dc.SaveChanges();

            return RedirectToAction("Messaging","User", new { userID = getMessage.ReceiverID });
        }
    }
}