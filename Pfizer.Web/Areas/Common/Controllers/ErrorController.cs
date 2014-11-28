using System;
using System.Web.Mvc;

namespace Pfizer.Web.Areas.Common.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Common/Error/

        public ActionResult Error500(string message)
        {
            ViewBag.Message = message;

            return View();
        }

        public ActionResult ShowError(string message)
        {
            ViewBag.Message = message;

            return View();
        }
    }
}