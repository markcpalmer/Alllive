﻿using System;
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
using Stripe;
using Newtonsoft.Json;

namespace Alllive.Controllers
{
    public class ConfirmPaymentRequest
    {
        [JsonProperty("payment_method_id")]
        public string PaymentMethodId { get; set; }

        [JsonProperty("payment_intent_id")]
        public string PaymentIntentId { get; set; }
    }
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
           
            var m = new UserModel()
            {
                TimeZone = "Eastern Standard Time"
            };


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
                    LastName = RegisteredUser.LastName,
                    TimeZone = RegisteredUser.TimeZone,
                    isTutor = RegisteredUser.RegisterAsTutor
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
                Password = model.passwords.ToString(),
                TimeZone = model.TimeZone,
                isTutor = model.isTutor
            };
            ViewBag.isTutor = model.isTutor;
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
            DateTime todaysDate = DateTime.Now;
            //todaysDate = todaysDate.AddDays(-30); use for tutor
            var DisplaySchedule = Dc.Schedules.Join(Dc.ScheduleMeetings,
               a => a.SessionID,
               b => b.SessionID,
               (a, b) => new { a, b }
                ).Where(a => a.a.UserID == UserID && a.b.Active == "Y" && a.b.StartTime >= todaysDate) 
                
                .Select(a => a.b)
                .Include(a => a.Attendees)
                .OrderBy(a => a.StartTime)
                .ToList();
            
            foreach(var meeting in DisplaySchedule)
            {
                var user = Dc.Users.Find(meeting.HostUserID);
                if (user != null)
                {
                    meeting.Host = user.FirstName + " " + user.LastName;
                }
            }
                
           
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
                Dc.Entry(tp).State = System.Data.Entity.EntityState.Modified;


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
            if (string.IsNullOrEmpty(tutorVM.User.TimeZone))
            {
                tutorVM.User.TimeZone = "Eastern Standard Time";
            }
            ViewBag.ViewProfile = true;
            if (HttpContext.Request.QueryString["viewPage"] == "1")
            {
                ViewBag.Page = "1";
            }
            else if (HttpContext.Request.QueryString["viewPage"] == "2") { ViewBag.Page = "2"; }
            else { ViewBag.Page = "3"; }
            return View(tutorVM);

        }

        [HttpPost]
        [Authorize]
        public ActionResult ViewProfile(string viewPage)
        {
            if (viewPage == "1") {
                ViewBag.Page = "1";
            }
            else if (viewPage == "2") { ViewBag.Page = "2"; }
            else { ViewBag.Page = "3"; }
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult SaveUserProfile(User user)
        {
            Dc.Users.Attach(user);
            Dc.Entry(user).State = EntityState.Modified;
            Dc.SaveChanges();
            return Json(new { });
        }

        public ActionResult SubmitPayment(string cc, string exp, string cvc)
        {
            return StripeSendMoney();
        }
        public ActionResult StripeSendMoney()
        {
            ////Stripe.CustomerCreateOptions customer = new CustomerCreateOptions();
            ////customer.Email = "mark.palmer@itsalllive.com";

            //Stripe.CreditCardOptions card = new CreditCardOptions();
            //card.Name = "Mark Palmer";
            //card.Number = "4242 4242 4242 4242";
            //card.ExpYear = 2021;
            //card.ExpMonth = 07;
            //card.Cvc = "483";
            //Stripe.TokenCreateOptions tokenCreate = new TokenCreateOptions();
            //tokenCreate.Card = card;
            //Stripe.TokenService tokenService = new TokenService();
            //Stripe.Token token = tokenService.Create(tokenCreate);

            //Stripe.CustomerCreateOptions customer = new CustomerCreateOptions();
            //customer.Email = "mark.palmer@itsalllive.com";
            //customer.Source = token.Id;
            //customer.Phone = "4409359237";
            //var custService = new Stripe.CustomerService();
            //Stripe.Customer stpCustomer = custService.Create(customer);








            //// Set your secret key. Remember to switch to your live secret key in production!
            //// See your keys here: https://dashboard.stripe.com/account/apikeys
            //StripeConfiguration.ApiKey = "sk_test_51H4bEIAVJDEhYcbP8AniC54IhmNxi8AOAkQpTgSCdwJjXwd8eoYEZmpBdZPOn7mpkBhQWkuzYYIFUv1y8Y3ncnKO008t1vsMSK";

            //var options = new Stripe.ChargeCreateOptions
            //{
            //    Amount = Convert.ToInt32(100),
            //    Currency = "usd",
            //    ReceiptEmail = customer.Email,
            //    Customer = stpCustomer.Id


            //};
            //var service = new Stripe.ChargeService();
            //Stripe.Charge charge = service.Create(options);

            //var options = new PaymentIntentCreateOptions
            //{
            //    Amount = Convert.ToInt32(5),
            //    //Customer = "",
            //    Currency = "usd",
            //    PaymentMethodTypes = new List<string>
            //      {
            //        "card",
            //      },
            //    ReceiptEmail = "mark.palmer@itsalllive.com",
            //};

            //var service = new PaymentIntentService();
            //var paymentIntent = service.Create(options);
            // Set your secret key. Remember to switch to your live secret key in production!
            // See your keys here: https://dashboard.stripe.com/account/apikeys
            StripeConfiguration.ApiKey = "sk_test_51H4bEIAVJDEhYcbP8AniC54IhmNxi8AOAkQpTgSCdwJjXwd8eoYEZmpBdZPOn7mpkBhQWkuzYYIFUv1y8Y3ncnKO008t1vsMSK";

            var options = new PaymentIntentCreateOptions
            {
                Amount = 100,
                Currency = "usd",
                // Verify your integration in this guide by including this parameter
                Metadata = new Dictionary<string, string>
    {
      { "accept_a_payment", "accept_a_payment" },
    },
            };

            var service = new PaymentIntentService();
            var paymentIntent = service.Create(options);
            //return RedirectToAction("Index","Home");
            return generatePaymentResponse(paymentIntent);
        }
        private ActionResult generatePaymentResponse(PaymentIntent intent)
        {
            // Note that if your API version is before 2019-02-11, 'requires_action'
            // appears as 'requires_source_action'.
            if (intent.Status == "requires_action" &&
                intent.NextAction.Type == "use_stripe_sdk")
            {
                // Tell the client to handle the action
                return Json(new
                {
                    requires_action = true,
                    payment_intent_client_secret = intent.ClientSecret
                });
            }
            else if (intent.Status == "succeeded")
            {
                // The payment didn’t need any additional actions and completed!
                // Handle post-payment fulfillment
                return Json(new { success = true });
            }
            else
            {
                // Invalid status
                ModelState.AddModelError("", "Invalid PaymentIntent status");
                return View();
              //  return StatusCode(500, new { error = "Invalid PaymentIntent status" });
            }
        }
    }
}