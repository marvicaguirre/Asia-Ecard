using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace Wizardsgroup.Core.Web.Extensions
{
    public static class HtmlTextBoxHelperExtension
    {
        #region TexboxLinkedFor

		public static MvcHtmlString CustomTextBoxLinkedFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression,string entityModel, Expression<Func<TModel,int>> cascadeFromExpression, string formatString, object htmlAttributes)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            string value = metadata.Model == null ? string.Empty : metadata.Model.ToString();

            var parentMetaData = ModelMetadata.FromLambdaExpression(cascadeFromExpression, helper.ViewData);
            string parentValue = parentMetaData.Model == null ? string.Empty : parentMetaData.Model.ToString();
            string cascadeFrom = parentMetaData.PropertyName;

            var attributes = new RouteValueDictionary(htmlAttributes);
            attributes["controltype"] = "kendoAutoCompleteTextBoxLinkedFor";
		    attributes["model"] = entityModel;
            attributes["id"] = metadata.PropertyName;
            attributes["name"] = metadata.PropertyName;
            attributes["parent"] = cascadeFrom;
            attributes["parentValue"] = parentValue;

            return helper.TextBox(ExpressionHelper.GetExpressionText(expression), value, attributes);
        }

        public static MvcHtmlString CustomTextBoxLinkedFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, string entityModel, Expression<Func<TModel, int>> cascadeFromExpression)
        {
            return CustomTextBoxLinkedFor(helper, expression, entityModel, cascadeFromExpression, String.Empty, null);
        }

        public static MvcHtmlString CustomTextBoxLinkedFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, string entityModel, Expression<Func<TModel, int>> cascadeFromExpression, string formatString)
        {
            return CustomTextBoxLinkedFor(helper, expression, entityModel, cascadeFromExpression, formatString, null);
        } 
	    #endregion

        #region TextBox
        public static MvcHtmlString CustomTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, string formatString, object htmlAttributes, int maxLength = 200)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            string value = metadata.Model == null ? string.Empty : metadata.Model.ToString();
            var attributes = new RouteValueDictionary(htmlAttributes);
            attributes["controltype"] = "kendoAutoCompleteTextBox";
            attributes["id"] = metadata.PropertyName;
            attributes["name"] = metadata.PropertyName;
            attributes["maxLength"] = maxLength.ToString();

            return helper.TextBox(ExpressionHelper.GetExpressionText(expression), value, attributes);
        }

        public static MvcHtmlString CustomTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, int maxLength = 200)
        {
            return CustomTextBoxFor(helper, expression, String.Empty, null, maxLength);
        }

        public static MvcHtmlString CustomTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, string formatString, int maxLength = 200)
        {
            return CustomTextBoxFor(helper, expression, formatString, null, maxLength);
        } 
        #endregion
    }
}