using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace Wizardsgroup.Core.Web.Extensions
{
    public static class HtmlDatePickerHelperExtension
    {
        private const string DefaultDateFormat = "MM/dd/yyyy";

        public static MvcHtmlString CustomDateTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, string formatString, object htmlAttributes)
        {
            DateTime date, temp;
            var metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            string format = String.IsNullOrEmpty(formatString) ? DefaultDateFormat : formatString;
            date = ( metadata.Model == null || !DateTime.TryParse(metadata.Model.ToString(),out temp)) ? new DateTime() : DateTime.Parse(metadata.Model.ToString());
            string value = date == new DateTime() ? String.Empty : date.ToString(format);
            var attributes = new RouteValueDictionary(htmlAttributes);
            attributes["controltype"] = "kendoDatePicker";
            attributes["dateFormat"] = format;

            return helper.TextBox(ExpressionHelper.GetExpressionText(expression), value, attributes);
        }

        public static MvcHtmlString CustomDateTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression)
        {
            return CustomDateTextBoxFor(helper, expression, String.Empty, null);
        }

        public static MvcHtmlString CustomDateTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, string formatString)
        {
            return CustomDateTextBoxFor(helper, expression, formatString, null);
        }
    }
}