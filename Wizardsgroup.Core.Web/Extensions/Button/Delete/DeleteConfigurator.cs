using System;
using System.Web.Mvc;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class DeleteConfigurator : IDeleteConfigurator
    {
        private readonly IButtonConfigBuilder _configBuilder;

        public DeleteConfigurator(IButtonConfigBuilder configBuilder)
        {
            _configBuilder = configBuilder;
            _configBuilder.Configuration.Button.ClassName = "buttonDeleteClass";
        }

        public IDeleteConfigurator Modal(Action<IModalProperConfigurator> configuration)
        {
            _configBuilder.Modal(configuration);
            return this;
        }

        public IDeleteConfigurator SelectionMode(SelectionMode mode)
        {
            _configBuilder.Configuration.SelectionMode = mode;
            return this;
        }

        public IDeleteConfigurator Action(Action<IControllerConfigurator> configuration)
        {
            _configBuilder.Action(configuration);
            return this;
        }

        public MvcHtmlString Render()
        {
            return new ButtonHtmlRenderer(_configBuilder.Configuration,new DeleteRendererStrategy()).Render();
        }
    }
}