#define ENABLE_SECURITY_CHECK

using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using Wizardsgroup.Core.Web.Helpers;

namespace Wizardsgroup.Core.Web.Extensions
{
    public static class HtmlButtonHelperExtension
    {
        private const string NBSP = "&nbsp;";

        public static MvcHtmlString CustomButtonEntry(this HtmlHelper helper, string buttonText, string action, string controller, string methodName, string modalTitle, int? modalWidth, int? modalHeight, int? buttonWidth, string gridName, string parentId = "0", object routeValue = null, bool autoCloseModalDialog = true)
        {
            if (helper == null) return null;
            if (string.IsNullOrWhiteSpace(buttonText)) return null;

            var url = new UrlHelper(helper.ViewContext.RequestContext);

            string theUrl = routeValue != null
                                 ? url.Action(action, controller) + "/" + routeValue
                                 : url.Action(action, controller);
            int _modalWidth = modalWidth.HasValue ? modalWidth.Value : 0;
            int _modalHeight = modalHeight.HasValue ? modalHeight.Value : 0;
            int _buttonWidth = buttonWidth.HasValue ? buttonWidth.Value : 100;

            string btnClass = "buttonEntryClass";
            btnClass += GetDefaultButtonStyle();
            string bootStrapIcon = GetBootStrapIconForCreateButton();

            var attrs = new List<dynamic>
                            {
                                new  { Key = "class", Value = string.Format("{0}", btnClass)}
                                , new  { Key = "url", Value = string.Format("{0}", theUrl)}
                                , new  { Key = "modaltitle", Value = string.Format("{0}", modalTitle)}
                                , new  { Key = "modalwidth", Value = string.Format("{0}", _modalWidth)}
                                , new  { Key = "modalheight", Value = string.Format("{0}", _modalHeight)}
                                , new  { Key = "style", Value = string.Format("width:{0}px", _buttonWidth)}
                                , new  { Key = "gridname", Value = string.Format("{0}", gridName)}
                                , new  { Key = "actionName", Value = string.Format("{0}", action)}
                                , new  { Key = "parentId", Value = string.Format("{0}", parentId)}
                                , new  { Key = "methodName", Value = string.Format("{0}", methodName)}
                                , new  { Key = "autoClose", Value = string.Format("{0}", autoCloseModalDialog)}
                                , !IsRequestedActionValid(action, controller) ? new  { Key = "disabled", Value =true } : new  { Key = "openAccess", Value =true }
                            };
            var attrBuilder = GenerateAttributes(attrs);
            string returnMvcHtmlString = string.Format("<a {0}>{1}{2}</a>", attrBuilder, bootStrapIcon, buttonText);

            return new MvcHtmlString(returnMvcHtmlString);
        }




        public static MvcHtmlString CustomButtonEntry(this HtmlHelper helper, string buttonText, string action, string controller, string modalTitle, int? modalWidth, int? modalHeight, int? buttonWidth, string gridName, string parentId = "0", object routeValue = null, bool autoCloseModalDialog = true)
        {
            if (helper == null) return null;
            if (string.IsNullOrWhiteSpace(buttonText)) return null;

            var url = new UrlHelper(helper.ViewContext.RequestContext);

            string theUrl = routeValue != null
                                 ? url.Action(action, controller) + "/" + routeValue
                                 : url.Action(action, controller);
            int _modalWidth = modalWidth.HasValue ? modalWidth.Value : 0;
            int _modalHeight = modalHeight.HasValue ? modalHeight.Value : 0;
            int _buttonWidth = buttonWidth.HasValue ? buttonWidth.Value : 100;

            string btnClass = "buttonEntryClass";
            btnClass += GetDefaultButtonStyle();
            string bootStrapIcon = GetBootStrapIconForCreateButton();

            var attrs = new List<dynamic>
                            {
                                new  { Key = "class", Value = string.Format("{0}", btnClass)}
                                , new  { Key = "url", Value = string.Format("{0}", theUrl)}
                                , new  { Key = "modaltitle", Value = string.Format("{0}", modalTitle)}
                                , new  { Key = "modalwidth", Value = string.Format("{0}", _modalWidth)}
                                , new  { Key = "modalheight", Value = string.Format("{0}", _modalHeight)}
                                , new  { Key = "style", Value = string.Format("width:{0}px", _buttonWidth)}
                                , new  { Key = "gridname", Value = string.Format("{0}", gridName)}
                                , new  { Key = "actionName", Value = string.Format("{0}", action)}
                                , new  { Key = "parentId", Value = string.Format("{0}", parentId)}
                                , new  { Key = "autoClose", Value = string.Format("{0}", autoCloseModalDialog)}
                                , !IsRequestedActionValid(action, controller) ? new  { Key = "disabled", Value =true } : new  { Key = "openAccess", Value =true }
                            };
            var attrBuilder = GenerateAttributes(attrs);
            string returnMvcHtmlString = string.Format("<a {0}>{1}{2}</a>", attrBuilder, bootStrapIcon, buttonText);

            return new MvcHtmlString(returnMvcHtmlString);
        }

