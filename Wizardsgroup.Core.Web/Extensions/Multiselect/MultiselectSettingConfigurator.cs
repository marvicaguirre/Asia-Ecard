using System;
using System.Collections.Generic;
using System.Linq;
using Wizardsgroup.Domain.Containers;
using Wizardsgroup.Domain.Interfaces;
using Wizardsgroup.Service.Factories;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class MultiselectSettingConfigurator<TModel, TValue> : IMultiselectSettingConfigurator<TModel, TValue>
    {
        private readonly MultiselectBuilderDataContainer<TModel,TValue> _container;
        public MultiselectSettingConfigurator(MultiselectBuilderDataContainer<TModel, TValue> container)
        {
            _container = container;
        }

        public ISingleFieldToMultiselectHtmlRenderer<TModel, TValue> DataSource(Action<ILookupDataSource> provider)
        {
            var dsProvider = new MultiSelectLookupDataSourceProvider();
            provider(dsProvider);
            ILookupFactory factory = dsProvider.LazyFactory.Compile()();
            ILookupFunction lookup = factory.Create<ILookupFunction>(dsProvider.TargetLookup);            
            var lookups = lookup.GetRecordsForLookup();
            _container.LookupValueFields = lookups.Select(o => MultiSelectLookup.Create(o.Text, o.Value));
            return new SingleFieldToMultiselectHtmlRenderer<TModel,TValue>(_container);
        }

        public ISingleFieldToMultiselectHtmlRenderer<TModel, TValue> DataSource(IEnumerable<IMultiSelectLookupValueField> lookupData)
        {
            _container.LookupValueFields = lookupData;
            return new SingleFieldToMultiselectHtmlRenderer<TModel,TValue>(_container);
        }
    }
}