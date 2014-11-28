using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Wizardsgroup.Domain.Interfaces;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class SingleFieldToMultiselectHtmlRenderer<TModel,TValue> : ISingleFieldToMultiselectHtmlRenderer<TModel, TValue>
    {
        private readonly MultiselectBuilderDataContainer<TModel,TValue> _container;

        public SingleFieldToMultiselectHtmlRenderer(MultiselectBuilderDataContainer<TModel,TValue> container)
        {
            _container = container;
        }

        public MvcHtmlString RenderHtml()
        {
            const string  htmlCloseTag = ">";
            const string htmlOpenTag = "<";
            const string htmlSlashTag = "/";
            var htmlToRender = new StringBuilder();            
            var memberValue = GetValueFromPropertyExpression();            
            htmlToRender.Append(_container.HtmlHelper.TextBoxFor(_container.Expression, new {@type = "hidden", @value=memberValue}).ToHtmlString());
            htmlToRender.Append(string.Format("{1}select id=\"{0}Multiselect\" name=\"{0}Multiselect\" multiple=\"multiple\" ", _container.Name, htmlOpenTag));
            SetMultiselectOptions(htmlToRender);
            htmlToRender.Append(htmlCloseTag);
            var listOfMemberValue = GetListOfMemberValueFromExpressionValue(memberValue);
            SetSelectedValueInMultiselect(listOfMemberValue, htmlToRender);
            htmlToRender.Append(string.Format("{0}{1}select{2}",htmlOpenTag,htmlSlashTag,htmlCloseTag));
            return MvcHtmlString.Create(htmlToRender.ToString());
        }

        private void SetSelectedValueInMultiselect(IEnumerable<string> listOfMemberValue, StringBuilder htmlToRender)
        {
            _container
                .LookupValueFields.ToList()
                .ForEach(item =>
                    {
                        item.IsSelected = listOfMemberValue.Any(idValue => idValue == item.Value);
                        htmlToRender.Append(string.Format("<option value=\"{0}\" {2}>{1}</option>", item.Value,item.Text,SetSelectedItemInOptionElement(item)));
                    });
        }

        private IEnumerable<string> GetListOfMemberValueFromExpressionValue(string memberValue)
        {
            var listOfMemberValue = new List<string>();
            if (memberValue != null)
            {
                var delimeter = string.IsNullOrEmpty(_container.FieldSetting.Delimeter)
                                    ? _container.FieldSetting.DefaultDelimeter
                                    : _container.FieldSetting.Delimeter;
                listOfMemberValue = memberValue.Split(delimeter.ToCharArray()).ToList();
            }
            return listOfMemberValue;
        }

        private string GetValueFromPropertyExpression()
        {            
            var metadata = ModelMetadata.FromLambdaExpression(_container.Expression, _container.HtmlHelper.ViewData);
            var memberValue = metadata.Model as string;
            return memberValue;
        }

        private void SetMultiselectOptions(StringBuilder htmlToRender)
        {
            htmlToRender.Append(string.Format("controltype=\"{0}\" ", "singleFieldMultiselect"));
            htmlToRender.Append(string.Format("delimeter=\"{0}\" ", string.IsNullOrEmpty(_container.FieldSetting.Delimeter) ? _container.FieldSetting.DefaultDelimeter : _container.FieldSetting.Delimeter));
            htmlToRender.Append(string.Format("filterable=\"{0}\" ", _container.FieldSetting.IsFilterable));
            htmlToRender.Append(string.Format("placeHolder=\"{0}\" ", _container.FieldSetting.PlaceHolder));
            htmlToRender.Append(string.Format("selectAll=\"{0}\" ", _container.FieldSetting.SelectAll));
            htmlToRender.Append(string.Format("width=\"{0}\" ", _container.FieldSetting.Width > _container.FieldSetting.DefaultWidth ? _container.FieldSetting.Width : _container.FieldSetting.DefaultWidth));
        }        

        private static string SetSelectedItemInOptionElement(IMultiSelectLookupValueField item)
        {
            return item.IsSelected ? "selected=\"selected\"" : string.Empty;
        }
    }
}