        public static MvcHtmlString CustomButtonDelete(this HtmlHelper helper, string buttonText, string action, string targetAction, string controller, string modalTitle, int? modalWidth, int? modalHeight, int? buttonWidth, string gridName)
        {
            var _action = string.IsNullOrEmpty(action) ? "DisplaySelectedItems" : action;
            var _targetAction = string.IsNullOrEmpty(targetAction) ? "DeleteMultipleConfirmed" : targetAction;

            var url = new UrlHelper(helper.ViewContext.RequestContext);

            string _theUrl = url.Action(_action, controller);
            string _theTargetUrl = url.Action(_targetAction, controller);
            int _modalWidth = modalWidth.HasValue ? modalWidth.Value : 0;
            int _modalHeight = modalHeight.HasValue ? modalHeight.Value : 0;
            int _buttonWidth = buttonWidth.HasValue ? buttonWidth.Value : 100;


            string btnClass = "buttonDeleteClass";
            btnClass += GetDefaultButtonStyle();
            string bootStrapIcon = GetBootStrapIconForDeleteButton();
            var attrs = new List<dynamic>
                            {
                                new  { Key = "class", Value = string.Format("{0}", btnClass)}
                                , new  { Key = "url", Value = string.Format("{0}", _theUrl)}
                                , new  { Key = "targetUrl", Value = string.Format("{0}", _theTargetUrl)}
                                , new  { Key = "modaltitle", Value = string.Format("{0}", modalTitle)}
                                , new  { Key = "modalwidth", Value = string.Format("{0}", _modalWidth)}
                                , new  { Key = "modalheight", Value = string.Format("{0}", _modalHeight)}
                                , new  { Key = "style", Value = string.Format("width:{0}px", _buttonWidth)}
                                , new  { Key = "gridname", Value = string.Format("{0}", gridName)}
                                , !IsRequestedActionValid(_targetAction, controller) ? new  { Key = "disabled", Value =true} : new  { Key = "openAccess", Value =true }
                            };
            var attrBuilder = GenerateAttributes(attrs);
            string returnMvcHtmlString = string.Format("<a {0}>{1}{2}</a>", attrBuilder, bootStrapIcon, buttonText);

            return new MvcHtmlString(returnMvcHtmlString);
        }

        public static MvcHtmlString CustomButtonDrop(this HtmlHelper helper, string buttonText, string action, string targetAction, string controller, string modalTitle, int? modalWidth, int? modalHeight, int? buttonWidth, string gridName)
        {
            var _action = string.IsNullOrEmpty(action) ? "DisplaySelectedItems" : action;
            var _targetAction = string.IsNullOrEmpty(targetAction) ? "DropMultipleConfirmed" : targetAction;

            var url = new UrlHelper(helper.ViewContext.RequestContext);

            if (!IsRequestedActionValid(targetAction, controller))
            {
                return new MvcHtmlString(NBSP);
            }

            string _theUrl = url.Action(_action, controller);
            string _theTargetUrl = url.Action(_targetAction, controller);
            int _modalWidth = modalWidth.HasValue ? modalWidth.Value : 0;
            int _modalHeight = modalHeight.HasValue ? modalHeight.Value : 0;
            int _buttonWidth = buttonWidth.HasValue ? buttonWidth.Value : 100;


            string btnClass = "buttonDropClass";
            btnClass += GetDefaultButtonStyle();
            string bootStrapIcon = GetBootStrapIconForDeleteButton();
            var attrs = new List<dynamic>
                            {
                                new  { Key = "class", Value = string.Format("{0}", btnClass)}
                                , new  { Key = "url", Value = string.Format("{0}", _theUrl)}
                                , new  { Key = "targetUrl", Value = string.Format("{0}", _theTargetUrl)}
                                , new  { Key = "modaltitle", Value = string.Format("{0}", modalTitle)}
                                , new  { Key = "modalwidth", Value = string.Format("{0}", _modalWidth)}
                                , new  { Key = "modalheight", Value = string.Format("{0}", _modalHeight)}
                                , new  { Key = "style", Value = string.Format("width:{0}px", _buttonWidth)}
                                , new  { Key = "gridname", Value = string.Format("{0}", gridName)}
                                , !IsRequestedActionValid(_targetAction, controller) ? new  { Key = "disabled", Value =true } : new  { Key = "openAccess", Value =true }
                            };
            var attrBuilder = GenerateAttributes(attrs);
            string returnMvcHtmlString = string.Format("<a {0}>{1}{2}</a>", attrBuilder, bootStrapIcon, buttonText);

            return new MvcHtmlString(returnMvcHtmlString);
        }

