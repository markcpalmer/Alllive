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
        protected AllliveDBDataContext Dc = new AllliveDBDataContext();

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

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