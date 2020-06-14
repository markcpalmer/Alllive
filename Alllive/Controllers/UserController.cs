using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Alllive.Models;
using System.Web.Security;
using Alllive.Helpers;
using fs = System.IO;
using System.Configuration;
using System.Data.Entity;

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
            ViewBag.minuteOptions = Utilities.GetTimeFrames();
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
                        HeadLine=RegisteredUser.HeadLine,
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
        [HttpGet]
        public ActionResult Login()
        {
            ViewBag.Message = "Login Page";
            return View();

        }
        [OverrideAuthorization]
        [HttpPost]
        public ActionResult Login(LoginViewModel Login) //Nullable Int Type 
        {
            if (ModelState.IsValid)
            {
                var userExists = Dc.Users.Where(a => a.UserName == Login.UserName).FirstOrDefault();//defaults to null and doesn't error out
                if (userExists != null)
                {
                    bool isPasswordCorrect = Dc.passwords.Any(p => p.UserID == userExists.UserID && p.password1 == Login.Password);
                    if (!isPasswordCorrect)
                    {
                        ModelState.AddModelError("", "Invalid UserName or Password");
                        return View("Login", Login);
                    }
                        try
                        {
                        //FormsAuthentication common Windows/Microsoft Method. This tells your server to whitelist the cookie.
                        FormsAuthentication.SetAuthCookie(Login.UserName, true); //True is for Remember ME
                        PrepareUserSession(userExists);

                        return RedirectToAction("Schedule", "User");//redirects user to different action"                    
                    }
                    catch
                    {
                        return View("Login", Login);
                    }


                }
                else
                {
                    ModelState.AddModelError("", "Invalid UserName or Password");
                    //     return View(Login);
                    return View("Login", Login);
                }
            }
            return View("Login", Login);




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
        [Authorize]
        public ActionResult Schedule()
        {
          //  currentUser = (UserModel)(Session["AllliveUser"]);
            int UserID = currentUser.UserId;
            //var DisplaySchedule = Dc.UserSchedule(ID);
            var DisplaySchedule = Dc.Schedules.Join( Dc.ScheduleMeetings,
               a =>a.SessionID,
               b => b.SessionID,
               (a,b) => new { a, b }
                ).Where(a=>a.a.UserID == UserID)
                .Select(a=>a.b)
                .Include(a=>a.Attendees);
            return View(DisplaySchedule);
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
           
            if (Request.Files.Count > 0)
            {
                //Todo: save file to server!
                string baseFile = Request.Files[0].FileName;
                string extension = baseFile.Substring(baseFile.LastIndexOf('.'));
                baseFile = baseFile.Substring(0, baseFile.LastIndexOf('.'));

                var filePath = ConfigurationManager.AppSettings["FileRoot"] +  "\\Images\\" + tp.TutorProfileID + baseFile + extension;
                var thumbPath = ConfigurationManager.AppSettings["FileRoot"] +  "\\Images\\Thumbs\\" + tp.TutorProfileID + baseFile + extension;

                if (fs.File.Exists(filePath) || fs.File.Exists(thumbPath))
                {
                    var i = DateTime.Now.Ticks;
                    filePath = ConfigurationManager.AppSettings["FileRoot"] + "\\Images\\" + tp.TutorProfileID + baseFile + string.Format("_{0}", i) + extension;
                    thumbPath = ConfigurationManager.AppSettings["FileRoot"] + "\\Images\\Thumbs\\" + tp.TutorProfileID + baseFile + string.Format("_{0}", i) + extension;
                }
                Request.Files[0].SaveAs(filePath);
                //using (System.Drawing.Image originalImage = System.Drawing.Image.FromFile(filePath))
                //{
                //    using (var thumbImage = DataHelper.GetImageThumbnail(originalImage))
                //    {
                //        thumbImage.Save(thumbPath);
                //    }
                //}

                var imgEntity = new Image();
                Dc.Images.Add(imgEntity);
                Dc.SaveChanges();
                tp.ImageID = imgEntity.ImageID;
                Dc.SaveChanges();
                

          
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
            ViewBag.minuteOptions = Utilities.GetTimeFrames();

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