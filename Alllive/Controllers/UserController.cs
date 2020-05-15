﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Alllive.Models;
using System.Web.Security;


namespace Alllive.Controllers
{
    public class UserController : AllLiveControllerBase
    {

        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        // GET: User
        [HttpGet]
        public ActionResult UserProfile(int? ID) //Nullable Int Type 
        {
            List<User> getUsers = new List<User>();

            /// get int ID 
            /// look at db to see if user id exists, if exists grab all related data
            /// store each record of data into the list
            /// display list
            if (ID == null)
            {
                return View(); // returns a view but which one?  how does it know the right view to choose
            }
            else
            {
                //TODO: Replace these 5 lines with one line of code
                //Use a SP.
                var getResults = Dc.Users.Where(user => user.UserID == ID).ToList();
                foreach (var item in getResults)
                {
                    getUsers.Add(item);
                }
                return View(getUsers);
            }
        }
        # region Register
        [OverrideAuthorization]
        public ActionResult Register()
        {
            ViewBag.Message = "Registration Page";
            string[] minutes = new string[]
            {
                "00","15","30","45"
            };
            var options = new List<string>();
            for(int h=0; h<24; h++)
            {
                string suffix = "am";
                if (h > 11)
                {
                    suffix = "pm";
                }
                foreach(string m in minutes)
                {
                    //if (h > 12)
                    //{
                    //    hourConverter = h + ":" + m;

                    //    DateTime dt = DateTime.ParseExact(hourConverter, "HH:mm", null, DateTimeStyles.None);
                    //    string time12 = dt.ToString("HH:mm");
                    //    options.Add(dt.ToShortTimeString().ToString());
                    //}
                    if (h == 0)
                    {
                        options.Add("12:" + m +" "+ suffix);
                    }
                    else
                    {
                        options.Add(h.ToString() + ":" + m +" "+ suffix);
                    }
                }
            }
            ViewBag.minuteOptions = options.Select(o=>new SelectListItem {Value=o,Text=o });
            return View();
        }

        [HttpPost]
        [OverrideAuthorization]
        public ActionResult Register(UserModel RegisteredUser)
        {
            //Comparison to see if the user exists
            User doesUserExists = new User();
            doesUserExists = Dc.Users
                .Where(exists => exists.UserName == RegisteredUser.UserName)
                .SingleOrDefault();

            if (doesUserExists != null)
            {
                //Error Message - User Exists
                //Typically if an item exists, you do an update here (just not for register page)

                ModelState.AddModelError("error", "User already exists");
                return View(RegisteredUser);
            }
            else
            {
                var newUser = new User()
                {
                    UserName = RegisteredUser.UserName,
                    FirstName = RegisteredUser.FirstName,
                    LastName = RegisteredUser.LastName

                };
                Dc.Users.Add(newUser);
                Dc.SaveChanges();
                var pw = new password() { UserID = newUser.UserID, password1 = RegisteredUser.Password };
                Dc.passwords.Add(pw);

                // RegisteredUser.UserId= Dc.insertregistereduser(RegisteredUser.UserName, RegisteredUser.LastName, RegisteredUser.FirstName, RegisteredUser.Password);
                RegisteredUser.UserId = newUser.UserID;
                
                //how do we get the UserID from the EF
                // sp looks like we are supposed to get the UserId back but its not returning.
                if (RegisteredUser.RegisterAsTutor)
                {
                    var tutor = new TutorProfile()
                    {
                        UserID = RegisteredUser.UserId,
                        Monday = RegisteredUser.MondayStart,
                        Tuesday = RegisteredUser.Tuesday,
                        Wednesday = RegisteredUser.Wednesday,
                        Thursday = RegisteredUser.Thursday,
                        Friday = RegisteredUser.Friday,
                        Saturday = RegisteredUser.Saturday,
                        Sunday = RegisteredUser.Sunday,
                        //map the rest on mhy own time
                        Bio = RegisteredUser.Bio,
                        SundayStart = RegisteredUser.SunStart,
                        SundayEnd = RegisteredUser.SunEnd,
                        SaturdayStart = RegisteredUser.SatStart,
                        SaturdayEnd = RegisteredUser.SatEnd,
                        MondayStart = RegisteredUser.MonStart,
                        MondayEnd = RegisteredUser.MonEnd,
                        TuesdayStart = RegisteredUser.TueStart,
                        TuesdayEnd = RegisteredUser.TueEnd,
                        WednesdayStart = RegisteredUser.WedStart,
                        WednesdayEnd = RegisteredUser.WedEnd,
                        ThursdayStart = RegisteredUser.ThuStart,
                        ThursdayEnd = RegisteredUser.ThuEnd,
                        FridayStart = RegisteredUser.FriStart,
                        FridayEnd = RegisteredUser.FriEnd,
                        Rate = RegisteredUser.Rate,
                        Math = RegisteredUser.MathSubject,
                        Reading = RegisteredUser.ReadingSubject,
                        Science = RegisteredUser.ScienceSubject,
                        StudentLevel = RegisteredUser.StudentLevel
                                  
                    };
                    Dc.TutorProfiles.Add(tutor);
                    
                }
                Dc.SaveChanges();
                return View("RegisterSuccess");

            }

        }
        #endregion
        #region Login
        [OverrideAuthorization]
        public ActionResult Login(UserModel Login) //Nullable Int Type 
        {
            var userExists = Dc.Users.Where(a => a.UserName == Login.UserName).FirstOrDefault();//defaults to null and doesn't error out

            if (userExists != null)
            {
                try
                {
                    //FormsAuthentication common Windows/Microsoft Method. This tells your server to whitelist the cookie.
                    FormsAuthentication.SetAuthCookie(Login.UserName, true); //True is for Remember ME
                    PrepareUserSession(userExists);
                    
                    return RedirectToAction("Schedule", "User", new { ID = userExists.UserID });//redirects user to different action"                    
                }
                catch { }

                return View();
            }
            else
            {
                return View();
            }


        }
        #endregion Login
        public void PrepareUserSession(User model)
        {
            // Assigning variables from the model parameter into the Session
            Session["AllLiveUser"] = new UserModel()
            {
                UserId = model.UserID,
                UserName = model.UserName,
                Password = model.passwords.ToString()
            };

            /// Assigning the session variables (password/username) into the UserModel 
            /// to reference valid user within each controller of the project.
            /// AUTHENTICATION.  the website knows who the user is at all times
            currentUser = (UserModel)(Session["AllliveUser"]);
        }

        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            Session["AllLiveUser"] = null;//Clear cookie
            return RedirectToAction("Index", "Home");
        }

