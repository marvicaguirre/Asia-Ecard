using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace Wizardsgroup.Core.Web.Extensions
{
    public static class HtmlReadOnlyFiedHelperExtension
    {
        private const string _defaultDateFormat = "MM/dd/yyyy";

        #region Readonly Textbox Linked
        public static MvcHtmlString CustomReadOnlyTextBoxLinkedFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, string entityModel, Expression<Func<TModel, int>> cascadeFromExpression, object htmlAttributes)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            string value = metadata.Model == null ? string.Empty : metadata.Model.ToString();

            var parentMetaData = ModelMetadata.FromLambdaExpression(cascadeFromExpression, helper.ViewData);
            string parentValue = parentMetaData.Model == null ? string.Empty : parentMetaData.Model.ToString();
            string cascadeFrom = parentMetaData.PropertyName;

            var attributes = new RouteValueDictionary(htmlAttributes);
            attributes["controltype"] = "kendoAutoCompleteReadOnlyLinkedFor";
            attributes["model"] = entityModel;
            attributes["parent"] = cascadeFrom;
            attributes["parentValue"] = parentValue;

            return helper.TextBox(ExpressionHelper.GetExpressionText(expression), value, attributes);
        }

        public static MvcHtmlString CustomReadOnlyTextBoxLinkedFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, string entityModel, Expression<Func<TModel, int>> cascadeFromExpression)
        {
            return CustomReadOnlyTextBoxLinkedFor(helper, expression, entityModel, cascadeFromExpression, null);
        }
        #endregion

        #region Readonly Textbox
        public static MvcHtmlString CustomReadOnlyTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, object htmlAttributes)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            string value = metadata.Model == null ? string.Empty : metadata.Model.ToString();
            var attributes = new RouteValueDictionary(htmlAttributes);
            attributes["controltype"] = "kendoAutoCompleteReadOnly";

            return helper.TextBox(ExpressionHelper.GetExpressionText(expression), value, attributes);
        }

        public static MvcHtmlString CustomReadOnlyTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression)
        {
            return CustomReadOnlyTextBoxFor(helper, expression, null);
        }
        #endregion

        #region Readonly Date Linked
        public static MvcHtmlString CustomReadOnlyDateTextBoxLinkedFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, string entityModel, Expression<Func<TModel, int>> cascadeFromExpression, string formatString, object htmlAttributes)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            string format = String.IsNullOrEmpty(formatString) ? _defaultDateFormat : formatString;
            DateTime date = metadata.Model == null ? new DateTime() : DateTime.Parse(metadata.Model.ToString());
            string value = date == new DateTime() ? String.Empty : date.ToString(format);

            var parentMetaData = ModelMetadata.FromLambdaExpression(cascadeFromExpression, helper.ViewData);
            string parentValue = parentMetaData.Model == null ? string.Empty : parentMetaData.Model.ToString();
            string cascadeFrom = parentMetaData.PropertyName;

            var attributes = new RouteValueDictionary(htmlAttributes);
            attributes["controltype"] = "kendoDatePickerReadOnlyLinkedFor";
            attributes["dateFormat"] = format;
            attributes["model"] = entityModel;
            attributes["parent"] = cascadeFrom;
            attributes["parentValue"] = parentValue;

            return helper.TextBox(ExpressionHelper.GetExpressionText(expression), value, attributes);
        }

        public static MvcHtmlString CustomReadOnlyDateTextBoxLinkedFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression,string entityModel, Expression<Func<TModel, int>> cascadeFromExpression)
        {
            return CustomReadOnlyDateTextBoxLinkedFor(helper, expression, entityModel,cascadeFromExpression,String.Empty, null);
        }

        public static MvcHtmlString CustomReadOnlyDateTextBoxLinkedFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression,string entityModel, Expression<Func<TModel, int>> cascadeFromExpression, string formatString)
        {
            return CustomReadOnlyDateTextBoxLinkedFor(helper, expression, entityModel, cascadeFromExpression, formatString, null);
        }
        #endregion

        #region Readonly Date
        public static MvcHtmlString CustomReadOnlyDateTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, string formatString, object htmlAttributes)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            string format = String.IsNullOrEmpty(formatString) ? _defaultDateFormat : formatString;
            DateTime date = metadata.Model == null ? new DateTime() : DateTime.Parse(metadata.Model.ToString());
            string value = date == new DateTime() ? String.Empty : date.ToString(format);
            var attributes = new RouteValueDictionary(htmlAttributes);
            attributes["controltype"] = "kendoDatePickerReadOnly";
            attributes["dateFormat"] = format;

            return helper.TextBox(ExpressionHelper.GetExpressionText(expression), value, attributes);
        }

        public static MvcHtmlString CustomReadOnlyDateTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression)
        {
            return CustomReadOnlyDateTextBoxFor(helper, expression, String.Empty, null);
        }

        public static MvcHtmlString CustomReadOnlyDateTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, string formatString)
        {
            return CustomReadOnlyDateTextBoxFor(helper, expression, formatString, null);
        }
        #endregion
    }
}