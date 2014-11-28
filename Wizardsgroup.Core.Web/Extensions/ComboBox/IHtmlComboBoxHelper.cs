using System;
using System.Linq.Expressions;

namespace Wizardsgroup.Core.Web.Extensions
{
    public interface IHtmlComboBoxHelper<TModel, TValue>
    {
        IHtmlComboBoxHelper<TModel, TValue> CascadeFrom(Expression<Func<TModel, TValue>> expression);
        //IComboBoxConfigurator<TModel, TValue> DataSource(Action<ILookupDataSource> dataSource);
        IComboBoxConfigurator<TModel, TValue> DataSource(string entityLookup);
        IComboBoxConfigurator<TModel, TValue> DataSource(Action<ICustomActionDataSource> dataSource);
        //IComboBoxConfigurator<TModel, TValue> DataSource(IEnumerable<ILookupValueField> lookupItems);
    }
}