        #region Schedule

        public ActionResult Schedule(int? ID)
        {
            var DisplaySchedule = Dc.UserSchedule(ID);
            return View(DisplaySchedule);
        }
        public ActionResult CancelMeeting(int ID)
        {
            Dc.CancelMeeting(ID);
            var getUser = Dc.Schedules.Where(a => a.SessionID == ID).FirstOrDefault();
            return RedirectToAction("Schedule","User",new { ID = getUser.UserID });
        }
        public ActionResult EditMeeting(int ID)
        { 
            ScheduleMeeting result = Dc.ScheduleMeetings.Where(a => a.SessionID == ID).FirstOrDefault();
            return View(result);
            
        }
        [HttpPost]
        public ActionResult EditMeeting(ScheduleMeeting User)
        {
            Dc.EditMeeting(User.SessionID,User.SessionName,User.Description,User.Date,User.StartTime,
                User.EndTime,User.TimeZone,User.Recurr,User.Frequency,User.RepeatDaily,User.RepeatWeekly,User.RepeatMonthly,
                User.Sunday,User.Monday,User.Tuesday,User.Wednesday,User.Thursday,User.Friday,User.Saturday,User.RepeatMonthRadio1,
                User.RepeatMonthRadio2,User.Radio2List1,User.Radio2List2,User.EndDateBy,User.EndDateAfter,User.MeetingLink
          );
            
            var getUser = Dc.Schedules.Where(a => a.SessionID == User.SessionID).FirstOrDefault();
            return RedirectToAction("Schedule", "User", new { ID = getUser.UserID });

        }
        #endregion
        [Authorize]
        public ActionResult SaveTutor(TutorProfile tp)
        {
            if (tp.TutorProfileID == 0)
            {
                Dc.TutorProfiles.Add(tp);
            }
            else
            {
                Dc.TutorProfiles.Attach(tp);
                
            }
            Dc.SaveChanges();
            return RedirectToAction("ViewProfile");
        }
        [Authorize]
        public ActionResult TutorRegistration(int? tutorProfileId)
        {
            var findTutor = Dc.TutorProfiles.FirstOrDefault(tp => tp.UserID == currentUser.UserId);
            var m = new TutorProfile();
            if (tutorProfileId.HasValue)
            {
                findTutor = Dc.TutorProfiles.FirstOrDefault(tp => tp.TutorProfileID == tutorProfileId.Value);
            }
            if (findTutor != null)
            {
                m = findTutor;
            }
            else
            {
                m.UserID = currentUser.UserId;
            }
            return View(m);
           
        }
        [Authorize]
        public ActionResult ViewProfile()
        {
            var tutorVM = Dc.Users.GroupJoin(Dc.TutorProfiles,
                    u => u.UserID,
                    tp => tp.UserID,
                    (u, tp) => new { tp, u }
                ).Where(a => a.u.UserID == currentUser.UserId

                ).ToList().Select(a => new TutorViewModel(a.tp.FirstOrDefault(), a.u))
                .FirstOrDefault();
            if (tutorVM == null)
            {
                tutorVM = new TutorViewModel();
            }
            return View(tutorVM);

        }

    }
}