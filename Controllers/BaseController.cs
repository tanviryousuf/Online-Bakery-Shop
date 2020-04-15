﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TanvirBakery.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string name = Session["username"] as string;
            if (string.IsNullOrEmpty(name))
            {
                Response.Redirect("/");
            }
        }
    }
}