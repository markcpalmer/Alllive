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
            if (filterContext.HttpContext.Session["AllliveUser"] != null)
            {
                currentUser = (UserModel)filterContext.HttpContext.Session["AllliveUser"];
                ViewBag.myUserID = currentUser.UserId;
            }
            var actionDescriptor = filterContext.ActionDescriptor.GetCustomAttributes(true);
            //var controllerDecriptor = filterContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(true);
            if (actionDescriptor.Any(a => a.GetType() == typeof(AuthorizeAttribute)))
            { 
                if(currentUser==null)
                {
                    filterContext.Result = RedirectToAction("Login", "User");
                }
            }
        }

    }
}