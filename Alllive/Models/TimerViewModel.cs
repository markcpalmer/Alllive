//-----------------------------------------------------------------------  
// <copyright file="HTMLDisplayViewModel.cs" company="None">  
//     Copyright (c) Allow to distribute this code and utilize this code for personal or commercial purpose.  
// </copyright>  
// <author>Asma Khalid</author>  
//-----------------------------------------------------------------------  

namespace MvcTimerPicker.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    /// <summary>  
    /// Timer view model class.  
    /// </summary>  
    public class TimerViewModel
    {
        /// <summary>  
        /// Gets or sets the HTML content property.  
        /// </summary>  
        [Required]
        [Display(Name = "Time")]
        public string Timer { get; set; }

        /// <summary>  
        /// Gets or sets message property.  
        /// </summary>  
        [Display(Name = "Message")]
        public string Message { get; set; }

        /// <summary>  
        /// Gets or sets a value indicating whether it is valid or not property.  
        /// </summary>  
        [Display(Name = "Is Valid")]
        public bool IsValid { get; set; }
    }
}