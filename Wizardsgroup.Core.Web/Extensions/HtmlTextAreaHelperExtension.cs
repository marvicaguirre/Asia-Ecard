using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace Wizardsgroup.Core.Web.Extensions
{
    public static class HtmlTextAreaHelperExtension
    {
        public static MvcHtmlString CustomTextAreaBoxFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, object htmlAttributes, int maxLength = 3000)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            string value = metadata.Model == null ? string.Empty : metadata.Model.ToString();
            var attributes = new RouteValueDictionary(htmlAttributes);
            attributes["controltype"] = "textArea";
            attributes["rows"] = "5";
            attributes["cols"] = "30";
            attributes["maxLength"] = maxLength.ToString();

            return helper.TextArea(ExpressionHelper.GetExpressionText(expression), value, attributes);
        }

        public static MvcHtmlString CustomTextAreaBoxFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, int maxLength = 3000)
        {
            return CustomTextAreaBoxFor<TModel, TValue>(helper, expression, null, maxLength);
        }
    }
}