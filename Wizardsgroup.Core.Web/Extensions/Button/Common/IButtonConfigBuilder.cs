using System;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal interface IButtonConfigBuilder
    {
        ButtonConfigurationContainer Configuration { get; }
        IButtonConfigBuilder Modal(Action<IModalProperConfigurator> configuration);
        IButtonConfigBuilder Action(Action<IControllerConfigurator> configuration);
        IButtonConfigBuilder Route(object route = null);
    }
}