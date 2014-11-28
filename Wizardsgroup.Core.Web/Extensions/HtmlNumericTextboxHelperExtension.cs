using System;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace Wizardsgroup.Core.Web.Extensions
{
    public static class HtmlNumericTextboxHelperExtension
    {

        #region Linked numeric textbox
        public static MvcHtmlString CustomNumericTextBoxLinkedFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, string entityModel, Expression<Func<TModel, int>> cascadeFromExpression, string formatString, object htmlAttributes, int minimumValue)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            string value = metadata.Model == null ? string.Empty : metadata.Model.ToString();

            var parentMetaData = ModelMetadata.FromLambdaExpression(cascadeFromExpression, helper.ViewData);
            string parentValue = parentMetaData.Model == null ? string.Empty : parentMetaData.Model.ToString();
            string cascadeFrom = parentMetaData.PropertyName;

            var attributes = new RouteValueDictionary(htmlAttributes);
            attributes["controltype"] = "kendoNumericTextBoxLinkedFor";
            attributes["min"] = minimumValue;
            attributes["max"] = Int64.MaxValue;
            attributes["model"] = entityModel;
            attributes["parent"] = cascadeFrom;
            attributes["parentValue"] = parentValue;

            if (formatString != null && formatString.Trim() != string.Empty)
                attributes["format"] = formatString;
            else
                attributes["format"] = "#,##0";

            return helper.TextBox(ExpressionHelper.GetExpressionText(expression), value, attributes);
        }

        public static MvcHtmlString CustomNumericTextBoxLinkedFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression,string entityModel, Expression<Func<TModel, int>> cascadeFromExpression)
        {
            return CustomNumericTextBoxLinkedFor(helper, expression,entityModel,cascadeFromExpression, String.Empty, null, 0);
        }

        public static MvcHtmlString CustomNumericTextBoxLinkedFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, string entityModel, Expression<Func<TModel, int>> cascadeFromExpression, string formatString)
        {
            return CustomNumericTextBoxLinkedFor(helper, expression, entityModel, cascadeFromExpression, formatString, null, 0);
        } 
        #endregion

        #region Normal numderic textbox
        public static MvcHtmlString CustomNumericTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, string formatString, object htmlAttributes, int minimumValue, int decimals)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            string value = metadata.Model == null ? string.Empty : metadata.Model.ToString();
            var attributes = new RouteValueDictionary(htmlAttributes);
            attributes["controltype"] = "kendoNumericTextBox";
            attributes["min"] = minimumValue;
            attributes["max"] = Int64.MaxValue;
            attributes["decimals"] = decimals;

            if (formatString != null && formatString.Trim() != string.Empty)
                attributes["format"] = formatString;
            else
            {
                if (decimals > 0)
                {
                    StringBuilder sb = new StringBuilder(decimals);

                    sb.Append("#,##0.");
                    for (int i = 0; i < decimals; i++)
                    {
                        sb.Append("0");
                    }

                    attributes["format"] = sb.ToString();
                }
                else
                    attributes["format"] = "#,##0";
            }

            return helper.TextBox(ExpressionHelper.GetExpressionText(expression), value, attributes);
        }

        public static MvcHtmlString CustomNumericTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression)
        {
            return CustomNumericTextBoxFor(helper, expression, String.Empty, null, 0, 0);
        }

        public static MvcHtmlString CustomNumericTextBoxAllowNegativeFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, int decimals)
        {
            return CustomNumericTextBoxFor(helper, expression, String.Empty, null, int.MinValue, decimals);
        }

        public static MvcHtmlString CustomNumericTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, string formatString)
        {
            return CustomNumericTextBoxFor(helper, expression, formatString, null, 0, 0);
        } 
        #endregion
    }
}