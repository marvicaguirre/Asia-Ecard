using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using Wizardsgroup.Domain.Containers;
using Wizardsgroup.Domain.Interfaces;
using Wizardsgroup.Service.Factories;

namespace Wizardsgroup.Core.Web.Extensions
{
    public class HtmlCheckboxListHelper<TModel, TValue> : IHtmlCheckboxListHelper<TModel, TValue> where TValue : IEnumerable<IMultiSelectLookupValueField>
    {        
        private readonly CheckboxListBuilderDataContainer<TModel, TValue> _checkboxListBuilderDataContainer  = new CheckboxListBuilderDataContainer<TModel, TValue>();

        public HtmlCheckboxListHelper(HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, Mode mode)
        {
            _checkboxListBuilderDataContainer.HtmlHelper = helper;
            _checkboxListBuilderDataContainer.Expression = expression;
            _checkboxListBuilderDataContainer.Mode = mode;
        }
        
        public IHtmlCheckboxListHelper<TModel, TValue> CheckboxIsSelectedByDefault()
        {
            _checkboxListBuilderDataContainer.IsSelectedByDefault = true;
            return this;
        }

        //TODO check if can be executed asynchronous
        public ICheckboxListHtmlRenderer<TModel, TValue> DataSource(Action<ILookupDataSource> provider)
        {
            var dsProvider = new MultiSelectLookupDataSourceProvider();
            provider(dsProvider);
            ILookupFactory factory = dsProvider.LazyFactory.Compile()();
            ILookupFunction lookup = factory.Create<IMultiSelectLookup>(dsProvider.TargetLookup);
            var lookups = lookup.GetRecordsForLookup();
            _checkboxListBuilderDataContainer.LookupValueFields = lookups.Select(o => MultiSelectLookup.Create(o.Text, o.Value));

            return new CheckboxListHtmlRenderer<TModel, TValue>(_checkboxListBuilderDataContainer);
        }

        public ICheckboxListHtmlRenderer<TModel, TValue> DataSource(IEnumerable<IMultiSelectLookupValueField> lookupData)
        {
            _checkboxListBuilderDataContainer.LookupValueFields = lookupData;
            return new CheckboxListHtmlRenderer<TModel, TValue>(_checkboxListBuilderDataContainer);
        }
    }
}