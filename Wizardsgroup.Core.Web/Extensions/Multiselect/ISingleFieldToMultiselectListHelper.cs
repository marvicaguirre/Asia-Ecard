using System;

namespace Wizardsgroup.Core.Web.Extensions
{
    public interface ISingleFieldToMultiselectListHelper<TModel,TValue>
    {
        IMultiselectSettingConfigurator<TModel, TValue> Configuration(Action<IMultiselectSettingBuilder> configurator);        
    }
}