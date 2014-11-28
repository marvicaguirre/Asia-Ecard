using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class SingleFieldToMultiselectListHelper<TModel, TValue> : ISingleFieldToMultiselectListHelper<TModel, TValue>
    {
        private readonly MultiselectBuilderDataContainer<TModel,TValue> _container = new MultiselectBuilderDataContainer<TModel,TValue>();
        public SingleFieldToMultiselectListHelper(HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression)
        {
            _container.HtmlHelper = helper;
            _container.Expression = expression;
        }

        public IMultiselectSettingConfigurator<TModel, TValue> Configuration(Action<IMultiselectSettingBuilder> configurator)
        {
            var settingBuilder = new MultiselectSettingBuilder();
            configurator(settingBuilder);
            _container.FieldSetting = settingBuilder.Container;
            return new MultiselectSettingConfigurator<TModel, TValue>(_container);
        }
    }
}