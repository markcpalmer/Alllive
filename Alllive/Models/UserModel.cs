using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alllive.Models
{
    public class UserModel
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Email required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Last Name Required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "First Name Required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Password Required")]
        public string Password { get; set; }

        [NotMapped]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public bool RegisterAsTutor { get; set; }

        // This will be advance MVC/SQL
        // You will need a seperate Table that stores "LOCATION/FILE/EXT" of the image, that way you don't blow through your DB storage
        // For location, store the image in the IIS server
        public string Photo { get; set; } 

        public bool MathSubject { get; set; }

        public bool ReadingSubject { get; set; }

        public bool ScienceSubject { get; set; }

        public string Bio { get; set; } //Be best to set character limit and display it in View

        /// You don't want any of these required
        public bool MondayStart { get; set; }
        public DateTime MonStart { get; set; }
        public DateTime MonEnd { get; set; }

        public bool Tuesday { get; set; }
        public string TueStart { get; set; }
        public string TueEnd { get; set; }

        public bool Wednesday { get; set; }
        public string WedStart { get; set; }
        public string WedEnd { get; set; }

        public bool Thursday { get; set; }
        public string ThuStart { get; set; }
        public string ThuEnd { get; set; }

        public bool Friday { get; set; }
        public string FriStart { get; set; }
        public string FriEnd { get; set; }
        public bool Saturday { get; set; }
        public string SatStart { get; set; }
        public string SatEnd { get; set; }
        public bool Sunday { get; set; }
        public string SunStart { get; set; }
        public string SunEnd { get; set; }



        public UserModel GetUser(UserModel logon, bool cookie)
        {
            AllliveDBDataContext Dc = new AllliveDBDataContext();
            User user = new User();

            // Determine if the cookie exists
            if (cookie == false)
            {
                /// assign login password what is currently stored in logon.password.  
                /// because there is a cookie being stored.  
                /// If someone hacks password then the passord wouldn't match backend?

                logon.Password = logon.Password;
            }

            // Determine if user exists
            try
            {
                user = Dc.Users.Single(a => a.UserName == logon.UserName); //Gets UserName and Password

            }
            catch
            {
                logon = null;
                return logon;
            }

            return logon;
        }
    }
}