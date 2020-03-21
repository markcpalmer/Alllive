using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Alllive.Models;


namespace Alllive.Controllers
{
    public class UserController : Controller
    {
        AllliveDBDataContext Dc = new AllliveDBDataContext();
        UserModel U = new UserModel();
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
        public ActionResult Register()
        {
            ViewBag.Message = "Registration Page";
            return View();
        }

        [HttpPost]
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

                Dc.insertregistereduser(RegisteredUser.UserName, RegisteredUser.LastName, RegisteredUser.FirstName, RegisteredUser.Password);
                return RedirectToAction("Login", "Home");

            }

        }
        #endregion

    }
}