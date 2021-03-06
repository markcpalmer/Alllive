﻿using Alllive.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alllive.Controllers
{
    public class SearchController : AllLiveControllerBase
    {
        // GET: Search
        [OverrideAuthorization]
        public ActionResult SearchTutor()
        {
            return View();
        }
        [OverrideAuthorization]
        public ActionResult SearchResults(SearchTutorModel m)
        {
            string search = (m.SearchText ?? "").ToLower();
            bool usesdays = (m.Sunday || m.Monday || m.Tuesday || m.Wednesday || m.Thursday || m.Friday || m.Saturday);
            var results = Dc.TutorProfiles.Join(Dc.Users,
                    tp => tp.UserID,
                    u => u.UserID,
                    (tp, u) => new { tp, u }
                ).Where(a =>
                (
                    string.IsNullOrEmpty(m.SearchText)||
                    a.tp.Bio.Contains(m.SearchText)|| 
                    (search=="math" && a.tp.Math)||
                    (search == "reading" && a.tp.Reading) ||
                    (search == "science" && a.tp.Science) 
                ) &&
                (a.tp.Rate >= m.RateLow && a.tp.Rate <= m.RateHigh)&&
               (!usesdays|| (a.tp.Sunday && m.Sunday) ||
                (a.tp.Monday && m.Monday) ||
                (a.tp.Tuesday && m.Tuesday) ||
                (a.tp.Wednesday && m.Wednesday) ||
                (a.tp.Thursday && m.Thursday) ||
                (a.tp.Friday && m.Friday) ||
                (a.tp.Saturday && m.Saturday) )
                ).ToList().Select(a => new TutorViewModel(a.tp, a.u));


            return PartialView(results);
        }
        public ActionResult ViewTutor(int tutorID)
        {
            if (Session["AllliveUser"] != null)
            {
                var myUser = (UserModel)Session["AllliveUser"];
                ViewBag.myUserID = myUser.UserId;
            }

            var tutor = Dc.TutorProfiles.Find(tutorID);
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
    }
}