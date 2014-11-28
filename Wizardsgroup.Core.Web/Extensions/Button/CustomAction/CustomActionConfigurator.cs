using System;
using System.Web.Mvc;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class CustomActionConfigurator : ICustomActionConfigurator
    {
        private readonly IButtonConfigBuilder _configBuilder;

        public CustomActionConfigurator(IButtonConfigBuilder configBuilder)
        {
            _configBuilder = configBuilder;
            _configBuilder.Configuration.Button.ClassName = "buttonActionClass";
        }

        public ICustomActionConfigurator Action(Action<IControllerCustomActionConfigurator> configuration)
        {
            configuration(new ControllerCustomActionConfigurator(new ControllerConfigurator(_configBuilder.Configuration.Controller),_configBuilder.Configuration));
            return this;
        }

        public MvcHtmlString Render()
        {
            return new ButtonHtmlRenderer(_configBuilder.Configuration, new CustomActionRendererStrategy()).Render();
        }
    }
}