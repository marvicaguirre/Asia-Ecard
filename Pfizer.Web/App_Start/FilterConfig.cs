using System.Web.Mvc;
using Pfizer.Web.Code;
using System.Linq;
using Pfizer.Web.Areas.Common.Controllers;
using Pfizer.Domain.ViewModels;

namespace Pfizer.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AuthorizeAttribute());
            filters.Add(new CustomActionFilter());
        }

        public class CustomActionFilter : ActionFilterAttribute
        {
            // This will be executed before processing the View
            public override void OnResultExecuted(ResultExecutedContext filterContext)
            {
                // ********************** Implementation for ReadOnly View **********************
                //
                // Hierarchy of value of readonly to consider by framework:
                //  1. ViewBag.ViewBagReadOnlyView
                //          @null = do nothing
                //          @false = page is editable
                //          @true = page is locked
                //  2. Model.ReadOnlyView
                //          @did not inherit AbstractBaseViewModel = #1 will be considered
                //          @inherit AbstractBaseViewModel
                //              @null = #1 will be considered
                //              @false = page is editable
                //              @true = page is locked
                //  3. Area of Controller
                //          @same Area = #2 will be considered
                //          @different Area = page is locked
                //  4. ViewBag.ViewBagReadOnlyBypassArea 
                //          @null = #3 will be considered
                //          @false = #3 will be considered
                //          @true = #2 will be considered
                //  5. ViewBag.ViewBagReadOnlyViewOverrideWithThis
                //          @null = #4 will be considered
                //          @false = page is editable
                //          @true = page is locked
                //
                // ****************************************************************************** 
                // Note: pending, display page from different area as editable
                //
                // ****************************************************************************** 


                // lets check for the current result type, we only want to process ViewResult or PartialViewResult
                // and controller is not ActivityNotificationController and it must inherit AbstractCommonController
                var controller = filterContext.Controller;
                if ((filterContext.Result.GetType() == typeof(ViewResult) ||
                    filterContext.Result.GetType() == typeof(PartialViewResult)) &&
                    !controller.GetType().Equals(typeof(ActivityNotificationController)) &&
                    controller.GetType().IsSubclassOf(typeof(AbstractCommonController)))
                {
                    // will hold the final value of readonly
                    bool? isreadonly = null;

                    // #5. ViewBag.ViewBagReadOnlyViewOverrideWithThis
                    isreadonly = ((AbstractCommonController)controller).ViewBagReadOnlyViewOverrideWithThis;
                    if (isreadonly == null)
                    {
                        // #4. ViewBag.ViewBagReadOnlyBypassArea 
                        isreadonly = ((AbstractCommonController)controller).ViewBagReadOnlyBypassArea;
                        if (isreadonly == null || isreadonly == false)
                        {
                            // #3. Area of Controller
                            isreadonly = ModuleCategory.Instance.IsActiveTab(
                                ((AbstractCommonController)controller).ViewBagActiveModule,
                                controller.ToString().Split('.').Last());
                            if (isreadonly == true) // active tab
                            {
                                // #2. Model.ReadOnlyView
                                isreadonly = ProcessLevel2(controller);
                            }
                            else
                            {
                                isreadonly = true;
                            }
                        }
                        else
                        {
                            // #2. Model.ReadOnlyView
                            isreadonly = ProcessLevel2(controller);
                        }
                    }

                    // finally we can now inject readonly
                    filterContext.HttpContext.Response.Write(
                        string.Format(
                            "<input id=\"DynamicReadOnlyView\" name=\"DynamicReadOnlyView\" type=\"hidden\" value=\"{0}\">",
                            isreadonly.GetValueOrDefault(false).ToString().ToLower()));

                }

                // return control to base
                base.OnResultExecuted(filterContext);
            }

            internal bool? ProcessLevel2(ControllerBase controller)
            {
                bool? isreadonly = false;
                var model = controller.ViewData.Model;
                if (model == null || !model.GetType().IsSubclassOf(typeof(AbstractBaseViewModel)))
                {
                    // @did not inherit AbstractBaseViewModel
                    // #1. 
                    isreadonly = ((AbstractCommonController)controller).ViewBagReadOnlyView;
                }
                else
                {
                    // @inherit AbstractBaseViewModel
                    // #1.
                    isreadonly = ((AbstractBaseViewModel) model).ReadOnlyView;
                }
                return isreadonly;
            }
        }
    }
}