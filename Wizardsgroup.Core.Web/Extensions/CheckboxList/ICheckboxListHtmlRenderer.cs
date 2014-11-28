using System.Collections.Generic;
using System.Web.Mvc;
using Wizardsgroup.Core.Web.Enums;
using Wizardsgroup.Domain.Interfaces;

namespace Wizardsgroup.Core.Web.Extensions
{
    public interface ICheckboxListHtmlRenderer<TModel, TValue> where TValue : IEnumerable<IMultiSelectLookupValueField>
    {
        MvcHtmlString RenderHtml();
        MvcHtmlString RenderHtml(CheckboxListForType type);
    }
}