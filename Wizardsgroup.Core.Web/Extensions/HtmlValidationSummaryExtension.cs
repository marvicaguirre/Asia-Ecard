using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Wizardsgroup.Core.Web.Extensions
{
    public static class HtmlValidationSummaryExtension
    {
        public static MvcHtmlString CustomValidationSummary(this HtmlHelper helper)
        {
            var confirmOnSubmitOverride = "<script>confirmOnSubmitOverrideFx = function() {return true;}; _grid_refreshCallback = undefined;</script>";            
            if (_HackFixCheckForGuidWithParamNamed_Id_IsInvalid(helper.ViewData.ModelState))
                return new MvcHtmlString(confirmOnSubmitOverride);

            var returnMvcHtmlString = new StringBuilder();

            returnMvcHtmlString.Append("<fieldset class=\"validation-summary-errors\">");
            returnMvcHtmlString.Append("<legend>Please fix the following errors:</legend>");
            returnMvcHtmlString.Append("<ul class=\"field-validation-error\">");
            var errorMessage = string.Empty;
            foreach (ModelState modelState in helper.ViewData.ModelState.Values)
            {
                errorMessage = modelState.Errors.Aggregate(errorMessage, (current, error) => current + ("&nbsp; - " + error.ErrorMessage + "<br/>"));
            }
            returnMvcHtmlString.Append(errorMessage);

            returnMvcHtmlString.Append("</ul>");
            returnMvcHtmlString.Append("</fieldset>");
            returnMvcHtmlString.Append("<script>");
            //hack fix to re-initialized kendo controls
            returnMvcHtmlString.Append("$(function(){var dialog=$('#divEntry');_initKendoControls(dialog);});");
            returnMvcHtmlString.Append("confirmOnSubmitOverrideFx = function() {return true;};");            
            returnMvcHtmlString.Append("</script>");                   

            return new MvcHtmlString(returnMvcHtmlString.ToString());
        }

        private static bool _HackFixCheckForGuidWithParamNamed_Id_IsInvalid(ModelStateDictionary modelStateDictionary)
        {
            var returnValue = modelStateDictionary.IsValid;
            if (!modelStateDictionary.IsValid)
            {
                if (modelStateDictionary.Keys.Count == 1
                    && modelStateDictionary.Keys.FirstOrDefault(o=>o.Equals("id")) != null)
                {
                    returnValue = true;
                }
            }
            return returnValue;
        }
    }
}