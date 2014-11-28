using System;
using System.Collections.Generic;
using Wizardsgroup.Domain.Interfaces;

namespace Wizardsgroup.Core.Web.Extensions
{
    public interface IHtmlCheckboxListHelper<TModel, TValue> where TValue : IEnumerable<IMultiSelectLookupValueField>
    {
        IHtmlCheckboxListHelper<TModel, TValue> CheckboxIsSelectedByDefault(); 
        ICheckboxListHtmlRenderer<TModel, TValue> DataSource(Action<ILookupDataSource> dataSource);
        ICheckboxListHtmlRenderer<TModel, TValue> DataSource(IEnumerable<IMultiSelectLookupValueField> lookupData);        
    }
}