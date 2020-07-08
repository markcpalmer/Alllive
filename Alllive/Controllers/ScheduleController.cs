using Alllive.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public ActionResult RequestTutor(string StartTime, string EndTime)
        {
            var sendMessage = new MessageController();
            var m = new Message();

            //need to populate message object and then send it using message controller
            return View();
        }
    }
}