        public static MvcHtmlString CustomButtonModal(this HtmlHelper helper, string buttonText, string action, string controller, string modalTitle, int? modalWidth, int? modalHeight, int? buttonWidth, string gridName, object routeValue = null)
        {
            var url = new UrlHelper(helper.ViewContext.RequestContext);

            //string theUrl = routeValue != null ? url.Action(action, controller) + "/" + routeValue.ToString() : url.Action(action, controller);
            string theUrl = url.Action(action, controller);
            int _modalWidth = modalWidth.HasValue ? modalWidth.Value : 0;
            int _modalHeight = modalHeight.HasValue ? modalHeight.Value : 0;
            int _buttonWidth = buttonWidth.HasValue ? buttonWidth.Value : 100;

            var attrs = new List<dynamic>
        {
                                new  { Key = "class", Value = string.Format("{0}", "buttonModalClass")}
                                , new  { Key = "url", Value = string.Format("{0}", theUrl)}
                                , new  { Key = "modaltitle", Value = string.Format("{0}", modalTitle)}
                                , new  { Key = "modalwidth", Value = string.Format("{0}", _modalWidth)}
                                , new  { Key = "modalheight", Value = string.Format("{0}", _modalHeight)}
                                , new  { Key = "style", Value = string.Format("width:{0}px", _buttonWidth)}
                                , new  { Key = "gridname", Value = string.Format("{0}", gridName)}
                                , new  { Key = "actionName", Value = string.Format("{0}", action)}
                                , new  { Key = "parentId", Value = string.Format("{0}", routeValue)}
                                , new  { Key = "autoClose", Value = string.Format("{0}", false)}
                                , !IsRequestedActionValid(action, controller) ? new  { Key = "disabled", Value =true } : new  { Key = "openAccess", Value =true }
                            };
            var attrBuilder = GenerateAttributes(attrs);
            string returnMvcHtmlString = string.Format("<a {0}>{1}</a>", attrBuilder, buttonText);
            return new MvcHtmlString(returnMvcHtmlString);
        }

        public static MvcHtmlString CustomButtonToggle(this HtmlHelper helper, string controller, string modalTitle, int? modalWidth, int? modalHeight, int? buttonWidth, string gridName)
        {
            string _action = "ConfirmItems";
            string _targetAction = "ToggleStatus";

            var url = new UrlHelper(helper.ViewContext.RequestContext);

            string _theUrl = url.Action(_action, controller);
            string _theTargetUrl = url.Action(_targetAction, controller);
            int _modalWidth = modalWidth.HasValue ? modalWidth.Value : 0;
            int _modalHeight = modalHeight.HasValue ? modalHeight.Value : 0;
            int _buttonWidth = buttonWidth.HasValue ? buttonWidth.Value : 100;

            string btnClass = "buttonToggleClass";
            btnClass += GetDefaultButtonStyle();
            string bootStrapIcon = "";
            var attrs = new List<dynamic>
                            {
                                new  { Key = "class", Value = string.Format("{0}", btnClass)}
                                , new  { Key = "url", Value = string.Format("{0}", _theUrl)}
                                , new  { Key = "targetUrl", Value = string.Format("{0}", _theTargetUrl)}
                                , new  { Key = "modaltitle", Value = string.Format("{0}", modalTitle)}
                                , new  { Key = "modalwidth", Value = string.Format("{0}", _modalWidth)}
                                , new  { Key = "modalheight", Value = string.Format("{0}", _modalHeight)}
                                , new  { Key = "style", Value = string.Format("width:{0}px", _buttonWidth)}
                                , new  { Key = "gridname", Value = string.Format("{0}", gridName)}
                                , !IsRequestedActionValid(_targetAction, controller) ? new  { Key = "disabled", Value =true } : new  { Key = "openAccess", Value =true }
                            };
            var attrBuilder = GenerateAttributes(attrs);
            string returnMvcHtmlString = string.Format("<a {0}>{1}{2}</a>", attrBuilder, bootStrapIcon, "Toggle");

            return new MvcHtmlString(returnMvcHtmlString);
        }

