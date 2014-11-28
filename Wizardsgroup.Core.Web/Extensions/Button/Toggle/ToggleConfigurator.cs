using System;
using System.Web.Mvc;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class ToggleConfigurator : IToggleConfigurator
    {
        private readonly IButtonConfigBuilder _configBuilder;

        public ToggleConfigurator(IButtonConfigBuilder configBuilder)
        {
            _configBuilder = configBuilder;
            _configBuilder.Configuration.Button.ClassName = "buttonToggleClass";
        }

        public IToggleConfigurator Modal(Action<IModalProperConfigurator> configuration)
        {
            _configBuilder.Modal(configuration);
            return this;
        }

        public IToggleConfigurator Action(string controller)
        {
            _configBuilder.Action(o => o.Controller(controller).Confirm("ConfirmItems").Action("ToggleStatus"));
            return this;
        }

        public MvcHtmlString Render()
        {
            return new ButtonHtmlRenderer(_configBuilder.Configuration, new ToggleRendererStrategy()).Render();
        }
    }
}