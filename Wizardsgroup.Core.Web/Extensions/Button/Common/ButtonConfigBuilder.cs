using System;

namespace Wizardsgroup.Core.Web.Extensions
{
    internal class ButtonConfigBuilder : IButtonConfigBuilder
    {
        #region Member
        public ButtonConfigurationContainer Configuration { get; private set; } 
        #endregion

        #region Constructor
        public ButtonConfigBuilder()
        {
            Configuration = new ButtonConfigurationContainer();
        } 
        #endregion

        public IButtonConfigBuilder Modal(Action<IModalProperConfigurator> configuration)
        {
            IModalProperConfigurator configurator = new ModalProperConfigurator(Configuration.Modal);
            configuration(configurator);
            return this;
        }

        public IButtonConfigBuilder Action(Action<IControllerConfigurator> configuration)
        {
            IControllerConfigurator configurator = new ControllerConfigurator(Configuration.Controller);
            configuration(configurator);
            return this;
        }

        public IButtonConfigBuilder Route(object route = null)
        {
            Configuration.Route = route;
            return this;
        }   
    }
}