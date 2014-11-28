using System;
using System.Linq;
using System.Web.Mvc;
using WebGrease.Css.Extensions;
using Wizardsgroup.Utilities.Helpers;

namespace Pfizer.Web.Areas.Common.Controllers
{
    public class ButtonController : Controller
    {        
        public ActionResult ValidationActionForMultiple(Guid[] checkedRecords, string controllerName, string actionToExecute)
        {
            var parameter = checkedRecords.Select(guid => string.Format("checkedRecords={0}",guid.ToString())).ToArray();
            var queryString = string.Join("&", parameter);
            var url = GetUrl(controllerName, actionToExecute, queryString);
            return Redirect(url);
        }

        public ActionResult ValidationActionForSingle(Guid checkedRecord, string controllerName, string actionToExecute)
        {
            var queryString = string.Format("checkedRecord={0}", checkedRecord);
            var url = GetUrl(controllerName, actionToExecute, queryString);
            return Redirect(url);
        }

        private static string GetUrl(string controllerName,string actionToExecute,string queryString)
        {
            var area = GetAreaFromController(controllerName);
            return string.Format("~/{0}/{1}/{2}?{3}", area, controllerName, actionToExecute, queryString);
        }

        private static string GetAreaFromController(string controllerName)
        {
            var area = "Common";
            var controllerType = ReflectionHelper.Instance.GetTypesFromAssembly("Pfizer.Web").SingleOrDefault(o => o.Name == string.Format("{0}Controller", controllerName));
            if (controllerType == null || controllerType.Namespace == null) return area;
            var index = 0;
            controllerType.Namespace.Split('.').ForEach(name =>
            {
                index++;
                if (name.Equals("Areas"))
                {
                    area = controllerType.Namespace.Split('.')[index];
                }
            });

            return area;
        }
	}
}