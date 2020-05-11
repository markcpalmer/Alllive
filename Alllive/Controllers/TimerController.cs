//-----------------------------------------------------------------------  
// <copyright file="TimerController.cs" company="None">  
//     Copyright (c) Allow to distribute this code and utilize this code for personal or commercial purpose.  
// </copyright>  
// <author>Asma Khalid</author>  
//-----------------------------------------------------------------------  

namespace MvcTimerPicker.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Models;

    /// <summary>  
    /// Timer controller class.  
    /// </summary>  
    public class TimerController : Controller
    {
        #region Index view method.  

        #region Get: /Timer/Index method.  

        /// <summary>  
        /// Get: /Timer/Index method.  
        /// </summary>          
        /// <returns>Return index view</returns>  
        public ActionResult Index()
        {
            // Initialization/  
            TimerViewModel model = new TimerViewModel() { Timer = null, IsValid = false, Message = string.Empty };

            try
            {
            }
            catch (Exception ex)
            {
                // Info  
                Console.Write(ex);
            }

            // Info.  
            return this.View(model);
        }

        #endregion

        #region POST: /Timer/Index  

        /// <summary>  
        /// POST: /Timer/Index  
        /// </summary>  
        /// <param name="model">Model parameter</param>  
        /// <returns>Return - Response information</returns>  
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Index(TimerViewModel model)
        {
            try
            {
                // Verification  
                if (ModelState.IsValid)
                {
                    // Initialization.  
                    string format = "yyyy-MM-dd HH:mm:ss.fff";
                    string formatDisplay = "hh:mm tt";
                    DateTime dateTimeVal = Convert.ToDateTime(model.Timer);

                    // Timer Settings.  
                    string timerVal = dateTimeVal != null ? dateTimeVal.ToString(format, CultureInfo.InvariantCulture) : dateTimeVal.ToString();
                    string timerDisplay = dateTimeVal != null ? dateTimeVal.ToString(formatDisplay, CultureInfo.InvariantCulture) : dateTimeVal.ToString();

                    // Settings.  
                    ModelState.Clear();
                    model.Timer = timerVal;
                    model.Message = timerDisplay + " timer has been Set successfully!!";
                    model.IsValid = true;
                }
            }
            catch (Exception ex)
            {
                // Info  
                Console.Write(ex);
            }

            // Info  
            return this.View(model);
        }

        #endregion

        #endregion
    }
}