        public static MvcHtmlString CustomButtonConfirm(this HtmlHelper helper, string buttonText, string action, string targetAction, string controller, string modalTitle, int? modalWidth, int? modalHeight, int? buttonWidth, string gridName)
        {
            var _action = action;
            var _targetAction = targetAction;

            var url = new UrlHelper(helper.ViewContext.RequestContext);

            string _theUrl = url.Action(_action, controller);
            string _theTargetUrl = url.Action(_targetAction, controller);
            int _modalWidth = modalWidth.HasValue ? modalWidth.Value : 0;
            int _modalHeight = modalHeight.HasValue ? modalHeight.Value : 0;
            int _buttonWidth = buttonWidth.HasValue ? buttonWidth.Value : 100;

            string btnClass = "buttonConfirmClass";
            btnClass += GetDefaultButtonStyle();
            string bootStrapIcon = string.Empty;
            var attrs = new List<dynamic>
                            {
                                new  { Key = "class", Value = string.Format("{0}", btnClass)}
                                , new  { Key = "url", Value = string.Format("{0}", _theUrl)}
                                , new  { Key = "targetUrl", Value = string.Format("{0}", _theTargetUrl)}
                                , new  { Key = "modaltitle", Value = string.Format("{0}", modalTitle)}
                                , new  { Key = "modalwidth", Value = string.Format("{0}", _modalWidth)}
                                , new  { Key = "modalheight", Value = string.Format("{0}", _modalHeight)}
                                , new  { Key = "style", Value = string.Format("width:{0}px", _buttonWidth)}
                                , new  { Key = "gridname", Value = string.Format("{0}", gridName)}
                                , !IsRequestedActionValid(_targetAction, controller) ? new  { Key = "disabled", Value =true } : new  { Key = "openAccess", Value =true }
                            };
            var attrBuilder = GenerateAttributes(attrs);
            string returnMvcHtmlString = string.Format("<a {0}>{1}{2}</a>", attrBuilder, bootStrapIcon, buttonText);

            return new MvcHtmlString(returnMvcHtmlString);
        }

        /// <summary>
        /// Custom button to perform an action from a call to a custom Javascript function.
        /// </summary>
        /// <param name="helper">The HtmlHelper object</param>
        /// <param name="buttonText">The caption of the button</param>
        /// <param name="action">The controller action</param>
        /// <param name="controller">The name of the controller</param>
        /// <param name="methodName">The Javascript function to execute</param>
        /// <param name="buttonWidth">The width of the button</param>
        /// <param name="gridName">The name of the Kendo grid</param>
        /// <param name="targetLevel">The level of depth where the result should display (0 = top level grid, 1..n = depth of grid next to the top level grid</param>
        /// <param name="routeValue">The route values</param>
        /// <returns></returns>
        public static MvcHtmlString CustomButtonAction(this HtmlHelper helper, string buttonText, string action, string controller, string methodName, int? buttonWidth, string gridName, int? targetLevel = null, object routeValue = null, bool ignoreSecurity = false)
        {
            string theUrl = new UrlHelper(helper.ViewContext.RequestContext).Action(action, controller);
            dynamic keyValuePair = !ignoreSecurity || !IsRequestedActionValid(action, controller)
                ? new {Key = "disabled", Value = true}
                : new {Key = "openAccess", Value = true};
            int _buttonWidth = buttonWidth.HasValue ? buttonWidth.Value : 100;
            var attrs = new List<dynamic>
                            {
                                new  { Key = "class", Value = string.Format("{0}", "buttonActionClass")}
                                , new  { Key = "url", Value = string.Format("{0}", theUrl)}
                                , new  { Key = "style", Value = string.Format("width:{0}px", _buttonWidth)}
                                , new  { Key = "gridname", Value = string.Format("{0}", gridName)}
                                , new  { Key = "parentId", Value = string.Format("{0}", routeValue)}
                                , new  { Key = "methodName", Value = string.Format("{0}", methodName)}
                                , new  { Key = "targetLevel", Value = string.Format("{0}", targetLevel)}
                                //, keyValuePair
                            };
            var attrBuilder = GenerateAttributes(attrs);
            string returnMvcHtmlString = string.Format("<a {0}>{1}</a>", attrBuilder, buttonText);

            return new MvcHtmlString(returnMvcHtmlString);
        }

