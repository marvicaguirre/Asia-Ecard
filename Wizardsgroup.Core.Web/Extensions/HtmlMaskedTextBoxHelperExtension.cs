using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace Wizardsgroup.Core.Web.Extensions
{
    public static class HtmlMaskedTextBoxHelperExtension
    {
        public static MvcHtmlString CustomMaskedTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> helper,
            Expression<Func<TModel, TValue>> expression)
        {
            return CustomMaskedTextBoxFor(helper, expression, null, null);
        }

        public static MvcHtmlString CustomMaskedTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> helper,
            Expression<Func<TModel, TValue>> expression, String maskFormat, object htmlAttributes)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            String value = metadata.Model == null ? String.Empty : metadata.Model.ToString();

            var attributes = new RouteValueDictionary(htmlAttributes);
            attributes["id"] = metadata.PropertyName;
            attributes["name"] = metadata.PropertyName;
            attributes["autocomplete"] = "off";
            attributes["controltype"] = "kendoMaskedTextBox";

            if (String.IsNullOrEmpty(maskFormat))
                attributes["mask"] = "000-000-000-000";
            else
                attributes["mask"] = maskFormat;

            return helper.TextBox(ExpressionHelper.GetExpressionText(expression), value, attributes);
        }
    }
}