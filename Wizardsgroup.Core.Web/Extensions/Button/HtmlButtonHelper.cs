using System;
using System.Web.Mvc;
using Wizardsgroup.Utilities.Extensions;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class HtmlButtonHelper : IHtmlButtonHelper
    {        
        readonly IButtonConfigBuilder _configBuilder = new ButtonConfigBuilder();
        public HtmlButtonHelper(HtmlHelper helper,string text)
        {
            helper.Guard("HtmlHelper must not be null.");
            text.Guard("Button must not be null or empty.");
            _configBuilder.Configuration.HtmlHelper = helper;
            _configBuilder.Configuration.Button.Text = text;
        }

        public ICreateConfigurator Create(Action<IButtonPropertyConfigurator> configuration)
        {            
            SetPropertiesFromAction(_configBuilder, configuration);
            return new CreateConfigurator(_configBuilder);
        }

        public IDeleteConfigurator Delete(Action<IButtonPropertyConfigurator> configuration)
        {
            SetPropertiesFromAction(_configBuilder, configuration);
            return new DeleteConfigurator(_configBuilder);
        }

        public IApprovalConfigurator Approval(Action<IButtonPropertyConfigurator> configuration)
        {
            SetPropertiesFromAction(_configBuilder, configuration);
            return new ApprovalConfigurator(_configBuilder);
        }

        public IToggleConfigurator Toggle(Action<IButtonPropertyConfigurator> configuration)
        {
            SetPropertiesFromAction(_configBuilder,configuration);
            return new ToggleConfigurator(_configBuilder);
        }

        public IConfirmConfigurator Confirm(Action<IButtonPropertyConfigurator> configuration)
        {
            SetPropertiesFromAction(_configBuilder, configuration);
            return new ConfirmConfigurator(_configBuilder);
        }

        public IModalConfigurator CustomModal(Action<IButtonPropertyConfigurator> configuration)
        {
            SetPropertiesFromAction(_configBuilder, configuration);
            return new ModalConfigurator(_configBuilder);
        }

        public ICustomActionConfigurator CustomAction(Action<IButtonPropertyConfigurator> configuration)
        {
            SetPropertiesFromAction(_configBuilder, configuration);
            return new CustomActionConfigurator(_configBuilder);
        }

        public ISelectModalConfiguration SelectionModal(Action<IButtonPropertyConfigurator> configuration)
        {
            SetPropertiesFromAction(_configBuilder, configuration);
            return new SelectModalConfiguration(_configBuilder);
        }

        private void SetPropertiesFromAction(IButtonConfigBuilder builder,Action<IButtonPropertyConfigurator> configuration)
        {
            IButtonPropertyConfigurator configurator = new ButtonPropertyConfigurator(builder.Configuration.Button);
            configuration(configurator);             
        }
    }
}