        public static MvcHtmlString CustomButtonApproval(this HtmlHelper helper, string buttonText, string action, string controller, string modalTitle, int? modalWidth, int? modalHeight, int? buttonWidth, string gridName, bool isWithConfirmation = false, bool isSingleSelect = false)
        {
            string theUrl = new UrlHelper(helper.ViewContext.RequestContext).Action(action, controller);
            int _modalWidth = modalWidth.HasValue ? modalWidth.Value : 0;
            int _modalHeight = modalHeight.HasValue ? modalHeight.Value : 0;
            int _buttonWidth = buttonWidth.HasValue ? buttonWidth.Value : 100;

            var attrs = new List<dynamic>
                            {
                                new  { Key = "class", Value = string.Format("{0}", "buttonApprovalClass")}
                                , new  { Key = "url", Value = string.Format("{0}", theUrl)}
                                , new  { Key = "style", Value = string.Format("width:{0}px", _buttonWidth)}
                                , new  { Key = "gridname", Value = string.Format("{0}", gridName)}
                                , new  { Key = "modaltitle", Value = string.Format("{0}", modalTitle)}                                
                                , new  { Key = "modalwidth", Value = string.Format("{0}", _modalWidth)}
                                , new  { Key = "modalheight", Value = string.Format("{0}", _modalHeight)}
                                , new  { Key = "withConfirm", Value = string.Format("{0}", isWithConfirmation ? "yes" : "no" )}
                                , new  { Key = "singleselect", Value = string.Format("{0}", isSingleSelect ? "yes" : "no" )}
                                , !IsRequestedActionValid(action, controller) ? new  { Key = "disabled", Value =true } : new  { Key = "openAccess", Value =true }
                            };
            var attrBuilder = GenerateAttributes(attrs);
            string returnMvcHtmlString = string.Format("<a {0}>{1}</a>", attrBuilder, buttonText);
            return new MvcHtmlString(returnMvcHtmlString);
        }

        private static string GetDefaultButtonStyle()
        {
            if (IsBootStrapAvailable())
            {
                return " btn btn-info btn-block";
            }
            return string.Empty;
        }

        private static string GetBootStrapIconForCreateButton()
        {
            const string icon = "icon-plus";
            return IsBootStrapAvailable() ? string.Format(GetBootStrapIconTemplate(), icon) : "";
        }

        private static string GetBootStrapIconForDeleteButton()
        {
            const string icon = "icon-trash";
            return IsBootStrapAvailable() ? string.Format(GetBootStrapIconTemplate(), icon) : "";
        }

        private static string GetBootStrapIconTemplate()
        {
            //note: don't forget the space at the end
            return "<i class=\"{0}\"></i> ";
        }

        private static StringBuilder GenerateAttributes(IEnumerable<dynamic> attrs)
        {
            var attrBuilder = new StringBuilder();
            foreach (var attr in attrs)
            {
                attrBuilder.Append(string.Format("{0}=\"{1}\"", attr.Key, attr.Value));
                attrBuilder.Append(" ");
            }
            return attrBuilder;
        }

        private static bool IsRequestedActionValid(string action, string controller)
        {
            //return true;
            //TODO match correct action to function
            return SecurityHelper.IsRequestedActionValid(action, controller);
        }

        /// <summary>
        /// Check if the BootStrap CSS framework is available
        /// </summary>
        /// <returns></returns>
        private static bool IsBootStrapAvailable()
        {
            //TODO: get from config file
            return true;
        }

    }
}