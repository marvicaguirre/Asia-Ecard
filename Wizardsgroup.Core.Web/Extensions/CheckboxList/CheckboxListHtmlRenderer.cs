using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Wizardsgroup.Core.Web.Constants;
using Wizardsgroup.Core.Web.Enums;
using Wizardsgroup.Domain.Interfaces;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class CheckboxListHtmlRenderer<TModel, TValue> : ICheckboxListHtmlRenderer<TModel, TValue> where TValue : IEnumerable<IMultiSelectLookupValueField>
    {
        private readonly CheckboxListBuilderDataContainer<TModel, TValue> _checkboxListBuilderDataContainer;

        public CheckboxListHtmlRenderer(CheckboxListBuilderDataContainer<TModel,TValue> checkboxListBuilderDataContainer)
        {
            _checkboxListBuilderDataContainer = checkboxListBuilderDataContainer;
        }

        public MvcHtmlString RenderHtml()
        {
            var metadata = ModelMetadata.FromLambdaExpression(_checkboxListBuilderDataContainer.Expression, _checkboxListBuilderDataContainer.HtmlHelper.ViewData);
            var memberValue = metadata.Model as IEnumerable<IMultiSelectLookupValueField>;
            var memberExpression = (MemberExpression)_checkboxListBuilderDataContainer.Expression.Body;
            var memberName = memberExpression.Member.Name;

            var htmlToRender = new StringBuilder();
            var index = 0;
            htmlToRender.Append(string.Format("<div id='{0}' class=\"{1}\">", memberName, "editor-field"));
            _checkboxListBuilderDataContainer.LookupValueFields.ToList().ForEach(lookupValueField =>
            {
                htmlToRender.Append(_checkboxListBuilderDataContainer.HtmlHelper.Hidden(string.Format("{0}[{1}].Value", memberName, index), lookupValueField.Value).ToHtmlString());
                htmlToRender.Append(_checkboxListBuilderDataContainer.HtmlHelper.Hidden(string.Format("{0}[{1}].Text", memberName, index), lookupValueField.Text).ToHtmlString());
                htmlToRender.Append(_checkboxListBuilderDataContainer.HtmlHelper.CheckBox(string.Format("{0}[{1}].IsSelected", memberName, index), SetHtmlAttributesChecked(_checkboxListBuilderDataContainer.IsSelectedByDefault, lookupValueField, memberValue)));
                htmlToRender.Append(_checkboxListBuilderDataContainer.HtmlHelper.Label(string.Format("{0}[{1}].IsSelected", memberName, index), " " + lookupValueField.Text).ToHtmlString());
                htmlToRender.Append("<br/>");
                index++;
            });
            htmlToRender.Append("</div>");
            return MvcHtmlString.Create(htmlToRender.ToString()); 
        }


        public MvcHtmlString RenderHtml(CheckboxListForType type)
        {
            if (type == CheckboxListForType.TwoColumnsRightAlign)
            {
                var metadata = ModelMetadata.FromLambdaExpression(_checkboxListBuilderDataContainer.Expression, _checkboxListBuilderDataContainer.HtmlHelper.ViewData);
                var memberValue = metadata.Model as IEnumerable<IMultiSelectLookupValueField>;
                var memberExpression = (MemberExpression)_checkboxListBuilderDataContainer.Expression.Body;
                var memberName = memberExpression.Member.Name;

                var htmlToRender = new StringBuilder();
                var index = 0;
                htmlToRender.Append(string.Format("<div id='{0}' class=\"{1}\" style=\"width:968px\" >", memberName, "editor-field"));
                int colIndex = 0;
                _checkboxListBuilderDataContainer.LookupValueFields.ToList().ForEach(lookupValueField =>
                {
                    
                    if (colIndex == 0)
                    {
                        htmlToRender.Append("<div>");
                    }
                    htmlToRender.Append("<div style=\"width:50%;float:left;\">");
                    htmlToRender.Append(_checkboxListBuilderDataContainer.HtmlHelper.Hidden(string.Format("{0}[{1}].Value", memberName, index), lookupValueField.Value).ToHtmlString());
                    htmlToRender.Append(_checkboxListBuilderDataContainer.HtmlHelper.Hidden(string.Format("{0}[{1}].Text", memberName, index), lookupValueField.Text).ToHtmlString());
                    htmlToRender.Append(_checkboxListBuilderDataContainer.HtmlHelper.Label(string.Format("{0}[{1}].IsSelected", memberName, index), " " + lookupValueField.Text, new { style = "width: 85%; text-align: right; padding-right: 10px;" }).ToHtmlString());
                    htmlToRender.Append(_checkboxListBuilderDataContainer.HtmlHelper.CheckBox(string.Format("{0}[{1}].IsSelected", memberName, index), SetHtmlAttributesChecked(_checkboxListBuilderDataContainer.IsSelectedByDefault, lookupValueField, memberValue)));
                    htmlToRender.Append("</div>");
                    colIndex++;
                    if (colIndex > 1 || index == _checkboxListBuilderDataContainer.LookupValueFields.ToList().Count -1)
                    {
                        htmlToRender.Append("<div style=\"clear:both;\"/>");
                        htmlToRender.Append("</div>");
                        colIndex = 0;
                    }
                    index++;
                });
                htmlToRender.Append("</div>");
                return MvcHtmlString.Create(htmlToRender.ToString());
            }
            else if (type == CheckboxListForType.TwoColumnsLeftAlign)
            {
                var metadata = ModelMetadata.FromLambdaExpression(_checkboxListBuilderDataContainer.Expression, _checkboxListBuilderDataContainer.HtmlHelper.ViewData);
                var memberValue = metadata.Model as IEnumerable<IMultiSelectLookupValueField>;
                var memberExpression = (MemberExpression)_checkboxListBuilderDataContainer.Expression.Body;
                var memberName = memberExpression.Member.Name;

                var htmlToRender = new StringBuilder();
                var index = 0;
                htmlToRender.Append(string.Format("<div id='{0}' class=\"{1}\" style=\"width:968px\" >", memberName, "editor-field"));
                int colIndex = 0;
                _checkboxListBuilderDataContainer.LookupValueFields.ToList().ForEach(lookupValueField =>
                {

                    if (colIndex == 0)
                    {
                        htmlToRender.Append("<div>");
                    }
                    htmlToRender.Append("<div style=\"width:50%;float:left;\">");
                    htmlToRender.Append(_checkboxListBuilderDataContainer.HtmlHelper.Hidden(string.Format("{0}[{1}].Value", memberName, index), lookupValueField.Value).ToHtmlString());
                    htmlToRender.Append(_checkboxListBuilderDataContainer.HtmlHelper.Hidden(string.Format("{0}[{1}].Text", memberName, index), lookupValueField.Text).ToHtmlString());
                    htmlToRender.Append(_checkboxListBuilderDataContainer.HtmlHelper.CheckBox(string.Format("{0}[{1}].IsSelected", memberName, index), SetHtmlAttributesChecked(_checkboxListBuilderDataContainer.IsSelectedByDefault, lookupValueField, memberValue)));
                    htmlToRender.Append(_checkboxListBuilderDataContainer.HtmlHelper.Label(string.Format("{0}[{1}].IsSelected", memberName, index), " " + lookupValueField.Text, new { style = "width: 85%; text-align: left; padding-left: 10px;" }).ToHtmlString());
                    htmlToRender.Append("</div>");
                    colIndex++;
                    if (colIndex > 1 || index == _checkboxListBuilderDataContainer.LookupValueFields.ToList().Count - 1)
                    {
                        htmlToRender.Append("<div style=\"clear:both;\"/>");
                        htmlToRender.Append("</div>");
                        colIndex = 0;
                    }
                    index++;
                });
                htmlToRender.Append("</div>");
                return MvcHtmlString.Create(htmlToRender.ToString());
            }

            return RenderHtml();


        }

        private bool SetHtmlAttributesChecked(bool isSelectedByDefault, IMultiSelectLookupValueField lookupValueField, IEnumerable<IMultiSelectLookupValueField> dataStoreValues)
        {
            if (_checkboxListBuilderDataContainer.Mode == Mode.Create)
            {
                return isSelectedByDefault || lookupValueField.IsSelected;
            }
            var dataStoreValue = dataStoreValues.FirstOrDefault(o => o.Value == lookupValueField.Value);
            return dataStoreValue != null && dataStoreValue.IsSelected;
        }
    }
}