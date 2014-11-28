using System;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Wizardsgroup.Core.Web.Helpers;

namespace Wizardsgroup.Core.Web.Extensions
{
    public static class HtmlLabelForHelperExtension
    {
        public static MvcHtmlString CustomLabelFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, object htmlAttributes, bool overrideRequired = false)
        {
            var isRequired = DataDictionaryHelper.Instance.IsRequiredField("Pfizer.Repository", helper, expression);
            if (isRequired)
            {
                isRequired = !overrideRequired;
            }
            var htmlToRender = new StringBuilder();
            htmlToRender.Append(string.Format("<div class=\"editor-label {0}\">",isRequired ? "required-field" : string.Empty));
            htmlToRender.Append(htmlAttributes == null
                                    ? helper.LabelFor(expression).ToHtmlString()
                                    : helper.LabelFor(expression, htmlAttributes).ToHtmlString());
            htmlToRender.Append("</div>");            
            return MvcHtmlString.Create(htmlToRender.ToString());            
        }

        public static MvcHtmlString CustomLabelFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression)
        {
            return CustomLabelFor(helper,expression, null);
        }

    }
}