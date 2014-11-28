using System;
using System.Web.Mvc;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class CreateConfigurator : ICreateConfigurator
    {
        private readonly IButtonConfigBuilder _configBuilder;

        public CreateConfigurator(IButtonConfigBuilder configBuilder)
        {            
            _configBuilder = configBuilder;
            _configBuilder.Configuration.Button.ClassName = "buttonEntryClass";
        }

        public ICreateConfigurator Modal(Action<IModalProperConfigurator> configuration)
        {
            _configBuilder.Modal(configuration);
            return this;
        }

        public ICreateConfigurator Action(string action, string controller)
        {
            _configBuilder.Action(o=>o.Controller(controller).Action(action));
            return this;
        }

        public ICreateConfigurator Route(object route = null)
        {
            _configBuilder.Route(route);
            return this;
        }

        public MvcHtmlString Render()
        {            
            return new ButtonHtmlRenderer(_configBuilder.Configuration, new CreateRendererStrategy()).Render();
        }
    }
}