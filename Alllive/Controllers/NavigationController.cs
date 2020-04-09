using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    }
}