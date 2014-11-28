using System;
using System.Web.Mvc;
using Pfizer.Repository;
using Wizardsgroup.Service.LinkedCascade;
using Wizardsgroup.Utilities.Helpers;

namespace Pfizer.Web.Areas.Common.Controllers
{
    public class TextBoxController : Controller
    {
        [OutputCache(Duration = 30, VaryByParam = "model;controlProperty;parentId")]
        public ActionResult GetValueFromLinkedControl(string model, string controlProperty, int parentId)
        {
            var linkedFor = new LinkedCascadeData(ReflectionHelper.Instance,new UnitOfWorkWrapper(), model,"Pfizer.Domain");
            var linkedControlValue = linkedFor.GetDataFromParent(parentId, controlProperty);
            return Json(linkedControlValue, JsonRequestBehavior.AllowGet);
        }

    }
}
