using System;
using System.Web.Mvc;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class ModalConfigurator : IModalConfigurator
    {
        private readonly IButtonConfigBuilder _configBuilder;

        public ModalConfigurator(IButtonConfigBuilder configBuilder)
        {
            _configBuilder = configBuilder;
            _configBuilder.Configuration.Button.ClassName = "buttonModalClass";
        }

        public IModalConfigurator Modal(Action<IModalProperConfigurator> configuration)
        {
            _configBuilder.Modal(configuration);
            return this;
        }

        public IModalConfigurator Action(string action, string controller)
        {
            _configBuilder.Action(o => o.Controller(controller).Action(action));
            return this;
        }

        public IModalConfigurator Route(object route = null)
        {
            _configBuilder.Route(route);
            return this;
        }

        public MvcHtmlString Render()
        {
            return new ButtonHtmlRenderer(_configBuilder.Configuration, new ModalRendererStrategy()).Render();
        }
    }
}