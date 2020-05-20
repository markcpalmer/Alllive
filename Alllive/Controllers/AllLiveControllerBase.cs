using Alllive.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alllive.Controllers
{
    public class AllLiveControllerBase : Controller
    {
        protected UserModel currentUser;
        protected AllliveDBEntities Dc = new AllliveDBEntities();
        
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            var descriptor = filterContext.ActionDescriptor.GetCustomAttributes(true);
            if (descriptor.Any(a => a.GetType() == typeof(AuthorizeAttribute)))
            {


                if (filterContext.HttpContext.Session["AllliveUser"] != null)
                {
                    currentUser = (UserModel)filterContext.HttpContext.Session["AllliveUser"];
                }
                else
                {
                    filterContext.Result = RedirectToAction("Login", "Home");
                }
            }
        }

    }
}