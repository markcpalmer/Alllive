using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Alllive.Helpers;
using Alllive.Models;

namespace Alllive.Controllers
{
    public class NavigationController : AllLiveControllerBase
    {
        

      
        // GET: Navagation
        public ActionResult Index()
        {
            return View();
        }
        [OverrideAuthorization]
        public ActionResult DistanceLearning()
        {
            return View();
        }
       
        [OverrideAuthorization]
        public ActionResult OnlineYoga()
        {
            return View();
        }
    }
}