using System;
using System.Web.Mvc;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class ConfirmConfigurator : IConfirmConfigurator
    {
        private readonly IButtonConfigBuilder _configBuilder;

        public ConfirmConfigurator(IButtonConfigBuilder configBuilder)
        {
            _configBuilder = configBuilder;
            _configBuilder.Configuration.Button.ClassName = "buttonConfirmClass";
        }

        public IConfirmConfigurator Modal(Action<IModalProperConfigurator> configuration)
        {
            _configBuilder.Modal(configuration);
            return this;
        }

        public IConfirmConfigurator SelectionMode(SelectionMode mode)
        {
            _configBuilder.Configuration.SelectionMode = mode;
            return this;
        }

        public IConfirmConfigurator Action(Action<IControllerConfigurator> configuration)
        {
            _configBuilder.Action(configuration);
            return this;
        }

        public MvcHtmlString Render()
        {
            return new ButtonHtmlRenderer(_configBuilder.Configuration, new ConfirmRendererStrategy()).Render();
        }
    }
}