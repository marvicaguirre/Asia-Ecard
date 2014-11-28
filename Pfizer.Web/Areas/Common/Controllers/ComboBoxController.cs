using System;
using System.Linq;
using System.Web.Mvc;
using Pfizer.Repository;
using Wizardsgroup.Core.Interface;
using Wizardsgroup.Core.Web.Infrastructure.Storage;
using Wizardsgroup.Domain.Interfaces;
using Wizardsgroup.Service.Factories;
using Wizardsgroup.Utilities.Helpers;
using Wizardsgroup.Utilities.Extensions;

namespace Pfizer.Web.Areas.Common.Controllers
{
    public class ComboBoxController : Controller
    {
        private readonly IUnitOfWork _unitOfWork = new UnitOfWorkWrapper();
        //[OutputCache(Duration = 10, VaryByParam = "service;key")]
        [AllowAnonymous]        
        public ActionResult GetDataForDropDown(string service, string key)        
        {
            var filter = GetFilterForSearch();
            ILookupFunction customLookup = new LookupFactory(ReflectionHelper.Instance, _unitOfWork, "Pfizer.Service").Create<ICustomLookup>(service);
            customLookup.Specification = StatefulStorage.PerSession.Get<object>(key);
            customLookup.TextFilter = filter;
            var data = customLookup.GetRecordsForLookup();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [OutputCache(Duration = 30, VaryByParam = "service;cascadeFromValue;key")]
        [AllowAnonymous]
        public ActionResult GetDataForDropDownCascade(string service, string cascadeFromValue, string key)
        {
            if (string.IsNullOrEmpty(cascadeFromValue))
                return Json("", JsonRequestBehavior.AllowGet);

            ILookupFunction customLookup = new LookupFactory(ReflectionHelper.Instance, _unitOfWork, "Pfizer.Service").Create<ICustomLookup>(service);
            customLookup.Specification = StatefulStorage.PerSession.Get<object>(key);
            var data = customLookup.GetRecordsForCascade(cascadeFromValue.ToInteger());
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        private string GetFilterForSearch()
        {
            try
            {
                const int filterIndex = 3;
                const string filterQueryValue = "filter%5bfilters%5d%5b0%5d%5bvalue%5d=";
                var splitedQueryString = Request.QueryString.ToString().Split('&');
                if (splitedQueryString.Count() < 8) return string.Empty;
                var filter = splitedQueryString[filterIndex].Replace(filterQueryValue, string.Empty);
                return filter;
            }
            catch (Exception)
            {
                return string.Empty;
            }
            
        }
    }
}