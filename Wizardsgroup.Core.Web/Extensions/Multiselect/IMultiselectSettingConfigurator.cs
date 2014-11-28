using System;
using System.Collections.Generic;
using Wizardsgroup.Domain.Interfaces;

namespace Wizardsgroup.Core.Web.Extensions
{
    public interface IMultiselectSettingConfigurator<TModel, TValue>
    {
        ISingleFieldToMultiselectHtmlRenderer<TModel, TValue> DataSource(Action<ILookupDataSource> provider);
        ISingleFieldToMultiselectHtmlRenderer<TModel, TValue> DataSource(IEnumerable<IMultiSelectLookupValueField> lookupData); 
    }
}