using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pfizer.Web.Areas.Common.Controllers
{
    public class MenuController : Controller
    {
        //
        // GET: /Common/Menu/

        /// <summary>
        /// Load a menu item using a URL
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public virtual ViewResult Load(string url)
        {
            object myurl = url;
            return View(myurl);
        }

    }
}
