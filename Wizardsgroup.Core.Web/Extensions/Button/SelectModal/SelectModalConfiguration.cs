using System;
using System.Web.Mvc;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class SelectModalConfiguration : ISelectModalConfiguration
    {
        private readonly IButtonConfigBuilder _configBuilder;

        public SelectModalConfiguration(IButtonConfigBuilder configBuilder)
        {
            _configBuilder = configBuilder;
            _configBuilder.Configuration.Button.ClassName = "buttonSelectModalClass";
        }

        public ISelectModalConfiguration Modal(Action<IModalProperConfigurator> configuration)
        {
            _configBuilder.Modal(configuration);
            return this;
        }

        public ISelectModalConfiguration Action(Action<IControllerSelectModalConfigurator> configuration)
        {
            configuration(new ControllerSelectModalConfigurator(new ControllerConfigurator(_configBuilder.Configuration.Controller)));
            return this;
        }

        public ISelectModalConfiguration SelectionMode(SelectionMode mode)
        {
            _configBuilder.Configuration.SelectionMode = mode;
            return this;
        }

        public MvcHtmlString Render()
        {
            return new ButtonHtmlRenderer(_configBuilder.Configuration, new SelectModalRendererStrategy()).Render();
        }
    }
}