using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace Wizardsgroup.Core.Web.Extensions
{
    public static class HtmlNumberOnlyTextBoxForHelperExtension
    {
        public static MvcHtmlString CustomNumberOnlyTextBoxLinkedFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, string entityModel, Expression<Func<TModel, int>> cascadeFromExpression, object htmlAttributes, int minimumValue)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            string value = metadata.Model == null ? string.Empty : metadata.Model.ToString();

            var parentMetaData = ModelMetadata.FromLambdaExpression(cascadeFromExpression, helper.ViewData);
            string parentValue = parentMetaData.Model == null ? string.Empty : parentMetaData.Model.ToString();
            string cascadeFrom = parentMetaData.PropertyName;

            var attributes = new RouteValueDictionary(htmlAttributes);
            attributes["controltype"] = "kendoNumericTextBoxNumberOnlyLinkedFor";            
            attributes["min"] = minimumValue;
            attributes["max"] = Int64.MaxValue;
            attributes["format"] = "#";
            attributes["decimals"] = "0";
            attributes["type"] = "number";
            attributes["model"] = entityModel;
            attributes["parent"] = cascadeFrom;
            attributes["parentValue"] = parentValue;

            return helper.TextBox(ExpressionHelper.GetExpressionText(expression), value, attributes);
        }

        public static MvcHtmlString CustomNumberOnlyTextBoxLinkedFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression,string entityModel, Expression<Func<TModel, int>> cascadeFromExpression)
        {
            return CustomNumberOnlyTextBoxLinkedFor(helper, expression,entityModel,cascadeFromExpression, null, 0);
        } 

        #region Normal numer only textbox
        public static MvcHtmlString CustomNumberOnlyTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, object htmlAttributes, int minimumValue)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            string value = metadata.Model == null ? string.Empty : metadata.Model.ToString();
            var attributes = new RouteValueDictionary(htmlAttributes);
            attributes["controltype"] = "kendoNumericTextBoxNumberOnly";
            attributes["min"] = minimumValue;
            attributes["max"] = Int64.MaxValue;
            attributes["format"] = "#";
            attributes["decimals"] = "0";
            attributes["type"] = "number";

            return helper.TextBox(ExpressionHelper.GetExpressionText(expression), value, attributes);
        }

        public static MvcHtmlString CustomNumberOnlyTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression)
        {
            return CustomNumberOnlyTextBoxFor(helper, expression, null, 0);
        } 
        #endregion
    }
}