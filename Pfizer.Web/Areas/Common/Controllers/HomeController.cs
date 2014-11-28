using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Wizardsgroup.Core.Web.SessionManagement;

namespace Pfizer.Web.Areas.Common.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Common/Home/

        public ActionResult UnderConstruction()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Welcome()
        {
            SessionManager.CheckSession();
            var userSessionContainer = SessionManager.GetUserSessionContainer();            
            if (userSessionContainer == null)
                Response.RedirectToRoute("DefaultArea", new { controller = "Account", action = "RedirectToLogin" });

            ViewBag.UserName = userSessionContainer.UserName;

            return View();
        }

        public ActionResult Default()
        {
            return View();
        }
    }
}
