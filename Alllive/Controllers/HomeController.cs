using Alllive.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alllive.Controllers
{
    public class HomeController : Controller
    {
        private UserModel currentUser;
        public ActionResult Index()
        {
            if (Session != null && Session["AllliveUser"] != null)
            {
                currentUser = (UserModel)(Session["AllliveUser"]);

            }
            if (currentUser == null)
            {
                return View();
            }
            return RedirectToAction("Schedule", "User");

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Catalog()
        {
            ViewBag.Message = "Catalog Page";
            return View();

        }
    }
}