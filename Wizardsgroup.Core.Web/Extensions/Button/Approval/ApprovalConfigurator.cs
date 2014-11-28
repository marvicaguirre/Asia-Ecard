using System;
using System.Web.Mvc;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class ApprovalConfigurator : IApprovalConfigurator
    {
        private readonly IButtonConfigBuilder _configBuilder;

        public ApprovalConfigurator(IButtonConfigBuilder configBuilder)
        {
            _configBuilder = configBuilder;
            _configBuilder.Configuration.Button.ClassName = "buttonApprovalClass";
        }

        public IApprovalConfigurator Modal(Action<IModalProperConfigurator> configuration)
        {
            _configBuilder.Modal(configuration);
            return this;
        }

        public IApprovalConfigurator SelectionMode(SelectionMode mode)
        {
            _configBuilder.Configuration.SelectionMode = mode;
            return this;
        }

        public IApprovalConfigurator Action(Action<IControllerApprovalConfigurator> configuration)
        {            
            configuration(new ControllerApprovalConfigurator(new ControllerConfigurator(_configBuilder.Configuration.Controller)));
            return this;
        }

        public MvcHtmlString Render()
        {
            return new ButtonHtmlRenderer(_configBuilder.Configuration, new ApprovalRendererStartegy()).Render();
        }
    }
}